using Player;
using System;
using UnityEngine;

namespace Notes {

    // ノーツの状態を表す列挙型
    public enum NotesState {

        None,
        Idle,   // 基本の状態、この時入力を受け付ける
        Hold,   // 入力後に呼ばれる、Flickノーツは使わない
        Active, // 判定後に向かう処理、エフェクト等々
        Ded　   // 破壊されるまでの間の処理を行う、スコアの加算等々
    };

    // ステートを変更するトリガー
    public enum NotesTrigger {

        None,
        FallTrigger,
        HoldTrigger,
        ActiveTrigger,
        DedTrigger,
    };

    public abstract class NotesObject : MonoBehaviour {

        // メンバー変数
        
        public NotesScoreData score { get; protected set; }

        public float CreateTime { get; protected set; }
        
        public PlayerState AnsTrigger { get; protected set; }

        public float timeCnt { get; protected set; }

        public int Max_holdCnt { get; protected set; }

        public NotesManagerForNotesData Judg { get; protected set; }

        public BPMInfo BPMInfo { get; protected set; }

        public NotesDebugInfo DebugInfo { get; protected set; }

        public int holdCnt;

        private float fallSpeed;
        protected StateMachine<NotesState, NotesTrigger> st;

        protected void Awake()
        {
            
        }

        protected virtual void Start() {

            fallSpeed = (transform.position.y + 3f) / Judg.CreateTimeDelay;
            holdCnt = 0;

            if (Judg.AutoPlay) { Judg.JudgmentTimeDelay = 0; }
        }


        protected virtual void FixedUpdate()
        {
            // 落下処理
            float speed = -1 * fallSpeed * Time.fixedDeltaTime * Judg.MusicSpeed;
            transform.position += new Vector3(0, speed, speed * 5.96f / 5f);

            // オートプレイの処理
            if (timeCnt >= Judg.CreateTimeDelay)
            {
                if (Judg.AutoPlay)
                {
                    InGamePlayer.NotesAction[(int)DebugInfo.NotesLane]?.Invoke(AnsTrigger, Judg.CreateTimeDelay + CreateTime);
                }                   
            }

            // ステートマシンの更新
            st.Update(Time.fixedDeltaTime * Judg.MusicSpeed);

            timeCnt += Time.fixedDeltaTime * Judg.MusicSpeed;
        }

        public abstract void SetInitilizeNotes(NotesInformaiton informaiton);
    }
}