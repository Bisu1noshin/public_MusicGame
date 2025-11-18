using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Notes {
    public enum Direction
    {
        Non = -1,
        Top = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public enum NotesKind
    {
        None = -1,
        Flick = 0,
        Hold = 1,
        Rush = 2
    };

    public enum NotesLane
    {
        None,
        Left = 0,
        Right = 1
    }

    // ノーツを召喚するための構造体
    public class Notes
    {
        public Notes(int time_, int dirN, int kind)
        {
            this.time = time_;
            this.dir = (Direction)dirN;
            this.kind = (NotesKind)kind;
        }

        public int time { get; private set; }
        public Direction dir { get; private set; }
        public NotesKind kind { get; private set; }
    }

    // 曲に対応したノーツのデータを保存するクラス
    public class NotesData
    {

        // メンバー変数

        public int BPM { get; private set; }

        public List<Notes> notes { get; private set; }

        public NotesData() {

            BPM = 0;
            notes = new List<Notes>();
        }
    }
}

