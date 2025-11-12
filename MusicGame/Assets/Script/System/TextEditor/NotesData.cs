using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Notes {
    public enum Direction
    {
        Non = -1, Top, Right, Down, Left
    }

    // ノーツを召喚するための構造体
    public struct Notes
    {
        public Notes(float time_, int dirN, bool isH, int lane_)
        {
            time = time_;
            dir = (Direction)dirN;
            isHold = isH;
            lane = lane_;
        }
        float time;
        Direction dir;
        bool isHold;
        int lane;
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

