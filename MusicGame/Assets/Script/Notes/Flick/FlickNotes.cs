using Notes;
using Player;
using UnityEngine;

namespace Notes
{
    public class FlickNotes : NotesObject
    {
        private const float perfectLenge = 0.033f;
        private const float goodLenge = 0.05f;

        public override void SetInitilizeNotes(NotesInformaiton i_)
        {
            // 方向指定
            AnsTrigger = (PlayerState)(i_.Notes.dir);

            // BPMの定義
            BPMInfo = i_.BPMInfo;

            // スプライトの調整
            {
                var value = 2.5f;
                transform.GetChild(0).localScale = new Vector3(value, value, value);
            }

            // 変数の初期化
            {
                score = new NotesScoreData();
                score.SetScore(NotesScore.Miss);
                CreateTime = i_.CteateTime;
                Judg = i_.Judgment;
                DebugInfo = i_.DebugInfo;
            }

            // ステートマシンの初期化
            {
                st = new StateMachine<NotesState, NotesTrigger>(NotesState.Idle);

                // ステートマシーンの初期化
                st.SetupState(NotesState.Idle, new FlickIdleState(this, st));
                st.SetupState(NotesState.Active, new FlickActiveState(this, st));
                st.SetupState(NotesState.Ded, new FlickDedState(this, st));

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
