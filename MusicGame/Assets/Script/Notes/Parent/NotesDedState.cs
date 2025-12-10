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
            owner.score.DebugLogScore(owner.NotesNum, owner.lane);
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
