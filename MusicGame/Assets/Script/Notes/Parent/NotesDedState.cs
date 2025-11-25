using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Notes {

    public abstract class NotesDedState : StateBase<NotesObject,NotesTrigger>
    {
        public NotesDedState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {
            Debug.Log(owner.score.GetTotalScore().ToString());
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
