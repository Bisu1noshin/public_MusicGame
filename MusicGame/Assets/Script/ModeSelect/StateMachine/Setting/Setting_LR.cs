using UnityEngine;
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_LR : StateBase<SettingState, STrigger>
    {
        Action deleteAction;
        ISettingState mOwner;
        public Setting_LR(SettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {
            mOwner = owner;
        }

        protected override void OnEnter()
        {
            deleteAction += PopupController.CreateInstance("左右反転を" +
                (mOwner.PlayerConfig.LeftRightCahge ? "OFF" : "ON") + "にします。よろしいですか？");
            Player.enterAction = () =>
            {
                mOwner.PlayerConfig.LeftRightCahge = !mOwner.PlayerConfig.LeftRightCahge;
                mOwner.StateMachine.ExecuteTriggerAction(STrigger.Home);
            };
            Player.backAction = () =>
            {
                mOwner.StateMachine.ExecuteTriggerAction(STrigger.Home);
            };
            Player.vecAction = null;
        }
        protected override void OnUpdate(float deltaTime)
        {

        }
        protected override void OnExit()
        {
            deleteAction?.Invoke();
        }
    }
}