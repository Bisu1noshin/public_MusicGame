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

        

        protected void Start() { }
        protected void Update() { }
    }
}