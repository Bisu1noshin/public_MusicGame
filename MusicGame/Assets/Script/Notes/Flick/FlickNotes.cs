using Notes;
using Player;
using UnityEngine;

public class FlickNotes : NotesParent
{
    private const float perfectLenge = 0.033f;
    private const float goodLenge = 0.05f;

    protected override void Initialize() {

        // 変数の初期化
        {
            score = new NotesScoreData();
            side = NotesSide.Left;
            AnsTrigger = PlayerState.Left;
            timeCnt = 0;
            BPM = 200;
        }
    }

    public override void ActiveNotes(Player.PlayerState state)
    {
        if (state == Player.PlayerState.Idle) { return; }
        if (st.GetState() == NotesState.Ded) { return; }

        if (state == AnsTrigger) {

            // goodの処理
            if (this.timeCnt <= perfectTime + goodLenge && this.timeCnt >= perfectTime - goodLenge)
                score.SetScore(NotesScore.Good);

            // perfectの処理
            if (this.timeCnt <= perfectTime + perfectLenge && this.timeCnt >= perfectTime - perfectLenge)
                score.SetScore(NotesScore.Perfect);
        }

        st.ExecuteTriggerAction(NotesTrigger.DedTrigger);
        Debug.Log(this.timeCnt.ToString());
    }
}
