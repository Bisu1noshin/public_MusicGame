using UnityEngine;
using System;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_Device : Kameda_StateParent<ISettingState, STrigger>
    {
        public Setting_Device(ISettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {
        }

        protected override void OnEnter()
        {
            deleteAction += CreatePopupInstance("使用デバイスを" +
                (owner.PlayerConfig.InputDevice == Notes.InputDevice.Controller ? "キーボード" : "コントローラー") +
                "にします。よろしいですか？");
            Player.enterAction = () =>
            {
                PlayEnterSound();
                if (owner.PlayerConfig.InputDevice == Notes.InputDevice.Controller)
                {
                    owner.PlayerConfig.InputDevice = Notes.InputDevice.KyeBord;
                }
                else
                {
                    owner.PlayerConfig.InputDevice = Notes.InputDevice.Controller;
                }
                //プレイヤーを作り直す
                Player.destroyAction?.Invoke();
                GameObject.Instantiate(Resources.Load<GameObject>("ModeSelect/Player"));
                stateMachine.ExecuteTriggerAction(STrigger.Home);
            };
            Player.backAction = () =>
            {
                PlayCancelSound();
                stateMachine.ExecuteTriggerAction(STrigger.Home);
            };
            Player.vecAction = null;
        }
        protected override void OnUpdate(float deltaTime)
        {
        }
        protected override void OnExit()
        {
            deleteAction?.Invoke();
            deleteAction = null;
        }
    }
}