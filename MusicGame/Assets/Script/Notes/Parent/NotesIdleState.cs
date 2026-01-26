using Player;
using UnityEngine;

namespace Notes
{
    public abstract class NotesIdleState : StateBase<NotesObject, NotesTrigger>
    {
        private bool isActionBind;

        public NotesIdleState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {
            isActionBind = true;
        }

        protected override void OnEnter()
        {

        }

        protected override void OnUpdate(float deltaTime)
        {
            // ノーツのイベントをplayerにバインド
            if (InGamePlayer.NotesAction[(int)owner.DebugInfo.NotesLane] == null && isActionBind)
            {
                InGamePlayer.NotesAction[(int)owner.DebugInfo.NotesLane] = ActiveNotes;
                isActionBind = false;
            }
        }

        protected override void OnExit()
        {
            InGamePlayer.NotesAction[(int)owner.DebugInfo.NotesLane] = null;
        }

        // 発火イベント
        protected abstract void ActiveNotes(PlayerState state, float ActiveTime);
    }
}
