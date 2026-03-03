using System;
using System.Diagnostics;
using System.IO;

namespace SSPBundleTool
{
    public static class SfxProcessor
    {
        private static readonly byte[] SevenZipSignature = { 0x37, 0x7A, 0xBC, 0xAF, 0x27, 0x1C };

        private static readonly string[] SevenZipCandidates =
        {
            @"C:\Program Files\7-Zip\7z.exe",
            @"C:\Program Files (x86)\7-Zip\7z.exe",
        };

        public static void AddNarToSfx(
            string inputSfxPath,
            string narFilePath,
            string outputSfxPath,
            IProgress<string> progress)
        {
            string sevenZipExe = FindSevenZip();
            if (sevenZipExe == null)
                throw new FileNotFoundException(
                    "7-Zip (7z.exe) が見つかりませんでした。\n" +
                    "https://www.7-zip.org/ から7-Zipをインストールしてください。");

            progress.Report($"7-Zip を使用: {sevenZipExe}");
            progress.Report("入力ファイルを読み込み中...");

            byte[] inputBytes = File.ReadAllBytes(inputSfxPath);

            progress.Report("7zシグネチャを検索中...");
            int sigOffset = FindSignature(inputBytes, SevenZipSignature);
            if (sigOffset < 0)
                throw new InvalidDataException("7zシグネチャが見つかりませんでした。入力ファイルが正しいSFXファイルか確認してください。");

            progress.Report($"7zアーカイブ開始オフセット: {sigOffset} bytes");

            byte[] prefix = new byte[sigOffset];
            Array.Copy(inputBytes, 0, prefix, 0, sigOffset);

            string tempFile = Path.GetTempFileName();
            // 拡張子を .7z にしないと 7z.exe が形式を認識しない場合があるため改名
            string temp7z = tempFile + ".7z";
            // アーカイブ内でのファイル名を initial_install.nar に固定するため、
            // 元のファイル名に関わらず同名の一時ファイルにコピーしてから追加する
            string tempNar = Path.Combine(Path.GetTempPath(), "initial_install.nar");
            try
            {
                progress.Report("7zアーカイブ部分を一時ファイルに書き出し中...");
                File.Delete(tempFile);
                using (var fs = File.Create(temp7z))
                {
                    fs.Write(inputBytes, sigOffset, inputBytes.Length - sigOffset);
                }

                File.Copy(narFilePath, tempNar, overwrite: true);

                progress.Report("initial_install.nar をアーカイブに追加中...");
                // -w で作業ディレクトリを一時フォルダに指定し、ファイル名のみがパスに使われるようにする
                RunSevenZip(sevenZipExe, $"a \"{temp7z}\" \"{tempNar}\" -w\"{Path.GetTempPath()}\"", progress);

                progress.Report($"出力ファイルを書き込み中: {outputSfxPath}");
                byte[] newArchiveBytes = File.ReadAllBytes(temp7z);
                using (var outStream = File.Create(outputSfxPath))
                {
                    outStream.Write(prefix, 0, prefix.Length);
                    outStream.Write(newArchiveBytes, 0, newArchiveBytes.Length);
                }

                progress.Report("完了しました！");
            }
            finally
            {
                if (File.Exists(temp7z))
                    File.Delete(temp7z);
                if (File.Exists(tempNar))
                    File.Delete(tempNar);
            }
        }

        private static void RunSevenZip(string exe, string arguments, IProgress<string> progress)
        {
            var psi = new ProcessStartInfo(exe, arguments)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            using (var proc = Process.Start(psi))
            {
                string stdout = proc.StandardOutput.ReadToEnd();
                string stderr = proc.StandardError.ReadToEnd();
                proc.WaitForExit();

                if (!string.IsNullOrWhiteSpace(stdout))
                    foreach (var line in stdout.Split('\n'))
                        if (!string.IsNullOrWhiteSpace(line))
                            progress.Report(line.TrimEnd());

                if (proc.ExitCode != 0)
                    throw new Exception($"7z.exe がエラーで終了しました (exit code {proc.ExitCode})。\n{stderr}");
            }
        }

        private static string FindSevenZip()
        {
            foreach (var path in SevenZipCandidates)
                if (File.Exists(path))
                    return path;
            return null;
        }

        private static int FindSignature(byte[] data, byte[] signature)
        {
            for (int i = 0; i <= data.Length - signature.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < signature.Length; j++)
                {
                    if (data[i + j] != signature[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                    return i;
            }
            return -1;
        }
    }
}
