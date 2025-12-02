using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace Notes {

    public class NotesManager : MonoBehaviour
    {
        [SerializeField] private Vector3[] NotesPosition = new Vector3[2];

        public float InGameTime = default;

        private Vector3[] rotate = new Vector3[4];
        private AudioSource audioSource;

        private NotesData notesData;
        private int BPM = 158;
        private int[] createIndex;
        private int[] createIndex_max;
        private bool isMusic = true;
        private bool goinstance = false;

        private const string n_path = "TextData/NotesData/ShiningStar/ShiningStar_NORMAL";
        private const string m_path = "Music/ShiningStar";

        private const string FlicNotesPath = "Notes/FlickNotes";
        private const string HoldNotesPath = "Notes/HoldNotes";
        private const string RushNotesPath = "Notes/RushNotes";

        // ノーツのプレファブ
        private GameObject[] notes;

        private void Awake()
        {
            // 変数の初期化
            {
                createIndex = new int[2];
                createIndex_max = new int[2];
                notesData = new NotesData();
                notes = new GameObject[3];

                InGameTime = 0;

                for (int i = 0; i < rotate.Length; i++)
                {
                    rotate[i] = new Vector3(0, 0, 90 * i);
                }
            }

            // ノーツオブジェクトの読み込み
            notes[0] = Resources.Load<GameObject>(FlicNotesPath);
            notes[1] = Resources.Load<GameObject>(HoldNotesPath);
            notes[2] = Resources.Load<GameObject>(RushNotesPath);

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

            // ノーツの召喚
            for (int i = 0; i < createIndex.Length; i++)
            {
                if (createIndex[i] >= createIndex_max[i]) { return; }

                Notes n = notesData.notes[i][createIndex[i]];
                createIndex[i] += CreateNotes(n, i);
            }

            // 時間の加算
            InGameTime += Time.fixedDeltaTime;
        }

        private int CreateNotes(Notes n_, int lane)
        {

            float hakuTime = 60.0f / (float)notesData.BPM;
            float CreateTime = hakuTime * (float)n_.time;

            // ノーツ生成に必要なデータの構築
            NotesInformaiton informaiton = new(
                create: CreateTime,
                obj: notes[(int)n_.kind],
                n: n_,
                bpm: BPM,
                v: NotesPosition[lane],
                q: rotate[(int)n_.dir]
                );

            // 生成時間になったら生成してカウントを増やす
            if (CreateTime <= InGameTime)
            {
                GameObject go = NotesGenerator.CreateNotes(informaiton);

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
            Notes notes_ = new(0, 0, 1, 3);

            // ノーツ生成に必要なデータの構築
            NotesInformaiton informaiton = new(
                create:0,
                obj: notes[(int)notes_.kind],
                n: notes_,
                bpm: BPM,
                v: NotesPosition[0],
                q: rotate[(int)notes_.dir]
                );

            GameObject go = NotesGenerator.CreateNotes(informaiton);

            return true;
        }
    }
}

