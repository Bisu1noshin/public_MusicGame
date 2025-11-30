using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class HoldIdleState : NotesIdleState
    {
        private const float perfectLenge = 0.033f;
        private const float goodLenge = 0.05f;

        private const float perfectTime = 1.0f;

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
            // 判定の時間外処理に遷移
            if (owner.timeCnt >= perfectTime + goodLenge)
            {
                stateMachine.ExecuteTriggerAction(NotesTrigger.HoldTrigger);
            }
        }

        protected override void ActiveNotes(PlayerState state)
        {
            if (state == Player.PlayerState.Idle) { return; }

            if (state == owner.AnsTrigger)
            {
                // perfectの処理
                if (owner.timeCnt <= perfectTime + perfectLenge && owner.timeCnt >= perfectTime - perfectLenge)
                {
                    owner.score.SetScore(NotesScore.Perfect, 0);
                    stateMachine.ExecuteTriggerAction(NotesTrigger.HoldTrigger);
                    return;
                }

                // goodの処理
                if (owner.timeCnt <= perfectTime + goodLenge && owner.timeCnt >= perfectTime - goodLenge)
                {
                    owner.score.SetScore(NotesScore.Good, 0);
                    stateMachine.ExecuteTriggerAction(NotesTrigger.HoldTrigger);
                    return;
                }
            }

            if (owner.timeCnt >= perfectTime + goodLenge)
                stateMachine.ExecuteTriggerAction(NotesTrigger.HoldTrigger);
        }
    }
}
