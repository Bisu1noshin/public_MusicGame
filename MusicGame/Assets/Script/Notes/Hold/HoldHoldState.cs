using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Notes
{
    public class HoldHoldState : NotesHoldState
    {
        private float isHoldTime;
        private float timeCnt;
        private int holdCnt;
        private readonly float clapTime;

        public HoldHoldState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {
            // 変数の初期化
            isHoldTime = 0f;
            clapTime = 60.0f / (float)owner.BPM;
            holdCnt = 1;
        }

        protected override void OnEnter()
        {
            base.OnEnter();

            isHoldTime = 0f;
            timeCnt = 0f;
        }

        protected override void OnUpdate(float deltaTime)
        {
            // カウントが最大値になったら破壊する
            if (holdCnt == owner.Max_holdCnt)
            {
                owner.NotesActoin -= ActiveNotes;
                stateMachine.ExecuteTriggerAction(NotesTrigger.DedTrigger);
                return;
            }

            // 一拍ホールドされていたらスコアを増やす
            if (clapTime <= isHoldTime)
            {
                owner.score.SetScore(NotesScore.Perfect, holdCnt);
                holdCnt++;
                stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
                return;
            }

            float clap = timeCnt - (holdCnt - 1) * clapTime;

            // 一拍たったら遷移
            if (clapTime <= clap)
            {
                owner.score.SetScore(NotesScore.Miss, holdCnt);
                holdCnt++;
                stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
                return;
            }

            timeCnt += deltaTime;
        }

        protected override void OnExit()
        {
            base.OnExit();
        }

        // 発火イベント
        protected override void ActiveNotes(PlayerState state, float ActiveTime)
        {
            if(state == owner.AnsTrigger)
                isHoldTime += Time.fixedDeltaTime;
        }
    }
}
