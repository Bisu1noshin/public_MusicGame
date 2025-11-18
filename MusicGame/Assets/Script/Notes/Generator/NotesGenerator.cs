using UnityEngine;

namespace Notes {

    public class NotesGenerator : MonoBehaviour
    {
        private NotesData[] notesData;
        private float timeCnt;
        private int[] createIndex;
        private const string n_path = "";
        private const string m_path = "";

        // ノーツのプレファブ
        private GameObject[] notes;

        private void Awake()
        {
            // 変数の初期化
            {
                createIndex = new int[2];
                notesData = new NotesData[2];
                notes = new GameObject[3];

                timeCnt = 0;
                
            }

            // ノーツオブジェクトの読み込み
            notes[0] = Resources.Load<GameObject>("");
            notes[1] = Resources.Load<GameObject>("");
            notes[2] = Resources.Load<GameObject>("");

            // ノーツの配置データの読み込み
            TextEditor.TextEditor text = new(m_path, n_path);
            notesData[(int)NotesLane.Left] = text.NotesReadTxt(NotesLane.Left);
            notesData[(int)NotesLane.Right] = text.NotesReadTxt(NotesLane.Right);
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            // 時間の加算
            timeCnt += Time.deltaTime;

            // ノーツの召喚
            //createIndex += CreateNotes();
        }

        private int CreateNotes(float time,NotesParent notes) {

            if (time >= timeCnt) {


                return 1;
            }

            return 0;
        }
    }
}
