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

    public abstract class NotesParent : MonoBehaviour {

        // メンバー変数

        public float ActiveTime { get; protected set; }

        protected StateMachine<NotesState, NotesTrigger> st;

        private const float fallSpeed = 5.0f;

        protected void Awake()
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

        protected void Start() { }

        protected void Update() {

            // ステートマシンの更新
            st.Update(Time.deltaTime);

            // 落下処理
            transform.position -= new Vector3(0, -1 * fallSpeed * Time.deltaTime, 0);
        }
    }
}