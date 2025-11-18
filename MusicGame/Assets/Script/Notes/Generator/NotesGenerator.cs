using UnityEngine;

namespace Notes {

    public class NotesGenerator : MonoBehaviour
    {
        private NotesData notesData;
        private float timeCnt;

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
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        private void CreateNotes(float time,NotesParent notes) {

            if (time >= timeCnt) {


            }
        }
    }
}
