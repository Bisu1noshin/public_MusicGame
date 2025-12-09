using UnityEngine;
namespace Notes
{

    public abstract class NotesActiveState : StateBase<NotesObject, NotesTrigger>
    {
        private GameObject[] JugeEfect = new GameObject[2];
        private int holdCnt;

        public NotesActiveState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {
            JugeEfect[0] = Resources.Load<GameObject>("Effect/NotesBomb_Good");
            JugeEfect[1] = Resources.Load<GameObject>("Effect/NotesBomb_Perfect");
            holdCnt = 0;
        }

        protected override void OnEnter()
        {
            var score = this.owner.score.score[holdCnt];

            if (score == NotesScore.Miss) { return; }

            Vector3 pos = new(owner.transform.position.x, -3.0f, 0);
            GameObject.Instantiate(JugeEfect[(int)score - 1], pos, Quaternion.identity);
        }

        protected override void OnUpdate(float deltaTime)
        {
            // pass
        }

        protected override void OnExit()
        {
            holdCnt++;
        }
    }
}
