using UnityEngine;
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_Lane : StateBase<SettingState, STrigger>
    {
        Action deleteAction;
        ISettingState mOwner;
        public Setting_Lane(SettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {
            mOwner = owner;
        }

        protected override void OnEnter()
        {
            deleteAction += PopupController.CreateInstance("レーン反転を" +
                (mOwner.PlayerConfig.LaneCahge ? "OFF" : "ON") + "にします。よろしいですか？");
            Player.enterAction = () =>
            {
                mOwner.PlayerConfig.LaneCahge = !mOwner.PlayerConfig.LaneCahge;
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