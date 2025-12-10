using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes
{
    public class HoldIdleState : NotesIdleState
    {
        public HoldIdleState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
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
            base.OnUpdate(deltaTime);

            // 判定の時間外処理に遷移
            if (owner.timeCnt >= owner.Judg.CreateTimeDelay + owner.Judg.GoodTime)
            {
                stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
            }
        }

        protected override void ActiveNotes(PlayerState state, float ActiveTime)
        {
            float DiscriminationTime = ActiveTime - owner.CreateTime;
            InGamePlayer.NotesAction[(int)owner.lane] -= ActiveNotes;

            if (state == owner.AnsTrigger)
            {
                // perfectの処理
                if (DiscriminationTime <= owner.Judg.CreateTimeDelay + owner.Judg.PerfectTime
                && DiscriminationTime >= owner.Judg.CreateTimeDelay - owner.Judg.PerfectTime)
                {
                    owner.score.SetScore(NotesScore.Perfect, owner.holdCnt);
                    stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
                    return;
                }

                // goodの処理  
                if (DiscriminationTime <= owner.Judg.CreateTimeDelay + owner.Judg.GoodTime
                    && DiscriminationTime >= owner.Judg.CreateTimeDelay - owner.Judg.GoodTime)
                {
                    owner.score.SetScore(NotesScore.Good, owner.holdCnt);
                    stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
                    return;
                }
            }
        }
    }
}
