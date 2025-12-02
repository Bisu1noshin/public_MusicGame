using Player;
using UnityEngine;
namespace Notes
{

    public abstract class NotesIdleState : StateBase<NotesObject, NotesTrigger>
    {
        public NotesIdleState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {
            owner.NotesActoin += ActiveNotes;
        }

        protected override void OnEnter()
        {
            owner.NotesActoin += ActiveNotes;
        }

        protected override void OnUpdate(float deltaTime)
        {
            
        }

        protected override void OnExit()
        {
            owner.NotesActoin -= ActiveNotes;
        }

        // 発火イベント
        protected abstract void ActiveNotes(PlayerState state);
    }
}
