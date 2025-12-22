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
        public NotesManagerPlayerConfig PlayerConfig;
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
        [Tooltip("召喚から判定までのディレイ")]
        public float CreateTimeDelay;

        [Range(0, 1)]
        [Tooltip("ノーツ判定のディレイ")]
        public float JudgmentTimeDelay;

        [Range(0, 1)]
        [Tooltip("Perfect判定の幅")]
        public float PerfectTime;

        [Range(0, 1)]
        [Tooltip("Good判定の幅")]
        public float GoodTime;

        [Range(0, 3)]
        [Tooltip("再生速度")]
        public float MusicSpeed;

        [Tooltip("オートモード")]
        public bool AuotPlay;
    }

    [System.Serializable]
    public class NotesManagerPlayerConfig
    {
        [Tooltip("レーンの反転")]
        public bool LaneCahge;

        [Tooltip("オートモード")]
        public bool AuotPlay;

        //[Tooltip("オートモード")]
        //public bool AuotPlay;
    }

}
