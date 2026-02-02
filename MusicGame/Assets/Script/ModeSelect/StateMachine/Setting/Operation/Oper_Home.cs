
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting.Operation
{
    public class Oper_Home : HomeStateAbstract<Setting_Oper, OTrigger>
    {
        PropertyController mProperty;
        public Oper_Home(Setting_Oper owner, IStateMachine<OTrigger> st) : base(owner, st)
        {
            mActions = new()
            {
                () => stateMachine.ExecuteTriggerAction(OTrigger.Lane),
                () => stateMachine.ExecuteTriggerAction(OTrigger.LR),
                () => stateMachine.ExecuteTriggerAction(OTrigger.UD),
                () => owner.BackToHome()
            };
            
        }
        protected override void OnEnter()
        {
            mProperty = owner.GetProperty();
            mPrevSelectNum = mSelectNum;
            ReplaceEnterAction(mSelectNum);
            Player.vecAction = (vec2) => Scroll(vec2);
        }
        protected override void OnUpdate(float deltaTime)
        {
            SetPropertyText(mSelectNum);
            if (mPrevSelectNum != mSelectNum)
            {
                ReplaceEnterAction(mSelectNum);
            }
            mSceneManager.TrySetCursorPos(mSelectNum, 4);
            mPrevSelectNum = mSelectNum;
        }
        protected override void OnExit()
        {
            mProperty = null;
        }
        void ReplaceEnterAction(int v)
        {
            if (v < mActions.Count - 1) Player.enterAction = () => PlayEnterSound();
            else Player.enterAction = () => PlayCancelSound();
            Player.enterAction += mActions[v];
        }

        void SetPropertyText(int v)
        {
            if (!mProperty) return;
            string str = v switch
            {
                0 => "レーンの左右を反転できます。\n現在：" + (owner.PlayerConfig.LaneCahge ? "ON" : "OFF"),
                1 => "ノーツの左右入力を反転できます。\n現在：" + (owner.PlayerConfig.LeftRightCahge ? "ON" : "OFF"),
                2 => "ノーツの上下入力を反転できます。\n現在：" + (owner.PlayerConfig.UpDownCahge ? "ON" : "OFF"),
                3 => "メニュー一覧に戻ります。",
                _ => null
            };
            mProperty.SetText(str);
        }
    }
}