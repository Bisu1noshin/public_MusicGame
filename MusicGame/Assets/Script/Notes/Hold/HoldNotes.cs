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

        public override void SetInitilizeNotes(NotesInformaiton i_)
        {
            // 最大値の設定
            max_index = i_.Notes.range + 1;
            Max_holdCnt = i_.Notes.range + 1;

            // スプライトの調整
            Vector3 Pos = transform.GetChild(0).position;
            Pos += new Vector3(0, (i_.Notes.range - 1) * 0.5f, 0);
            transform.GetChild(0).position = Pos;

            Vector3 Scale = new Vector3(1, i_.Notes.range, 1);
            transform.GetChild(0).localScale = Scale;

            // 方向指定
            AnsTrigger = (PlayerState)((int)i_.Notes.dir);

            // BPMの定義
            BPM = i_.BPM;

            // 変数の初期化
            {
                score = new NotesScoreData(max_index);
                index = 1;
                score.SetScore(NotesScore.Miss, 0);
                isFirst = true;
                CreateTime = i_.CteateTime;
                Judg = i_.Judgment;
                NotesNum = i_.NotesNum;
                lane = i_.lane;
            }

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
    }
}
