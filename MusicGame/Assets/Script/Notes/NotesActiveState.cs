using UnityEngine;
namespace Notes
{

    public class NotesActiveState : StateBase<NotesObject, NotesTrigger>
    {
        private const float dedPos = -100.0f;

        public NotesActiveState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {

        }

        protected override void OnUpdate(float deltaTime)
        {
            if (owner.transform.position.y <= dedPos)
                stateMachine.ExecuteTriggerAction(NotesTrigger.DedTrigger);
        }

        protected override void OnExit()
        {

        }
    }
}
