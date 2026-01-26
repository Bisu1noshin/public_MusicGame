using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes
{
    public abstract class NotesHoldState : StateBase<NotesObject, NotesTrigger>
    {
        public NotesHoldState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {
            
        }

        protected override void OnEnter()
        {
            if (InGamePlayer.NotesAction[(int)owner.DebugInfo.NotesLane] == null)
                InGamePlayer.NotesAction[(int)owner.DebugInfo.NotesLane] += ActiveNotes;
        }

        protected override void OnUpdate(float deltaTime)
        {
            // pass
        }

        protected override void OnExit()
        {

        }

        // 発火イベント
        protected abstract void ActiveNotes(PlayerState state,float ActiveTime);
    }
}
