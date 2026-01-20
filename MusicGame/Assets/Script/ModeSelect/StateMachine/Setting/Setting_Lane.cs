using UnityEngine;
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_Lane : Kameda_StateParent<ISettingState, STrigger>
    {
        public Setting_Lane(ISettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {
            deleteAction += CreatePopupInstance("レーン反転を" +
                (owner.PlayerConfig.LaneCahge ? "OFF" : "ON") + "にします。よろしいですか？");
            Player.enterAction = () =>
            {
                PlayEnterSound();
                owner.PlayerConfig.LaneCahge = !owner.PlayerConfig.LaneCahge;
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