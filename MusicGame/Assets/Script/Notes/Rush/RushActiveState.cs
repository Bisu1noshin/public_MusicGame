using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class RushActiveState : NotesActiveState
    {
        public RushActiveState(NotesObject owner, IStateMachine<NotesTrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {

        }

        protected override void OnUpdate(float deltaTime)
        {
            // pass
        }

        protected override void OnExit()
        {

        }
    }
}
