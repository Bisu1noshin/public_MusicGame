using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public abstract class NotesHoldState : StateBase<NotesObject, NotesTrigger>
    {
        public NotesHoldState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {
            
        }

        protected override void OnEnter()
        {
            owner.NotesActoin += ActiveNotes;
        }

        protected override void OnUpdate(float deltaTime)
        {
            // pass
        }

        protected override void OnExit()
        {
            owner.NotesActoin -= ActiveNotes;
        }

        // 発火イベント
        protected abstract NotesObject ActiveNotes(PlayerState state);
    }
}
