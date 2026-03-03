using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace SSPBundleTool
{
    public partial class MainForm : Form
    {
        private static readonly string SettingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SSPBundleTool", "settings.txt");

        private BackgroundWorker worker;

        public MainForm()
        {
            InitializeComponent();
            InitializeWorker();
            LoadSettings();
        }

        private void InitializeWorker()
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = false;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        // ── 設定の保存・読み込み ───────────────────────────────

        private void LoadSettings()
        {
            if (!File.Exists(SettingsPath))
                return;

            var settings = new Dictionary<string, string>();
            foreach (var line in File.ReadAllLines(SettingsPath))
            {
                int sep = line.IndexOf('=');
                if (sep < 0) continue;
                settings[line.Substring(0, sep)] = line.Substring(sep + 1);
            }

            if (settings.TryGetValue("SfxPath", out string sfx))
                txtSfxPath.Text = sfx;
            if (settings.TryGetValue("NarPath", out string nar))
                txtNarPath.Text = nar;
            if (settings.TryGetValue("OutputPath", out string output))
                txtOutputPath.Text = output;
        }

        private void SaveSettings()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath));
                File.WriteAllLines(SettingsPath, new[]
                {
                    $"SfxPath={txtSfxPath.Text}",
                    $"NarPath={txtNarPath.Text}",
                    $"OutputPath={txtOutputPath.Text}",
                });
            }
            catch { /* 保存失敗は無視 */ }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveSettings();
            base.OnFormClosing(e);
        }

        // ── ファイル参照 ──────────────────────────────────────

        private void btnBrowseSfx_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "自己解凍形式のSSPフルセットを選択";
                dlg.Filter = "実行ファイル (*.exe)|*.exe|すべてのファイル (*.*)|*.*";
                if (!string.IsNullOrEmpty(txtSfxPath.Text))
                    dlg.InitialDirectory = Path.GetDirectoryName(txtSfxPath.Text);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtSfxPath.Text = dlg.FileName;
                    AutoFillOutputPath();
                }
            }
        }

        private void btnBrowseNar_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "initial_install.nar を選択";
                dlg.Filter = "narファイル (*.nar)|*.nar|すべてのファイル (*.*)|*.*";
                if (!string.IsNullOrEmpty(txtNarPath.Text))
                    dlg.InitialDirectory = Path.GetDirectoryName(txtNarPath.Text);
                if (dlg.ShowDialog() == DialogResult.OK)
                    txtNarPath.Text = dlg.FileName;
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = "出力ファイルの保存先を選択";
                dlg.Filter = "実行ファイル (*.exe)|*.exe|すべてのファイル (*.*)|*.*";
                dlg.DefaultExt = "exe";
                if (!string.IsNullOrEmpty(txtOutputPath.Text))
                {
                    dlg.InitialDirectory = Path.GetDirectoryName(txtOutputPath.Text);
                    dlg.FileName = Path.GetFileName(txtOutputPath.Text);
                }
                if (dlg.ShowDialog() == DialogResult.OK)
                    txtOutputPath.Text = dlg.FileName;
            }
        }

        private void AutoFillOutputPath()
        {
            string sfxPath = txtSfxPath.Text;
            if (string.IsNullOrEmpty(sfxPath))
                return;

            string dir = Path.GetDirectoryName(sfxPath);
            string nameWithoutExt = Path.GetFileNameWithoutExtension(sfxPath);
            string ext = Path.GetExtension(sfxPath);
            txtOutputPath.Text = Path.Combine(dir, nameWithoutExt + "_bundled" + ext);
        }

        // ── 実行 ──────────────────────────────────────────────

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSfxPath.Text))
            {
                MessageBox.Show("SFXファイルを選択してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNarPath.Text))
            {
                MessageBox.Show("narファイルを選択してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOutputPath.Text))
            {
                MessageBox.Show("出力ファイルのパスを指定してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveSettings();
            txtLog.Clear();
            btnExecute.Enabled = false;

            worker.RunWorkerAsync(new WorkerArgs
            {
                InputSfxPath = txtSfxPath.Text,
                NarFilePath = txtNarPath.Text,
                OutputSfxPath = txtOutputPath.Text,
            });
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (WorkerArgs)e.Argument;
            var progress = new Progress<string>(msg =>
            {
                if (InvokeRequired)
                    Invoke(new Action(() => AppendLog(msg)));
                else
                    AppendLog(msg);
            });

            SfxProcessor.AddNarToSfx(
                args.InputSfxPath,
                args.NarFilePath,
                args.OutputSfxPath,
                progress);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnExecute.Enabled = true;

            if (e.Error != null)
            {
                AppendLog($"エラー: {e.Error.Message}");
                MessageBox.Show(
                    $"処理中にエラーが発生しました。\n\n{e.Error.Message}",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AppendLog(string message)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            txtLog.ScrollToCaret();
        }

        private class WorkerArgs
        {
            public string InputSfxPath { get; set; }
            public string NarFilePath { get; set; }
            public string OutputSfxPath { get; set; }
        }
    }
}
