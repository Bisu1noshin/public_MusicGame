using Notes;
using UnityEngine;

public class FlickNotes : NotesParent
{
    private const float perfectLenge = 0.1f;
    private const float goodLenge = 0.3f;

    protected override void ActiveNotes(Player.PlayerState state)
    {
        if (state == Player.PlayerState.Idle) { return; }

        if (state == AnsTrigger) {

            // goodの処理
            if (this.transform.position.y >= perfectPos + goodLenge && this.transform.position.y <= perfectPos - goodLenge)
                score = NotesScore.Good;

            // perfectの処理
            if (this.transform.position.y >= perfectPos+perfectLenge && this.transform.position.y <= perfectPos - perfectLenge)
                score = NotesScore.Perfect;
        }

        st.ExecuteTriggerAction(NotesTrigger.DedTrigger);
    }
}
