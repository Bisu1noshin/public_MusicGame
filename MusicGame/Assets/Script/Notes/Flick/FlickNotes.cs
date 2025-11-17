using Notes;
using Player;
using UnityEngine;

public class FlickNotes : NotesParent
{
    private const float perfectLenge = 0.033f;
    private const float goodLenge = 0.05f;
    private bool isFirst;

    protected override void Initialize() {

        // 変数の初期化
        {
            score = new NotesScoreData();
            side = NotesSide.Left;
            AnsTrigger = PlayerState.Left;
            timeCnt = 0;
            BPM = 200;
            score.SetScore(NotesScore.Miss);
            isFirst = true;
        }
    }

    public override void ActiveNotes(Player.PlayerState state)
    {
        if (state == Player.PlayerState.Idle) { return; }
        if (st.GetState() == NotesState.Ded) { return; }

        if (state == AnsTrigger)
        {
            // perfectの処理
            if (this.timeCnt <= perfectTime + perfectLenge && this.timeCnt >= perfectTime - perfectLenge)
            {
                score.SetScore(NotesScore.Perfect, 0);
                st.ExecuteTriggerAction(NotesTrigger.DedTrigger);
                return;
            }

            // goodの処理
            if (this.timeCnt <= perfectTime + goodLenge && this.timeCnt >= perfectTime - goodLenge)
            {
                score.SetScore(NotesScore.Good, 0);
                st.ExecuteTriggerAction(NotesTrigger.DedTrigger);
                return;
            }   
        }

        if (this.timeCnt >= perfectTime + goodLenge)
            st.ExecuteTriggerAction(NotesTrigger.DedTrigger);
    }
}
