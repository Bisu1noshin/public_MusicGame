
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting.Operation
{
    public class Oper_Home : HomeStateAbstract<Setting_Oper, OTrigger>
    {
        public Oper_Home(Setting_Oper owner, IStateMachine<OTrigger> st) : base(owner, st)
        {
            mActions = new()
            {
                () => stateMachine.ExecuteTriggerAction(OTrigger.Lane),
                () => stateMachine.ExecuteTriggerAction(OTrigger.LR),
                () => stateMachine.ExecuteTriggerAction(OTrigger.UD),
                () => owner.BackToHome()
            };
            ReplaceEnterAction(mSelectNum);
            Player.vecAction = (vec2) => Scroll(vec2);
        }
        protected override void OnEnter()
        {
            mPrevSelectNum = mSelectNum;
        }
        protected override void OnUpdate(float deltaTime)
        {
            if (mPrevSelectNum != mSelectNum)
            {
                ReplaceEnterAction(mSelectNum);
            }
            mSceneManager.TrySetCursorPos(mSelectNum, 4);
            mPrevSelectNum = mSelectNum;
        }
        protected override void OnExit()
        {
            
        }
        void ReplaceEnterAction(int v)
        {
            if (v < mActions.Count - 1) Player.enterAction = () => PlayEnterSound();
            else Player.enterAction = () => PlayCancelSound();
            Player.enterAction += mActions[v];
        }
    }
}