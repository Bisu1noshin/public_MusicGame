using System;
using System.Collections.Generic;

namespace Music {

    // 曲のデータを保存する構造体

    public struct Music {


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