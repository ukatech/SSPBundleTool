# SSP initial_install.nar 同梱ツール

SSPの自己解凍形式アーカイブ（SFX）に `initial_install.nar` を追加するWindowsツールです。

右のReleasesからダウンロードしてください。

## 概要

[SSP](https://ssp.shillest.net/) はルートディレクトリに `initial_install.nar` が存在すると、起動時に自動インストール処理を行います。本ツールは自己解凍形式で配布しているSSPフルセットに `initial_install.nar` を同梱し、展開後に自動インストールが走る自己解凍SSPパッケージを生成します。

## 動作要件

- Windows 10 / Windows 11
- [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)（Windows 10以降は標準搭載）
- [7-Zip](https://www.7-zip.org/) がインストール済みであること
  - `C:\Program Files\7-Zip\7z.exe` または `C:\Program Files (x86)\7-Zip\7z.exe` を自動検出します

## 使い方

1. `SSPBundleTool.exe` を起動する
2. **自己解凍形式のSSPフルセット** — SSPのSFX（例: `ssp_2_7_72f.exe`）を選択する
3. **初期インストールに使用するnar** — 同梱したい `initial_install.nar` を選択する
4. **出力ファイル** — 入力SFXを選ぶと `元のファイル名_bundled.exe` が自動入力される（変更も可）
5. **実行** ボタンを押す
6. ログに「完了しました！」と表示されれば成功

入力・出力ファイルのパスは次回起動時に復元されます（`%APPDATA%\SSPBundleTool\settings.txt`）。

