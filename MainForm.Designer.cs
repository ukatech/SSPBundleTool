namespace SSPBundleTool
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblSfx = new System.Windows.Forms.Label();
            this.txtSfxPath = new System.Windows.Forms.TextBox();
            this.btnBrowseSfx = new System.Windows.Forms.Button();
            this.lblNar = new System.Windows.Forms.Label();
            this.txtNarPath = new System.Windows.Forms.TextBox();
            this.btnBrowseNar = new System.Windows.Forms.Button();
            this.lblOutput = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.lblLog = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();

            // lblSfx
            this.lblSfx.AutoSize = true;
            this.lblSfx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSfx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSfx.Text = "自己解凍形式のSSPフルセット:";

            // txtSfxPath
            this.txtSfxPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSfxPath.ReadOnly = true;

            // btnBrowseSfx
            this.btnBrowseSfx.Text = "参照...";
            this.btnBrowseSfx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBrowseSfx.Click += new System.EventHandler(this.btnBrowseSfx_Click);

            // lblNar
            this.lblNar.AutoSize = true;
            this.lblNar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNar.Text = "初期インストールに使用するnar:";

            // txtNarPath
            this.txtNarPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNarPath.ReadOnly = true;

            // btnBrowseNar
            this.btnBrowseNar.Text = "参照...";
            this.btnBrowseNar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBrowseNar.Click += new System.EventHandler(this.btnBrowseNar_Click);

            // lblOutput
            this.lblOutput.AutoSize = true;
            this.lblOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOutput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOutput.Text = "出力ファイル:";

            // txtOutputPath
            this.txtOutputPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutputPath.ReadOnly = true;

            // btnBrowseOutput
            this.btnBrowseOutput.Text = "参照...";
            this.btnBrowseOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);

            // tableLayoutPanel (3 rows x 3 cols for file inputs)
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel.Controls.Add(this.lblSfx, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.txtSfxPath, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.btnBrowseSfx, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.lblNar, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.txtNarPath, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.btnBrowseNar, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.lblOutput, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.txtOutputPath, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.btnBrowseOutput, 2, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel.Size = new System.Drawing.Size(664, 96);
            this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(0);

            // btnExecute
            this.btnExecute.Text = "実行";
            this.btnExecute.Size = new System.Drawing.Size(200, 36);
            this.btnExecute.Location = new System.Drawing.Point(8, 112);
            this.btnExecute.Font = new System.Drawing.Font(this.Font.FontFamily, 11F, System.Drawing.FontStyle.Bold);
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);

            // lblLog
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(8, 156);
            this.lblLog.Text = "ログ:";

            // txtLog
            this.txtLog.Multiline = true;
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Location = new System.Drawing.Point(8, 174);
            this.txtLog.Size = new System.Drawing.Size(664, 200);
            this.txtLog.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.ForeColor = System.Drawing.Color.LimeGreen;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 400);
            this.MinimumSize = new System.Drawing.Size(680, 400);
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "SSP initial_install.nar 同梱ツール";
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.txtLog);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblSfx;
        private System.Windows.Forms.TextBox txtSfxPath;
        private System.Windows.Forms.Button btnBrowseSfx;
        private System.Windows.Forms.Label lblNar;
        private System.Windows.Forms.TextBox txtNarPath;
        private System.Windows.Forms.Button btnBrowseNar;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    }
}
