
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting.Operation
{
    public class Oper_Home : Kameda_StateParent<Setting_Oper, OTrigger>
    {
        int mPrevSelectNum;
        public Oper_Home(Setting_Oper owner, IStateMachine<OTrigger> st) : base(owner, st)
        {
            mActions = new List<Action>
            {
                () => stateMachine.ExecuteTriggerAction(OTrigger.Lane),
                () => stateMachine.ExecuteTriggerAction(OTrigger.LR),
                () => stateMachine.ExecuteTriggerAction(OTrigger.UD),
                () => owner.BackToHome()
            };
            ReplaceEnterAction(mSelectNum);
            Player.backAction = () =>
            {
                PlayCancelSound();
                owner.BackToHome();
            };
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
            mSceneManager.TrySetCursorPos(mSelectNum, mActions.Count);
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