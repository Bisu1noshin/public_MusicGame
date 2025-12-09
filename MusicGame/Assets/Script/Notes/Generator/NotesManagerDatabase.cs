using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Notes
{
    [CreateAssetMenu(fileName = "NotesManagerData", menuName = "Scriptable Objects/NotesManagerData")]
    public class NotesManagerDatabase:ScriptableObject
    {
        public NotesManagerForFileData fData;
        public NotesManagerForNotesData nData;
    }

    [System.Serializable]
    public class NotesManagerForFileData
    {        
        // 曲選択系

        [Tooltip("曲のファイルパッシュ")]
        public string MusicFilePath;

        [Tooltip("ノーツのファイルパッシュ")]
        public string NotesDataFilePath;
    }

    [System.Serializable]
    public class NotesManagerForNotesData
    {
        // ノーツ関係

        [Range(0, 10)]
        [Tooltip("判定までのディレイ")]
        public float JudgmentTimeDelay;

        [Range(0, 1)]
        [Tooltip("Perfect判定の幅")]
        public float PerfectTime;

        [Range(0, 1)]
        [Tooltip("Good判定の幅")]
        public float GoodTime;

        [Tooltip("オートモード")]
        public bool AoutPlay;
    }
}
