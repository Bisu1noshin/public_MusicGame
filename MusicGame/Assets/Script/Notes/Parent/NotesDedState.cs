using UnityEngine;

namespace Notes {

    public abstract class NotesDedState : StateBase<NotesObject,NotesTrigger>
    {
        public NotesDedState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {
            owner.score.DebugLogScore();
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
