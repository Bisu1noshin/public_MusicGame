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
        private readonly float clapTime;

        public HoldHoldState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {
            // 変数の初期化
            isHoldTime = 0f;
            clapTime = 60.0f / (float)owner.BPM - Time.fixedDeltaTime;
        }

        protected override void OnEnter()
        {
            base.OnEnter();

            isHoldTime = 0f;
            timeCnt = 0f;
        }

        protected override void OnUpdate(float deltaTime)
        {
            // 一拍ホールドされていたらスコアを増やす
            if (clapTime <= isHoldTime)
            {
                owner.score.SetScore(NotesScore.Perfect, owner.holdCnt);
                stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
                return;
            }

            float clap = timeCnt - (owner.holdCnt - 1) * clapTime;

            // 一拍たったら遷移
            if (clapTime <= clap)
            {
                owner.score.SetScore(NotesScore.Miss, owner.holdCnt);
                stateMachine.ExecuteTriggerAction(NotesTrigger.ActiveTrigger);
                return;
            }

            timeCnt += deltaTime;
        }

        protected override void OnExit()
        {
            base.OnExit();

            if (owner.holdCnt >= owner.Max_holdCnt - 1)
                owner.NotesActoin -= ActiveNotes;
        }

        // 発火イベント
        protected override void ActiveNotes(PlayerState state, float ActiveTime)
        {
            if(state == owner.AnsTrigger)
                isHoldTime += Time.fixedDeltaTime;
        }
    }
}
