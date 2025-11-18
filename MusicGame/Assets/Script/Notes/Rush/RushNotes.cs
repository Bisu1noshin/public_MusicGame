using Player;
using UnityEngine;

namespace Notes {

    public class RushNotes :NotesParent
    {
        private const float perfectLenge = 0.033f;
        private const float goodLenge = 0.05f;
        private int index;
        private int max_index = 9;
        private bool isFirst;

        protected override void Initialize()
        {

            // 変数の初期化
            {
                score = new NotesScoreData(max_index);
                side = NotesSide.Left;
                AnsTrigger = PlayerState.Left;
                timeCnt = 0;
                BPM = 200;
                index = 1;
                score.SetScore(NotesScore.Miss, 0);
                isFirst = true;
            }
        }

        public override void ActiveNotes(Player.PlayerState state)
        {
            if (st.GetState() == NotesState.Ded) { return; }

            // 破壊処理
            if (index >= max_index)
            {
                st.ExecuteTriggerAction(NotesTrigger.DedTrigger);
                return;
            }

            // 時間の定義
            float haku = 60.0f / BPM / 2.0f;
            float fromTime = perfectTime + haku * (float)(index - 1);
            float toTime = fromTime + haku;

            // 始点の処理
            if (isFirst)
            {
                if (state != PlayerState.Idle)
                {
                    // goodの処理
                    if (this.timeCnt <= perfectTime + goodLenge && this.timeCnt >= perfectTime - goodLenge)
                        score.SetScore(NotesScore.Good, 0);

                    // perfectの処理
                    if (this.timeCnt <= perfectTime + perfectLenge && this.timeCnt >= perfectTime - perfectLenge)
                        score.SetScore(NotesScore.Perfect, 0);
                }

                if (this.timeCnt >= perfectTime + goodLenge)
                    isFirst = false;
            }


            // 8分ごとの処理
            if (this.timeCnt >= fromTime && this.timeCnt <= toTime)
            {
                if (state == PlayerState.Idle)
                {
                    score.SetScore(NotesScore.Miss, index);
                    index++;
                    return;
                }
            }

            if (this.timeCnt >= toTime)
            {
                if (state != Player.PlayerState.Idle)
                {
                    score.SetScore(NotesScore.Perfect, index);
                }

                index++;
            }
        }
    }
}
