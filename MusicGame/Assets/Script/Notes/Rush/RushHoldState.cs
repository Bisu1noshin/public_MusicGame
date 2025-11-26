using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Notes
{
    public class RushHoldState : NotesHoldState
    {
        private float isHoldTime;
        private float timeCnt;
        private int holdCnt;
        private readonly float clapTime;

        public RushHoldState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {
            // 変数の初期化
            {
                isHoldTime = 0f;
                clapTime = 60.0f / (float)owner.BPM;
                holdCnt = 1;
            }
        }

        protected override void OnEnter()
        {
            base.OnEnter();

            isHoldTime = 0f;
        }

        protected override void OnUpdate(float deltaTime)
        {
            this.timeCnt += deltaTime;

            // 一拍ホールドされていたらスコアを増やす
            if (clapTime >= isHoldTime)
            {
                holdCnt++;
                owner.score.SetScore(NotesScore.Perfect, holdCnt);
            }

            // 一拍たったら遷移
            if (clapTime >= this.timeCnt)
            {
                if (holdCnt == owner.Max_holdCnt)
                {
                    stateMachine.ExecuteTriggerAction(NotesTrigger.DedTrigger);
                }

                stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
            }
        }

        protected override void OnExit()
        {
            base.OnExit();
        }

        // 発火イベント
        protected override void ActiveNotes(PlayerState state)
        {
            isHoldTime += Time.deltaTime;
        }
    }
}
