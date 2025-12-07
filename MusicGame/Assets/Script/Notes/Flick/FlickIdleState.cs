using Player;
using System;
using UnityEngine;

namespace Notes
{
    public class FlickIdleState : NotesIdleState
    {
        private const float perfectLenge = 0.04f;
        private const float goodLenge = 0.08f;
        private const float perfectTime = 1.0f;

        public FlickIdleState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {
            base.OnEnter();
        }

        protected override void OnExit()
        {
            base.OnExit();
        }

        protected override void OnUpdate(float deltaTime)
        {
            // 判定の時間外処理に遷移
            if (owner.timeCnt >= perfectTime + goodLenge)
            {
                stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
            }
        }

        protected override void ActiveNotes(PlayerState state, float ActiveTime)
        {
            float DiscriminationTime = ActiveTime - owner.CreateTime;
            owner.NotesActoin -= ActiveNotes;

            if (state == owner.AnsTrigger)
            {
                // perfectの処理
                if (DiscriminationTime <= perfectTime + perfectLenge && DiscriminationTime >= perfectTime - perfectLenge)
                {
                    owner.score.SetScore(NotesScore.Perfect);
                    stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
                    return;
                }

                // goodの処理
                if (DiscriminationTime <= perfectTime + goodLenge && DiscriminationTime >= perfectTime - goodLenge)
                {
                    owner.score.SetScore(NotesScore.Good);
                    stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
                    return;
                }
            }
        }
    }
}
