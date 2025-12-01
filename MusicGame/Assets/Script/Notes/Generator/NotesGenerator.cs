using System;
using UnityEngine;

namespace Notes {

    public static class NotesGenerator 
    {
        public static GameObject CreateNotes
            (GameObject obj, Notes n,int bpm,Vector3 v_,Vector3 q_)
        {
            Type[] notes =
            {
            typeof(FlickNotes),
            typeof(HoldNotes),
            typeof(RushNotes),
            };

            Quaternion q = Quaternion.Euler(q_);
            GameObject go = GameObject.Instantiate(obj, v_, q);

            // スクリプトを動的にアタッチ
            go.AddComponent(notes[(int)n.kind]);

            // 変数を初期化
            var n_ = go.GetComponent<NotesObject>();
            n_.SetInitilizeNotes(n, bpm);

            return go;
        }
    }
}
