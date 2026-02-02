using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeSelect.StateMachine.Setting.Operation
{
    public class Oper_Intro : Kameda_StateParent<Setting_Oper, OTrigger>
    {
        public Oper_Intro(Setting_Oper owner, IStateMachine<OTrigger> st) : base(owner, st)
        {

        }
        protected override void OnEnter()
        {
            
        }
        protected override void OnUpdate(float deltaTime)
        {
            
        }
        protected override void OnExit()
        {

        }
    }
}
