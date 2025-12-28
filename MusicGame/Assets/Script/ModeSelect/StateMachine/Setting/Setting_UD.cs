using UnityEngine;
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_UD : StateBase<SettingState, STrigger>
    {
        Action deleteAction;
        ISettingState mOwner;
        public Setting_UD(SettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {
            mOwner = owner;
        }

        protected override void OnEnter()
        {
            deleteAction += PopupController.CreateInstance("上下反転を" +
                (mOwner.PlayerConfig.UpDownCahge ? "OFF" : "ON") + "にします。よろしいですか？");
            Player.enterAction = () =>
            {
                mOwner.PlayerConfig.UpDownCahge = !mOwner.PlayerConfig.UpDownCahge;
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