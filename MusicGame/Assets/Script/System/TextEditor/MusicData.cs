using Notes;
using System;
using System.Collections.Generic;

namespace Music
{

    // 曲のデータを保存する構造体

    public class Music
    {
        public string name { get; private set; }
        public string src { get; private set; }
        public List<NotesData> list;
        public Music(string name_, string src_)
        {
            this.name = name_;
            this.src = src_;
        }
        public void AddList(NotesData data_)
        {
            list.Add(data_);
        }
    }

    public class MusicData
    {
        // メンバー変数
        public List<Music> music { get; private set; }


        // コンストラクタ
        public MusicData()
        {

            music = new List<Music>();
        }

    }
}
