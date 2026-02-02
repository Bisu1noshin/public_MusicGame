using GameInfo;
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

            var pos = new Vector3(owner.transform.position.x, -0.109999999f, -6.80000019f);
            GameObject.Instantiate(JugeEfect[(int)score - 1], pos, Quaternion.identity);
        }

        protected override void OnUpdate(float deltaTime)
        {
            // pass
        }

        protected override void OnExit()
        {
            SingletonDataManager.instance.SetScore(this.owner.score.score[holdCnt]);
            holdCnt++;
        }
    }
}
