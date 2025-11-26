using Player;
using UnityEngine;

namespace Notes {

    public class HoldNotes : NotesObject
    {
        private const float perfectLenge = 0.033f;
        private const float goodLenge = 0.05f;
        private int index = default;
        private bool isFirst;
        private int max_index;

        protected override void Initialize()
        {
            // ステートマシンの初期化
            {
                st = new StateMachine<NotesState, NotesTrigger>(NotesState.Idle);

                // ステートマシーンの初期化
                st.SetupState(NotesState.Idle, new HoldIdleState(this, st));
                st.SetupState(NotesState.Hold, new HoldHoldState(this, st));
                st.SetupState(NotesState.Active, new HoldActiveState(this, st));
                st.SetupState(NotesState.Ded, new HoldDedState(this, st));

                // 遷移条件の登録

                st.AddTransition(NotesState.Idle, NotesState.Active, NotesTrigger.ActiveTrigger);
                st.AddTransition(NotesState.Idle, NotesState.Hold, NotesTrigger.HoldTrigger);
                st.AddTransition(NotesState.Hold, NotesState.Active, NotesTrigger.ActiveTrigger);
                st.AddTransition(NotesState.Active, NotesState.Hold, NotesTrigger.HoldTrigger);
                st.AddTransition(NotesState.Active, NotesState.Ded, NotesTrigger.DedTrigger);
            }
        }

        public override void SetInitilizeNotes(Notes n_, int b_)
        {
            // 最大値の設定
            max_index = n_.lenge + 1;

            // スプライトの調整
            Vector3 Pos = transform.GetChild(0).position;
            Pos += new Vector3(0, (n_.lenge - 1) * 0.5f, 0);
            transform.GetChild(0).position = Pos;

            Vector3 Scale = new Vector3(1, n_.lenge, 1);
            transform.GetChild(0).localScale = Scale;

            // 方向指定
            AnsTrigger = (PlayerState)((int)n_.dir);

            //
            BPM = b_;

            // 変数の初期化
            {
                score = new NotesScoreData(max_index);
                index = 1;
                score.SetScore(NotesScore.Miss, 0);
                isFirst = true;
            }           
        }
    }
}
