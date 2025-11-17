using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Notes {

    // ノーツの状態を表す列挙型
    public enum NotesState {

        None,
        Fall,
        Active,
        Ded
    };

    // ステートを変更するトリガー
    public enum NotesTrigger {

        FallTrigger,
        ActiveTrigger,
        DedTrigger,
    };

    /// <summary>
    /// ノーツの左右を管理する
    /// </summary>
    public enum NotesSide {

        None,
        Left,
        Right,
    }

    public abstract class NotesParent : MonoBehaviour {

        // メンバー変数

        public float ActiveTime { get; protected set; }

        protected StateMachine<NotesState, NotesTrigger> st;
        public NotesScoreData score;
        protected NotesSide side;
        protected PlayerState AnsTrigger;

        private const float fallSpeed = 4.0f;

        protected const float perfectTime = 2.0f;
        protected float BPM;

        protected float timeCnt;

        protected void Awake()
        {
            // ステートマシンの初期化
            {
                st = new StateMachine<NotesState, NotesTrigger>(NotesState.Fall);

                // デリゲートの登録

                st.SetupState(NotesState.Fall, new NotesFallState(this, st));
                st.SetupState(NotesState.Active, new NotesActiveState(this, st));
                st.SetupState(NotesState.Ded, new NotesDedState(this, st));

                // 遷移条件の登録

                st.AddTransition(NotesState.Fall, NotesState.Active, NotesTrigger.ActiveTrigger);
                st.AddTransition(NotesState.Fall, NotesState.Ded, NotesTrigger.DedTrigger);
                st.AddTransition(NotesState.Active, NotesState.Ded, NotesTrigger.DedTrigger);
            }

            Initialize();
        }

        protected virtual void Start() {

            
        }

        protected virtual void Update()
        {

            // ステートマシンの更新
            st.Update(Time.deltaTime);

            // 落下処理
            transform.position += new Vector3(0, -1 * fallSpeed * Time.deltaTime, 0);

            // 時間を加算
            timeCnt += Time.deltaTime;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<InGamePlayer>(out InGamePlayer p_))
            {
                
            }
        }

        public abstract void ActiveNotes(PlayerState state);
        protected abstract void Initialize();
    }
}