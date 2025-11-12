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
    /// スコアを表すための列挙型
    /// </summary>
    public enum NotesScore {

        None,
        Good,
        Perfect,
        Miss
    }

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
        public NotesScore score;
        protected NotesSide side;
        protected PlayerState AnsTrigger;

        private const float fallSpeed = 5.0f;

        protected const float perfectPos = -3.0f;

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

            // 変数の初期化
            {
                score = NotesScore.Miss;
                side = NotesSide.Left ;
                AnsTrigger=PlayerState.Left ;
            }
        }

        protected virtual void Start() { }

        protected virtual void Update() {

            // ステートマシンの更新
            st.Update(Time.deltaTime);

            // 落下処理
            transform.position += new Vector3(0, -1 * fallSpeed * Time.deltaTime, 0);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<InGamePlayer>(out InGamePlayer p_))
            {

                PlayerState state;
                if (side == NotesSide.Left) { state = p_.LeftState; }
                else { state = p_.RightState; }

                ActiveNotes(state);
            }
        }

        protected abstract void ActiveNotes(PlayerState state);
    }
}