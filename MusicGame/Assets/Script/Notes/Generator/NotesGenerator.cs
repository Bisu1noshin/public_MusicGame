using System;
using UnityEngine;

namespace Notes {

    public class NotesGenerator 
    {
        public static GameObject CreateNotes(GameObject obj, Notes notes,int bpm)
        {
            GameObject go = obj;

            if (go.TryGetComponent<NotesObject>(out var n_))
            {
                n_.SetInitilizeNotes(notes, bpm);
            }
            else
            {
                throw new InvalidOperationException(
                "オブジェクトに指定のコンポーネントが存在しません");
            }

            return go;
        }
    }
}
