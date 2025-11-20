using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Notes {

    public class NotesGenerator : MonoBehaviour
    {
        [SerializeField] private Vector3[] NotesPosition = new Vector3[2];

        private Quaternion[] rotate = new Quaternion[4];

        private NotesData notesData;
        private float timeCnt;
        private int[] createIndex;
        private int[] createIndex_max;

        private const string n_path = "TextData/NotesData/ShiningStar/ShiningStar_NORMAL";
        private const string m_path = "";

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

                timeCnt = 0;

                for (int i = 0; i < rotate.Length; i++)
                {
                    rotate[i] = new Quaternion(0, 0, 90 * i, 0);
                }
            }

            // ノーツオブジェクトの読み込み
            notes[0] = Resources.Load<GameObject>(FlicNotesPath);
            notes[1] = Resources.Load<GameObject>(HoldNotesPath);
            notes[2] = Resources.Load<GameObject>(RushNotesPath);

            // ノーツの配置データの読み込み
            TextEditor.TextEditor text = new(m_path, n_path);
            notesData = text.NotesReadTxt();

            // ノーツの最大値の定義
            for (int i = 0; i < createIndex_max.Length; i++)
            {
                createIndex_max[i] = notesData.notes[i].Count;
            }
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            // 時間の加算
            timeCnt += Time.deltaTime;

            // ノーツの召喚
            for (int i = 0; i < createIndex.Length; i++)
            {
                if (createIndex[i] >= createIndex_max[i]) { return; }

                Notes n = notesData.notes[i][createIndex[i]];
                createIndex[i] += CreateNotes(n, i);
            }
            
        }

        private int CreateNotes(Notes n_,int lane) {

            float hakuTime = 60.0f / (float)notesData.BPM;
            float CreateTime = hakuTime * (float)n_.time;

            // 生成時間になったら生成してカウントを増やす
            if (CreateTime <= timeCnt) {

                GameObject go = notes[(int)n_.kind];
                Instantiate(go, NotesPosition[lane], rotate[(int)n_.dir]);

                return 1;
            }

            return 0;
        }
    }
}
