using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using LoadForAsync;
using Cysharp.Threading.Tasks;
using System;
using Unity.VisualScripting;

namespace Notes {

    public class NotesManager : MonoBehaviour, ISetAsyncObjects
    {
        private Vector3[] NotesPosition = new Vector3[2];
        [SerializeField] public NotesManagerDatabase NotesManagerData;

        public float InGameTime = default;
        public Action ReleaseAll { get; set; }

        [SerializeField] private AudioSource audioSource;

        private NotesData notesData;
        [SerializeField] private int BPM = 158;
        private int[] createIndex;
        private int[] createIndex_max;
        private bool isMusic = true;
        private bool goinstance = false;

        private string n_path = "TextData/NotesData/ShiningStar/ShiningStar_NORMAL";
        private List<string> n_paths;
        private string m_path = "Music/ShiningStar";

        private readonly string[] NotesDir = new string[4]{
            "_Up",
            "_Right",
            "_Down",
            "_Left"
        };
        private readonly string[] NotesPath = new string[2]{
            "FlickNotes",
            "HoldNotes",
        };

        private const string FlicNotesPath = "Notes/Flick/FlickNotes";
        private const string HoldNotesPath = "Notes/Hold/HoldNotes";
        private const string RushNotesPath = "Notes/RushNotes";

        // ノーツのプレファブ
        private GameObject[,] notes;

        private void Awake()
        {
            // 変数の初期化
            {
                createIndex = new int[2];
                createIndex_max = new int[2];
                notesData = new NotesData();
                notes = new GameObject[2,4];

                for (int i = 0; i < NotesPosition.Length; i++)
                { 
                    int value = -1 + i * 2;
                    if (NotesManagerData.PlayerConfig.LaneCahge) value *= -1;
                    if (NotesManagerData.PlayerConfig.NotesSpeed == 0)
                    {
                        throw new Exception("0はできません");
                    }

                    var indexY = 16f * NotesManagerData.PlayerConfig.NotesSpeed;

                    NotesPosition[i] = new Vector3(2.0f * value, indexY - 3f, 0f);
                }
                
                InGameTime = 0;

            }

            // スクリプタルオブジェクトから読み込み
            if (NotesManagerData != null)
            {
                n_paths = NotesManagerData.fData.NotesDataFilePath;
                m_path = NotesManagerData.fData.MusicFilePath;
            }

            // static変数の処理
            {
                InGameTime = new();
                InGameTime = 0;
            }

            TextEditor.TextEditor text = new(m_path, n_path);

            // 曲の再生速度の変更
            audioSource.pitch = NotesManagerData.nData.MusicSpeed;

            // ノーツの最大値の定義
            for (int i = 0; i < createIndex_max.Length; i++)
            {
                createIndex_max[i] = notesData.notes[i].Count;
            }
        }

        private void Start()
        {
            // リソースの読み込み
            {
                if (audioSource.resource == null)
                {
#if UNITY_EDITOR
                    // ノーツオブジェクトの読み込み
                    for (int i = 0; i < 4; i++)
                    {
                        notes[0, i] = Resources.Load<GameObject>(FlicNotesPath + NotesDir[i]);
                        notes[1, i] = Resources.Load<GameObject>(HoldNotesPath + NotesDir[i]);
                    }

                    // 生成用にデータを編集
                    List<NotesData> notesDatas = new List<NotesData>();
                    foreach (var path in n_paths)
                    {
                        TextEditor.TextEditor textEditor = new(m_path, path);
                        NotesData data = textEditor.NotesReadTxt();
                        data = NotesDataConversion.NotesDataReSize(data);
                        notesDatas.Add(data);
                    }

                    notesData = NotesDataConversion.NotesDataSum(notesDatas);

                    // 楽曲選択
                    {
                        audioSource.resource = Resources.Load<AudioResource>(m_path);
                    }
#endif
                }
            }
        }

        private void OnDisable()
        {
            // メモリの解放
            ReleaseAll?.Invoke();
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
                if (createIndex[i] < notesData.notes[i].Count)
                {
                    Notes n = notesData.notes[i][createIndex[i]];
                    createIndex[i] += CreateNotes(n, i, createIndex[i]);
                }
            }

            //if (!goinstance) { goinstance = DebugNotesCreate(); }

            // 時間の加算
            InGameTime += Time.fixedDeltaTime * NotesManagerData.nData.MusicSpeed;
        }

        private int CreateNotes(Notes n_, int lane,int index)
        {
            float hakuTime = 60.0f / (float)notesData.BPM;
            float CreateTime = hakuTime * n_.time;

            // ノーツの方向指定の変更
            var _notes = n_;
            var NotesLane = (NotesLane)lane;

            // プレイヤーコンフィグに合わせてリサイズ
            {
                if (NotesManagerData.PlayerConfig.UpDownCahge)
                {
                    Direction direction = n_.dir;
                    if (n_.dir == Direction.Top) { direction = Direction.Down; }
                    if (n_.dir == Direction.Down) { direction = Direction.Top; }
                    _notes = new(n_.time, (int)direction, (int)n_.kind, n_.range);
                }

                if (NotesManagerData.PlayerConfig.LeftRightCahge)
                {
                    Direction direction = n_.dir;
                    if (n_.dir == Direction.Left) { direction = Direction.Right; }
                    if (n_.dir == Direction.Right) { direction = Direction.Left; }
                    _notes = new(n_.time, (int)direction, (int)n_.kind, n_.range);
                }

                if (NotesManagerData.PlayerConfig.LaneCahge)
                {
                    if (NotesLane == NotesLane.Left) NotesLane = NotesLane.Right;
                    if (NotesLane == NotesLane.Right) NotesLane = NotesLane.Left;
                }
            }

            // ラッシュが出た時の制御文
            var debugInfo = new NotesDebugInfo(index + 1, (NotesLane)lane);
            var notesKind = _notes.kind;
            var notesRange = (int)_notes.dir;
            if (notesKind == NotesKind.Rush) notesKind = 0;
            if (notesRange < 0) notesRange = 0;

            // ノーツ生成に必要なデータの構築
            var BPMInfo = new BPMInfo(m_bpm: BPM, n_bpm: (int)notesData.BPM);
            var instantInfo = new NotesInstantInfo(notes[(int)notesKind, notesRange], NotesPosition[(int)NotesLane]);
            var _NotesManagerData = NotesManagerData.nData;
            _NotesManagerData.AutoPlay = NotesManagerData.PlayerConfig.AutoPlay;

            NotesInformaiton informaiton = new(
                create: CreateTime,
                speed: NotesManagerData.PlayerConfig.NotesSpeed,
                n: _notes,
                bpm: BPMInfo,
                instantInfo: instantInfo,
                j: _NotesManagerData,
                debugInfo: debugInfo
                );

            // 生成時間になったら生成してカウントを増やす
            if (CreateTime <= InGameTime)
            {
                if (n_.kind != NotesKind.Rush)
                {
                    NotesGenerator.CreateNotes(informaiton);
                }
                
                return 1;
            }

            return 0;
        }

        private bool DebugMusic()
        {
            if (InGameTime >= NotesManagerData.nData.CreateTimeDelay- Time.fixedDeltaTime)
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
            BPMInfo BPMInfo = new(m_bpm: BPM, n_bpm: (int)notesData.BPM);
            NotesInstantInfo instantInfo = new(notes[(int)notes_.kind, (int)notes_.dir], NotesPosition[0]);
            NotesDebugInfo debugInfo = new(0, (NotesLane)0);
            
            NotesInformaiton informaiton = new(
                create: 0,
                speed: NotesManagerData.PlayerConfig.NotesSpeed,
                n: notes_,
                bpm: BPMInfo,
                instantInfo: instantInfo,
                j: NotesManagerData.nData,
                debugInfo: debugInfo
                );

            GameObject go = NotesGenerator.CreateNotes(informaiton);

            return true;
        }

        public void SetAsyncObjects(LoadObjectTable ObjectTable)
        {
            notes = new GameObject[2, 4];

            // アセットの読み込み
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    notes[i, j] = ObjectTable.GetAsset<GameObject>(NotesPath[i] + NotesDir[j]);
                }
            }

            audioSource.resource = ObjectTable.GetAsset<AudioResource>("Music");

            // 生成用にデータを編集
            List<NotesData> notesDatas = new List<NotesData>();
            int index = 1;

            var textAsset = new List<TextAsset>();
            while (true)
            {
                var asset = ObjectTable.GetAsset<TextAsset>("TextAsset_" + index.ToString());
                if (asset == null) break;

                textAsset.Add(asset);
                index++;
            }

            foreach (var path in textAsset)
            {
                TextEditor.TextEditor textEditor = new(m_path, path);
                NotesData data = textEditor.NotesReadTxt();
                data = NotesDataConversion.NotesDataReSize(data);
                notesDatas.Add(data);
            }

            notesData = NotesDataConversion.NotesDataSum(notesDatas);

            Debug.Log("SuccessAsync");
        }
    }
}

