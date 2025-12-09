using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes
{
    public class RushIdleNotes : NotesIdleState
    {
        public RushIdleNotes(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
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
            if (owner.timeCnt >= owner.Judg.JudgmentTimeDelay + owner.Judg.GoodTime)
            {
                stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
            }
        }

        protected override void ActiveNotes(PlayerState state, float ActiveTime)
        {
            float DiscriminationTime = ActiveTime - owner.CreateTime;
            owner.NotesActoin -= ActiveNotes;

            // perfectの処理
            if (DiscriminationTime <= owner.Judg.JudgmentTimeDelay + owner.Judg.PerfectTime
                && DiscriminationTime >= owner.Judg.JudgmentTimeDelay - owner.Judg.PerfectTime)
            {
                owner.score.SetScore(NotesScore.Perfect, owner.holdCnt);
                stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
                return;
            }

            // goodの処理  
            if (DiscriminationTime <= owner.Judg.JudgmentTimeDelay + owner.Judg.GoodTime
                && DiscriminationTime >= owner.Judg.JudgmentTimeDelay - owner.Judg.GoodTime)
            {
                owner.score.SetScore(NotesScore.Good, owner.holdCnt);
                stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
                return;
            }
        }
    }
}
