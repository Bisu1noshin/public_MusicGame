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
        
        public PlayerState AnsTrigger { get; protected set; }

        public float BPM { get; protected set; }

        public float timeCnt { get; protected set; }

        public float CreateTime { get; protected set; }

        public int Max_holdCnt { get; protected set; }


        private const float fallSpeed = 8.0f;

        protected StateMachine<NotesState, NotesTrigger> st;

        // イベントアクションのデリゲート
        public Action<PlayerState, float> NotesActoin;

        protected void Awake()
        {
            
        }

        protected virtual void Start() {

            
        }


        protected virtual void FixedUpdate()
        {
            // ステートマシンの更新
            st.Update(Time.fixedDeltaTime);

            // 落下処理
            transform.position += new Vector3(0, -1 * fallSpeed * Time.fixedDeltaTime, 0);

            // パーフェクト
            if (timeCnt >= 1f)
            {
                var s = GetComponentInChildren<SpriteRenderer>();
                //NotesActoin?.Invoke(AnsTrigger, 1f + CreateTime);
                s.color = Color.blue;
            }

            timeCnt += Time.fixedDeltaTime;
        }

        public abstract void SetInitilizeNotes(NotesInformaiton informaiton);
    }
}