using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Notes
{
    public class HoldActiveState:NotesActiveState
    {
        public HoldActiveState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {
            base.OnEnter();
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (owner.holdCnt >= owner.Max_holdCnt - 1)
            {
                stateMachine.ExecuteTriggerAction(NotesTrigger.DedTrigger);
                return;
            }

            stateMachine.ExecuteTriggerAction(NotesTrigger.HoldTrigger);
        }

        protected override void OnExit()
        {
            base.OnExit();

            owner.holdCnt++;
        }
    }
}
