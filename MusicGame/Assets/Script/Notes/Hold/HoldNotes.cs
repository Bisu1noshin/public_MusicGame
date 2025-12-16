using Player;
using UnityEngine;

namespace Notes {

    public class HoldNotes : NotesObject
    {
        private const float perfectLenge = 0.033f;
        private const float goodLenge = 0.05f;
        private int index = default;
        private bool isFirst;

        public override void SetInitilizeNotes(NotesInformaiton i_)
        {
            // 幅の再定義
            var average = 2.0f * i_.BPMInfo.MusicBPM / i_.BPMInfo.NotesBPM;

            // 最大値の設定
            Max_holdCnt = (int)(i_.Notes.range * average) + 1;

            // スプライトの調整
            {
                for (int i = 0; i < Max_holdCnt; i++)
                {
                    var value = 60.0 / i_.BPMInfo.MusicBPM / 2.0 * 8.0 + 1.0 / 15.0;
                    var Y_Point = (value + 1.0) / 2.0 + value * i;
                    Vector3 vector = new Vector3(0, (float)Y_Point, 0);
                    var rot = new Vector3(0, 0, 180f * i);
                    transform.GetChild(i).localPosition = vector;
                    transform.GetChild(i).localRotation = Quaternion.Euler(rot);
                    transform.GetChild(i).localScale = new Vector3(1, (float)value / 2.5f, 1) * 2.5f;
                    transform.GetChild(i).gameObject.gameObject.SetActive(true);
                }
            }

            // 方向指定
            AnsTrigger = (PlayerState)((int)i_.Notes.dir);

            // BPMの定義
            BPMInfo = i_.BPMInfo;

            // 変数の初期化
            {
                score = new NotesScoreData(Max_holdCnt + 1);
                index = 1;
                score.SetScore(NotesScore.Miss, 0);
                isFirst = true;
                CreateTime = i_.CteateTime;
                Judg = i_.Judgment;
                DebugInfo = i_.DebugInfo;
            }

            // ステートマシンの初期化
            {
                st = new StateMachine<NotesState, NotesTrigger>(NotesState.Idle, new HoldIdleState(this, st));

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
