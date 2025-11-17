using Notes;
using Player;
using Unity.VisualScripting;
using UnityEngine;

public class HoldNotes : NotesParent
{
    private const float perfectLenge = 0.033f;
    private const float goodLenge = 0.05f;
    private int index;
    private int max_index = 9;

    protected override void Initialize()
    {

        // 変数の初期化
        {
            score = new NotesScoreData(max_index);
            side = NotesSide.Left;
            AnsTrigger = PlayerState.Left;
            timeCnt = 0;
            BPM = 200;
            index = 0; 
        }

        score.SetScore(NotesScore.Miss, index);
    }

    protected override void Update()
    {
        base.Update();


    }

    public override void ActiveNotes(Player.PlayerState state)
    {
        if (st.GetState() == NotesState.Ded) { return; }

        // 時間の定義
        float haku = 60.0f / BPM * 1000.0f / 2.0f;
        float fromTime = perfectTime + haku * index;
        float toTime = fromTime + haku;

        // 始点の処理
        if (state == AnsTrigger && index == 0)
        {
            // goodの処理
            if (this.timeCnt <= fromTime + goodLenge && this.timeCnt >= fromTime - goodLenge)
                score.SetScore(NotesScore.Good, index);

            // perfectの処理
            if (this.timeCnt <= fromTime + perfectLenge && this.timeCnt >= fromTime - perfectLenge)
                score.SetScore(NotesScore.Perfect, index);
        }

        // 8分ごとの処理
        if (this.timeCnt >= fromTime && this.timeCnt <= toTime) {

            score.SetScore(NotesScore.Perfect, index);

            if (state != AnsTrigger)
            {
                index++;
                score.SetScore(NotesScore.Miss, index);
                Debug.Log("miss");
            }
        }

        if (this.timeCnt >= toTime) { index++; Debug.Log("perfect"); }

        float DedTime = perfectTime + haku * max_index;

        // 破壊処理
        if (this.timeCnt >= DedTime) {

            st.ExecuteTriggerAction(NotesTrigger.DedTrigger);
        }
    }

    private void NotesUpDate() {

    }
}
