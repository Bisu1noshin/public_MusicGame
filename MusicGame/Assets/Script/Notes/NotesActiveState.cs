using UnityEngine;
namespace Notes
{

    public class NotesActiveState : StateBase<NotesParent, NotesTrigger>
    {
        public NotesActiveState(NotesParent owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {

        }

        protected override void OnUpdate(float deltaTime)
        {

        }

        protected override void OnExit()
        {

        }
    }
}
