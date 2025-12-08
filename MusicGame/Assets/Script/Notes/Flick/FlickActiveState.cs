using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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

            if (timeCnt >= 0.3f) 
                stateMachine.ExecuteTriggerAction(NotesTrigger.DedTrigger);
        }

        protected override void OnExit()
        {
            base.OnExit();
        }

        private void ChangeColor(int index)
        {
            var s = owner.GetComponentInChildren<SpriteRenderer>();

            Color[] color =
            {
                Color.black,
                Color.yellow,
                Color.green,
            };

            s.color = color[index];
        }
    }
}
