using UnityEngine;
namespace Notes
{

    public class NotesActiveState : StateBase<NotesParent, NotesTrigger>
    {
        private const float dedPos = -4.0f;

        public NotesActiveState(NotesParent owner, IStateMachine<NotesTrigger> st) : base(owner, st)
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
