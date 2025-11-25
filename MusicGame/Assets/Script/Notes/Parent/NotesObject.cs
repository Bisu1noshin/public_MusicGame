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
        
        public PlayerState AnsTrigger { get; private set; }

        public float BPM { get; private set; }

        public float timeCnt { get; private set; }

        public int Max_holdCnt { get; private set; }


        private const float fallSpeed = 4.0f;

        public const float perfectTime = 2.0f;
        protected NotesSide side;
        protected StateMachine<NotesState, NotesTrigger> st;

        // イベントアクションのデリゲート
        public Action<PlayerState> NotesActoin;

        protected void Awake()
        {
            // ステートマシンの初期化
            {
                st = new StateMachine<NotesState, NotesTrigger>(NotesState.Idle);

                // デリゲートの登録
                // 各子クラスで行う、忘れないように

                // 遷移条件の登録

                st.AddTransition(NotesState.Idle, NotesState.Active, NotesTrigger.ActiveTrigger);
                st.AddTransition(NotesState.Idle, NotesState.Hold, NotesTrigger.HoldTrigger);
                st.AddTransition(NotesState.Hold, NotesState.Active, NotesTrigger.ActiveTrigger);
                st.AddTransition(NotesState.Active, NotesState.Hold, NotesTrigger.HoldTrigger);
                st.AddTransition(NotesState.Active, NotesState.Ded, NotesTrigger.DedTrigger);
            }

            Initialize();
        }

        protected virtual void Start() {

            
        }

        protected virtual void Update()
        {

            // ステートマシンの更新
            st.Update(Time.deltaTime);

            // 落下処理
            transform.position += new Vector3(0, -1 * fallSpeed * Time.deltaTime, 0);

            // 時間を加算
            timeCnt += Time.deltaTime;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<InGamePlayer>(out InGamePlayer p_))
            {
                
            }
        }

        protected abstract void Initialize();

        public abstract void ActiveNotes(PlayerState state);
    }
}