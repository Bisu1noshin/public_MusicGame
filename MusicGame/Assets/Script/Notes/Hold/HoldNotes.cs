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
            {
                for (int i = 0; i < i_.Notes.range; i++)
                {
                    float speed = 60.0f / (float)i_.BPMInfo.MusicBPM / 4.0f;
                    float value = 8f * speed;
                    Vector3 vector = new Vector3(0, 2.5f * value / 2.0f + 2.5f * value * i, 0);
                    transform.GetChild(i).localPosition = vector;
                    transform.GetChild(i).localScale = new Vector3(1, value, 1);
                    transform.GetChild(i).gameObject.gameObject.SetActive(true);
                }
            }

            // 方向指定
            AnsTrigger = (PlayerState)((int)i_.Notes.dir);

            // BPMの定義
            BPMInfo = i_.BPMInfo;

            // 変数の初期化
            {
                score = new NotesScoreData(max_index);
                index = 1;
                score.SetScore(NotesScore.Miss, 0);
                isFirst = true;
                CreateTime = i_.CteateTime;
                Judg = i_.Judgment;
                DebugInfo = i_.DebugInfo;
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
