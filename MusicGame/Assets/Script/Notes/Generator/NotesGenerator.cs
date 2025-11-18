using UnityEngine;

namespace Notes {

    public class NotesGenerator : MonoBehaviour
    {
        private NotesData notesData;
        private float timeCnt;
        private int createIndex;

        // ノーツのプレファブ
        private GameObject Flick;
        private GameObject Hold;
        private GameObject Rush;

        private void Awake()
        {
            // ノーツの読み込み
            Flick = Resources.Load<GameObject>("");
            Hold = Resources.Load<GameObject>("");
            Rush = Resources.Load<GameObject>("");

            // 変数の初期化
            {
                createIndex = 0;
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
