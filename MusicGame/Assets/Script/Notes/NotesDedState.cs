using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Notes {

    public class NotesDedState : StateBase<NotesObject,NotesTrigger>
    {
        private const float CangeState = -100.0f;

        public NotesDedState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {
            Debug.Log(owner.score.GetTotalScore().ToString());
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (owner.transform.position.y <= CangeState)
                GameObject.Destroy(owner.gameObject);
        }

        protected override void OnExit()
        {
            
        }
    }
}
