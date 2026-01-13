using UnityEngine;
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_UD : Kameda_StateParent<ISettingState, STrigger>
    {
        public Setting_UD(ISettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {
            deleteAction += PopupController.CreateInstance("上下反転を" +
                (owner.PlayerConfig.UpDownCahge ? "OFF" : "ON") + "にします。よろしいですか？");
            Player.enterAction = () =>
            {
                PlayEnterSound();
                owner.PlayerConfig.UpDownCahge = !owner.PlayerConfig.UpDownCahge;
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