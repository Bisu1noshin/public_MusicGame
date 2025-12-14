using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using LoadForAsync;
using Cysharp.Threading.Tasks;
using TMPro;
using System;

namespace Notes {

    public class NotesManager : MonoBehaviour
    {
        [SerializeField] private Vector3[] NotesPosition = new Vector3[2];
        [SerializeField] private NotesManagerDatabase NotesManagerData;

        public float InGameTime = default;

        private AudioSource audioSource;

        private NotesData notesData;
        private int BPM = 158;
        private int[] createIndex;
        private int[] createIndex_max;
        private bool isMusic = true;
        private bool goinstance = false;

        private string n_path = "TextData/NotesData/ShiningStar/ShiningStar_NORMAL";
        private string m_path = "Music/ShiningStar";

        private readonly string[] NotesDir = new string[4]{
            "_Up",
            "_Right",
            "_Down",
            "_Left"
        };

        private const string FlicNotesPath = "Notes/Flick/FlickNotes";
        private const string HoldNotesPath = "Notes/Hold/HoldNotes";
        private const string RushNotesPath = "Notes/RushNotes";

        // ノーツのプレファブ
        private GameObject[,] notes;

        private async UniTask Awake()
        {
            // 変数の初期化
            {
                createIndex = new int[2];
                createIndex_max = new int[2];
                notesData = new NotesData();
                notes = new GameObject[3,4];

                InGameTime = 0;

            }

            // スクリプタルオブジェクトから読み込み
            if (NotesManagerData != null)
            {
                n_path = NotesManagerData.fData.NotesDataFilePath;
                m_path = NotesManagerData.fData.MusicFilePath;
            }

            // ノーツオブジェクトの読み込み
            for (int i = 0; i < 4; i++)
            {
                notes[0, i] = Resources.Load<GameObject>(FlicNotesPath + NotesDir[i]);
                notes[1, i] = Resources.Load<GameObject>(HoldNotesPath + NotesDir[0]);
                notes[2, i] = Resources.Load<GameObject>(RushNotesPath);
            }
            

            // ノーツの配置データの読み込み
            TextEditor.TextEditor text = new(m_path, n_path);
            notesData = text.NotesReadTxt(); 

            // 生成用にデータを編集
            NotesDataConversion notesDataConversion = new NotesDataConversion(notesData);
            notesData = notesDataConversion.GetData();

            // ノーツの最大値の定義
            for (int i = 0; i < createIndex_max.Length; i++)
            {
                createIndex_max[i] = notesData.notes[i].Count;
            }

            // 楽曲選択
            {
                audioSource = GetComponent<AudioSource>();

                audioSource.resource = Resources.Load<AudioResource>(m_path);

                // 曲の再生速度の変更
                audioSource.pitch = NotesManagerData.nData.MusicSpeed;

                //AudioResource audio = await LoadObjectForAsync.AsyncLoad<AudioResource>(m_path);
                //if (audio != null) { Debug.Log("ロードできました"); }
                //audioSource.resource = audio;
            }

            // static変数の処理
            {
                InGameTime = new();
                InGameTime = 0;
            }
        }

        private void Start()
        {
            // 一章節開けてから再製
            //audioSource.Play();
        }

        private void FixedUpdate()
        {
            // 仮音楽再生部分
            {
                if (isMusic)
                    isMusic = DebugMusic();
            }

            // 曲の終わりを認識
            if (!audioSource.isPlaying && !isMusic)
            {
                SceneManager.LoadScene("Onishi_Result");
                Debug.Log("曲が終了しました");
            }

            // ノーツの召喚
            for (int i = 0; i < createIndex.Length; i++)
            {
                if (createIndex[i] >= createIndex_max[i]) { return; }

                Notes n = notesData.notes[i][createIndex[i]];
                createIndex[i] += CreateNotes(n, i, createIndex[i]);
            }

            //if (!goinstance) { goinstance = DebugNotesCreate(); }

            // 時間の加算
            InGameTime += Time.fixedDeltaTime * NotesManagerData.nData.MusicSpeed;
        }

        private int CreateNotes(Notes n_, int lane,int index)
        {
            float hakuTime = 60.0f / (float)notesData.BPM;
            float CreateTime = hakuTime * (float)n_.time;

            // ノーツ生成に必要なデータの構築
            BPMInfo BPMInfo = new(m_bpm: BPM, n_bpm: notesData.BPM);
            NotesInstantInfo instantInfo = new(notes[(int)n_.kind, (int)n_.dir], NotesPosition[lane]);
            NotesDebugInfo debugInfo = new(index + 1, (NotesLane)lane);

            NotesInformaiton informaiton = new(
                create: CreateTime,
                n: n_,
                bpm: BPMInfo,
                instantInfo: instantInfo,
                j: NotesManagerData.nData,
                debugInfo: debugInfo
                );

            // 生成時間になったら生成してカウントを増やす
            if (CreateTime <= InGameTime)
            {
                if (n_.kind != NotesKind.Rush)
                {
                    GameObject go = NotesGenerator.CreateNotes(informaiton);
                }
                
                return 1;
            }

            return 0;
        }

        private bool DebugMusic()
        {
            if (InGameTime >= 1f)
            {
                audioSource.Play();
                return false;
            }

            return true;
        }

        private bool DebugNotesCreate()
        {
            Notes notes_ = new(0, 0, 2, 3);

            // ノーツ生成に必要なデータの構築
            BPMInfo BPMInfo = new(m_bpm: BPM, n_bpm: notesData.BPM);
            NotesInstantInfo instantInfo = new(notes[(int)notes_.kind, (int)notes_.dir], NotesPosition[0]);
            NotesDebugInfo debugInfo = new(0, (NotesLane)0);
            
            NotesInformaiton informaiton = new(
                create: 0,
                n: notes_,
                bpm: BPMInfo,
                instantInfo: instantInfo,
                j: NotesManagerData.nData,
                debugInfo: debugInfo
                );

            GameObject go = NotesGenerator.CreateNotes(informaiton);

            return true;
        }
    }
}

