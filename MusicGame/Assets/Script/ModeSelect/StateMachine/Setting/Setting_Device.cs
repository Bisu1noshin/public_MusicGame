using UnityEngine;
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_Device : StateBase<SettingState, STrigger>
    {
        Action deleteAction;
        ISettingState mOwner;
        public Setting_Device(SettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {
            mOwner = owner;
        }

        protected override void OnEnter()
        {
            deleteAction += PopupController.CreateInstance("オートプレイを" +
                (mOwner.PlayerConfig.InputDevice == Notes.InputDevice.Controller ? "キーボード" : "コントローラー") +
                "にします。よろしいですか？");
            Player.enterAction = () =>
            {
                if (mOwner.PlayerConfig.InputDevice == Notes.InputDevice.Controller)
                {
                    mOwner.PlayerConfig.InputDevice = Notes.InputDevice.KyeBord;
                }
                else
                {
                    mOwner.PlayerConfig.InputDevice = Notes.InputDevice.Controller;
                }
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