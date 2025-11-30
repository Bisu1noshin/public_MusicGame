using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;

namespace Notes
{
    public class FlickActiveState : NotesActiveState
    {
        private float timeCnt; 

        public FlickActiveState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {
            base.OnEnter();

            timeCnt = 0f;
        }

        protected override void OnUpdate(float deltaTime)
        {
            timeCnt += deltaTime;

            if (timeCnt >= 1f) 
                stateMachine.ExecuteTriggerAction(NotesTrigger.DedTrigger);
        }

        protected override void OnExit()
        {
            base.OnExit();
        }
    }
}
