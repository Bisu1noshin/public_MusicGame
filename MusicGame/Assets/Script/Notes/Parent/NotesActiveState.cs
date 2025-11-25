using UnityEngine;
namespace Notes
{

    public abstract class NotesActiveState : StateBase<NotesObject, NotesTrigger>
    {

        public NotesActiveState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {

        }

        protected override void OnUpdate(float deltaTime)
        {
            // pass
        }

        protected override void OnExit()
        {

        }
    }
}
