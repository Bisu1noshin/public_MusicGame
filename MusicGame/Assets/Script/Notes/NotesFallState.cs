using UnityEngine;
namespace Notes
{

    public class NotesFallState : StateBase<NotesParent, NotesTrigger>
    {
        private const float CangeState = -2.0f;

        public NotesFallState(NotesParent owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {

        }

        protected override void OnUpdate(float deltaTime)
        {
            if(owner.transform.position.y <= CangeState)
                stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
        }

        protected override void OnExit()
        {

        }
    }
}
