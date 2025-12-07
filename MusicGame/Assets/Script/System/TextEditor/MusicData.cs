using System;
using System.Collections.Generic;

namespace Music
{
    public enum Level
    {
        None = -1,
        Normal,
        Hard,
        Expert,
        Evil
    }

    // 曲のデータを保存する構造体

    public interface IMusicInfo {
        string Name { get; } //名前
        string Src { get; } //音源ファイルのパス
    }

    //曲名、音源、対応する譜面データを持つだけの簡易的クラス
    public class Music : IMusicInfo {
        public Music(string name_, string src_)
        {
            name = name_;
            src = GetMusicPath(src_);
            notesFilePath = new();
        }
        public string Name => name;
        public string Src => src;
        public string GetFilePath(Level level) => notesFilePath[(int)level];
        public void SetFilePath(List<string> path) => notesFilePath.AddRange(path);
        string GetMusicPath(string path_) => MusicRootPath + "/" + path_;

        private readonly string name, src;
        private List<string> notesFilePath;

        public static readonly string MusicRootPath = "";
    }

    public class MusicData
    {
        // メンバー変数
        public List<Music> music { get; private set; }


        // コンストラクタ
        public MusicData() {

            music = new List<Music>();
        }

    }
}