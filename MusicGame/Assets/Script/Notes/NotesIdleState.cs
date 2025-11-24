using Player;
using UnityEngine;
namespace Notes
{

    public abstract class NotesIdleState : StateBase<NotesObject, NotesTrigger>
    {
        private const float CangeState = -2.0f;

        public NotesIdleState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {
            owner.NotesActoin += ActiveNotes;
        }

        protected override void OnUpdate(float deltaTime)
        {
            // pass
        }

        protected override void OnExit()
        {
            owner.NotesActoin -= ActiveNotes;
        }

        // 発火イベント
        protected abstract void ActiveNotes(PlayerState state);
    }
}
