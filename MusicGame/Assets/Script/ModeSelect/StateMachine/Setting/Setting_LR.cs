using UnityEngine;
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_LR : Kameda_StateParent<ISettingState, STrigger>
    {
        public Setting_LR(ISettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {
            
        }

        protected override void OnEnter()
        {
            deleteAction += PopupController.CreateInstance("左右反転を" +
                (owner.PlayerConfig.LeftRightCahge ? "OFF" : "ON") + "にします。よろしいですか？");
            Player.enterAction = () =>
            {
                PlayEnterSound();
                owner.PlayerConfig.LeftRightCahge = !owner.PlayerConfig.LeftRightCahge;
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
            Player.enterAction ??= () =>
            {
                PlayEnterSound();
                owner.PlayerConfig.LeftRightCahge = !owner.PlayerConfig.LeftRightCahge;
                stateMachine.ExecuteTriggerAction(STrigger.Home);
            };
            Player.backAction ??= () =>
            {
                PlayCancelSound();
                stateMachine.ExecuteTriggerAction(STrigger.Home);
            };
        }
        protected override void OnExit()
        {
            deleteAction?.Invoke();
            deleteAction = null;
        }
    }
}