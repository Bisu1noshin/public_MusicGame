using Player;
using UnityEngine;

namespace Notes {

    public class RushNotes :NotesObject
    {
        private int max_index = default;

        public override void SetInitilizeNotes(NotesInformaiton i_)
        {
            // 最大値の設定
            max_index = i_.Notes.range + 1;
            Max_holdCnt = i_.Notes.range + 1;

            // スプライトの調整
            IntilizeSprite(i_.Notes.range);

            // BPMの定義
            BPMInfo = i_.BPMInfo;

            // 変数の初期化
            {
                score = new NotesScoreData(max_index);
                score.SetScore(NotesScore.Miss, 0);
                CreateTime = i_.CteateTime;
                Judg = i_.Judgment;
                DebugInfo = i_.DebugInfo;
            }

            // ステートマシンの初期化
            {
                st = new StateMachine<NotesState, NotesTrigger>(NotesState.Idle, new RushIdleNotes(this, st));

                /// ステートマシーンの初期化
                st.SetupState(NotesState.Idle, new RushIdleNotes(this, st));
                st.SetupState(NotesState.Hold, new RushHoldState(this, st));
                st.SetupState(NotesState.Active, new RushActiveState(this, st));
                st.SetupState(NotesState.Ded, new RushDedState(this, st));

                // 遷移条件の登録

                st.AddTransition(NotesState.Idle, NotesState.Active, NotesTrigger.ActiveTrigger);
                st.AddTransition(NotesState.Idle, NotesState.Hold, NotesTrigger.HoldTrigger);
                st.AddTransition(NotesState.Hold, NotesState.Active, NotesTrigger.ActiveTrigger);
                st.AddTransition(NotesState.Active, NotesState.Hold, NotesTrigger.HoldTrigger);
                st.AddTransition(NotesState.Active, NotesState.Ded, NotesTrigger.DedTrigger);
            }

        }

        private void IntilizeSprite(int r_)
        {
            Vector3 Pos = transform.GetChild(0).transform.position;
            Pos += new Vector3(0, (r_ - 1) * 0.5f, 0);
            transform.GetChild(0).position = Pos;

            Vector3 Scale = transform.GetChild(0).transform.localScale;
            Scale += new Vector3(0, r_, 0);
            transform.GetChild(0).localScale = Scale;
        }
    }
}
