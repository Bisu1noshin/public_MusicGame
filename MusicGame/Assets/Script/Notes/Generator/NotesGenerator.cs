using System;
using UnityEngine;

namespace Notes {

    /// <summary>
    /// BPMを保存するクラス
    /// </summary>
    public class BPMInfo{

        /// <summary>
        /// 楽曲のBPM
        /// </summary>
        public int MusicBPM { get; private set; }

        /// <summary>
        /// ノーツデータのBPM
        /// </summary>
        public int NotesBPM { get; private set; }

        /// <summary>
        /// BPMを保存するクラス
        /// </summary>
        /// <param name="m_bpm">楽曲のBPM</param>
        /// <param name="n_bpm">ノーツデータのBPM</param>
        public BPMInfo(int m_bpm,int n_bpm)
        {
            MusicBPM = m_bpm;
            NotesBPM = n_bpm;
        }
    }

    /// <summary>
    /// インスタンスに使う引数を保存するクラス
    /// </summary>
    public class NotesInstantInfo
    {
        /// <summary>
        /// 生成するオブジェクト
        /// </summary>
        public GameObject NotesObj { get; private set; }

        /// <summary>
        /// 生成位置
        /// </summary>
        public Vector3 CreatePos { get; private set; }

        /// <summary>
        /// インスタンスに使う引数を保存するクラス
        /// </summary>
        /// <param name="obj">生成するオブジェクト</param>
        /// <param name="vector">生成位置</param>
        public NotesInstantInfo(GameObject obj,Vector3 vector)
        {
            NotesObj = obj;
            CreatePos = vector;
        }
    }

    /// <summary>
    /// ノーツのログに出力したい情報を保存するクラス
    /// </summary>
    public class NotesDebugInfo
    {
        /// <summary>
        /// 召喚順のノーツの番号
        /// </summary>
        public int NotesNum { get; private set; }

        /// <summary>
        /// ノーツのレーン
        /// </summary>
        public NotesLane NotesLane { get; private set; }

        /// <summary>
        /// ノーツのログに出力したい情報を保存するクラス
        /// </summary>
        /// <param name="num">召喚順のノーツの番号</param>
        /// <param name="lane">ノーツのレーン</param>
        public NotesDebugInfo(int num, NotesLane lane)
        {
            NotesNum = num;
            NotesLane = lane;
        }
    }

    public class NotesInformaiton
    {
        public float CteateTime { get; private set; }

        public NotesInstantInfo InstantInfo { get; private set; }

        public BPMInfo BPMInfo { get; private set; }

        public NotesDebugInfo DebugInfo { get; private set; }

        public Notes Notes { get; private set; }

        public NotesManagerForNotesData Judgment { get; private set; }

        public NotesInformaiton
            (float create, NotesInstantInfo instantInfo,Notes n, BPMInfo bpm, NotesManagerForNotesData j, NotesDebugInfo debugInfo)
        {
            CteateTime = create;
            Notes = n;
            InstantInfo = instantInfo;
            BPMInfo = bpm;
            Judgment = j;
            DebugInfo = debugInfo;
        }
    }

    public static class NotesGenerator 
    {
        public static GameObject CreateNotes
            (NotesInformaiton informaiton)
        {
            Type[] notes ={
                typeof(FlickNotes),
                typeof(HoldNotes),
                typeof(RushNotes)
            };

            Quaternion q = Quaternion.Euler(new Vector3(0, 0, 0));
            GameObject go = GameObject.Instantiate
                (informaiton.InstantInfo.NotesObj, informaiton.InstantInfo.CreatePos, q);

            // スクリプトを動的にアタッチ
            go.AddComponent(notes[(int)informaiton.Notes.kind]);

            // 変数を初期化
            var n_ = go.GetComponent<NotesObject>();
            n_.SetInitilizeNotes(informaiton);

            return go;
        }
    }
}
