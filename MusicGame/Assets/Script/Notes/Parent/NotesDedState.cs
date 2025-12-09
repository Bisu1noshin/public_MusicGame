using GameInfo;
using UnityEngine;

namespace Notes {

    public abstract class NotesDedState : StateBase<NotesObject,NotesTrigger>
    {
        public NotesDedState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {
            int side = 1;
            if (owner.transform.position.x < 0) { side = 0; }
            owner.score.DebugLogScore(owner.NotesNum, side);
            SingletonDataManager.instance.SetScore(owner.score.GetTotalScore());
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
