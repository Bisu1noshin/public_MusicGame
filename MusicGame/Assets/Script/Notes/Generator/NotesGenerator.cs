using UnityEngine;
using UnityEngine.Rendering;

namespace Notes {

    public class NotesGenerator : MonoBehaviour
    {
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
            }

            // ノーツオブジェクトの読み込み
            notes[0] = Resources.Load<GameObject>(FlicNotesPath);
            notes[1] = Resources.Load<GameObject>(HoldNotesPath);
            notes[2] = Resources.Load<GameObject>(RushNotesPath);

            // ノーツの配置データの読み込み
            TextEditor.TextEditor text = new(m_path, n_path);
            notesData = text.NotesReadTxt();

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
                createIndex[i] += CreateNotes(n);
            }
            
        }

        private int CreateNotes(Notes n_) {

            float hakuTime = 60.0f / (float)notesData.BPM;
            float CreateTime = hakuTime * (float)n_.time;

            if (CreateTime <= timeCnt) {

                GameObject go = notes[(int)n_.kind];
                Instantiate(go);

                Debug.Log(CreateTime.ToString());

                return 1;
            }

            return 0;
        }
    }
}
