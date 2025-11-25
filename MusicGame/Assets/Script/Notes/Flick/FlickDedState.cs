using Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Notes
{
    public class FlickDedState : NotesDedState
    {
        private const float CangeState = -100.0f;

        public FlickDedState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {
            base.OnEnter();
        }

        protected override void OnUpdate(float deltaTime)
        {

        }

        protected override void OnExit()
        {
            base.OnExit();
        }
    }
}
