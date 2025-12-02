using Player;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

namespace Notes {

    public class RushNotes :NotesObject
    {
        private const float perfectLenge = 0.033f;
        private const float goodLenge = 0.05f;
        private int index;
        private int max_index = default;
        private bool isFirst;

        public override void SetInitilizeNotes(NotesInformaiton i_)
        {
            // 最大値の設定
            max_index = i_.Notes.lenge + 1;
            Max_holdCnt = i_.Notes.lenge;

            // スプライトの調整
            Vector3 Pos = transform.GetChild(0).transform.position;
            Pos += new Vector3(0, (i_.Notes.lenge - 1) * 0.5f, 0);
            transform.GetChild(0).position = Pos;

            Vector3 Scale = transform.GetChild(0).transform.localScale;
            Scale+= new Vector3(0, i_.Notes.lenge, 0);
            transform.GetChild(0).localScale = Scale;

            // BPMの定義
            BPM = i_.BPM;

            // 変数の初期化
            {
                score = new NotesScoreData(max_index);
                index = 1;
                score.SetScore(NotesScore.Miss, 0);
                isFirst = true;
                CreateTime = i_.CteateTime;
                NotesManager = i_.NotesManager;
            }

            // ステートマシンの初期化
            {
                st = new StateMachine<NotesState, NotesTrigger>(NotesState.Idle);

                /// ステートマシーンの初期化
                st.SetupState(NotesState.Idle, new RushIdleNotes(this, st));
                st.SetupState(NotesState.Hold, new RushHoldState(this, st));
                st.SetupState(NotesState.Active, new RushActiveState(this, st));
                st.SetupState(NotesState.Ded, new RushDedState(this, st));

                // 遷移条件の登録

                st.AddTransition(NotesState.Idle, NotesState.Active, NotesTrigger.ActiveTrigger);
                st.AddTransition(NotesState.Idle, NotesState.Hold, NotesTrigger.HoldTrigger);
                st.AddTransition(NotesState.Hold, NotesState.Active, NotesTrigger.ActiveTrigger);
                st.AddTransition(NotesState.Hold, NotesState.Ded, NotesTrigger.DedTrigger);
                st.AddTransition(NotesState.Active, NotesState.Hold, NotesTrigger.HoldTrigger);
            }

        }
    }
}
