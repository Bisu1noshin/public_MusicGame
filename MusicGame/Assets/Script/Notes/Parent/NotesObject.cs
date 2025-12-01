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

    /// <summary>
    /// ノーツの左右を管理する
    /// </summary>
    public enum NotesSide {

        None,
        Left,
        Right,
    }

    public abstract class NotesObject : MonoBehaviour {

        // メンバー変数

        public float ActiveTime { get; protected set; }
        
        public NotesScoreData score { get; protected set; }
        
        public PlayerState AnsTrigger { get; protected set; }

        public float BPM { get; protected set; }

        public float timeCnt { get; private set; }

        public int Max_holdCnt { get; protected set; }


        private const float fallSpeed = 8.0f;

        private const float perfectLenge = 0.033f;
        private const float goodLenge = 0.05f;

        protected NotesSide side;
        protected StateMachine<NotesState, NotesTrigger> st;

        // イベントアクションのデリゲート
        public Func<PlayerState, NotesObject> NotesActoin;

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
                //NotesActoin?.Invoke(AnsTrigger);
                s.color = Color.blue;
            }

            // 時間を加算
            timeCnt += Time.fixedDeltaTime;
        }

        public abstract void SetInitilizeNotes(Notes n_,int bpm);
    }
}