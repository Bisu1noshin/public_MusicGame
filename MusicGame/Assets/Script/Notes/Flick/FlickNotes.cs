using Notes;
using Player;
using UnityEngine;

namespace Notes
{
    public class FlickNotes : NotesObject
    {
        private const float perfectLenge = 0.033f;
        private const float goodLenge = 0.05f;

        public override void SetInitilizeNotes(Notes n_,int b_)
        {
            // 方向指定
            AnsTrigger = (PlayerState)((int)n_.dir);

            //
            BPM = b_;            

            // 変数の初期化
            {
                score = new NotesScoreData();
                score.SetScore(NotesScore.Miss);
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
