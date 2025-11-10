using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Notes {

    // ノーツを召喚するための構造体
    public struct Notes
    {


    }

    // 曲に対応したノーツのデータを保存するクラス
    public class NotesData
    {

        // メンバー変数

        public int BPM { get; private set; }

        public List<Notes> notes { get; private set; }
    }
}

