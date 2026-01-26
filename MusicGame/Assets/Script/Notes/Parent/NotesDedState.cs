using GameInfo;
using OnLine;
using UnityEngine;

namespace Notes {

    public abstract class NotesDedState : StateBase<NotesObject,NotesTrigger>
    {
        public NotesDedState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {
            owner.score.DebugLogScore(owner.DebugInfo.NotesNum, owner.DebugInfo.NotesLane);
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
