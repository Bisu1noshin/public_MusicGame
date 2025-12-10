using System;
using UnityEngine;

namespace Notes {
    public class NotesInformaiton
    {
        public float CteateTime { get; private set; }

        public GameObject NotesObj { get; private set; }

        public Notes Notes { get; private set; }

        public int BPM { get; private set; }

        public Vector3 CreatePos { get; private set; }

        public Vector3 CreateRot { get; private set; }

        public NotesManagerForNotesData Judgment { get; private set; }

        public int NotesNum { get; private set; }
        public NotesLane lane{ get; private set; }

        public NotesInformaiton
            (float create,GameObject obj,Notes n,int bpm, NotesManagerForNotesData j,int num,NotesLane l_, Vector3 v,Vector3 q)
        {
            CteateTime = create;
            NotesObj = obj;
            Notes = n;
            NotesNum = num;
            BPM = bpm;
            Judgment = j;
            CreatePos = v;
            CreateRot = q;
            lane = l_;
        }
    }

    public static class NotesGenerator 
    {
        public static GameObject CreateNotes
            (NotesInformaiton informaiton)
        {
            Type[] notes =
            {
            typeof(FlickNotes),
            typeof(HoldNotes),
            typeof(RushNotes),
            };

            Quaternion q = Quaternion.Euler(informaiton.CreateRot);
            GameObject go = GameObject.Instantiate(informaiton.NotesObj, informaiton.CreatePos, q);

            // スクリプトを動的にアタッチ
            go.AddComponent(notes[(int)informaiton.Notes.kind]);

            // 変数を初期化
            var n_ = go.GetComponent<NotesObject>();
            n_.SetInitilizeNotes(informaiton);

            return go;
        }
    }
}
