using UnityEngine;
using System.Collections.Generic;
using System;
using System.Buffers;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_Auto : StateBase<SettingState, STrigger>
    {
        Action deleteAction;
        ISettingState mOwner;
        public Setting_Auto(SettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {
            mOwner = owner;
        }

        protected override void OnEnter()
        {
            deleteAction += PopupController.CreateInstance("オートプレイを" +
                (mOwner.PlayerConfig.AutoPlay ? "OFF" : "ON") + "にします。よろしいですか？");
            Player.enterAction = () =>
            {
                mOwner.PlayerConfig.AutoPlay = !mOwner.PlayerConfig.AutoPlay;
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