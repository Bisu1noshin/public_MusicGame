using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Notes {
    public enum Direction
    {
        Non = -1,
        Top,
        Right,
        Down,
        Left
    }

    public enum NotesKind
    {
        None = -1,
        Flick,
        Hold,
        Rush
    };

    public enum NotesLane
    {
        None = -1,
        Left,
        Right
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

        public List<Notes>[] notes { get; set; }

        public NotesData(int bpm = 0)
        {

            BPM = bpm;
            notes = new List<Notes>[2];

            for (int i = 0; i < notes.Length; i++) {

                notes[i] = new List<Notes>();
            }
        }
    }

    //public interface INotesListEditor
    //{
    //    void AddNotes(Notes notes_);
    //    void AddList(List<Notes> list);
    //    void DeleteAllNotes();
    //}
}

