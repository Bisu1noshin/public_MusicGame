using UnityEngine;
using System.Collections.Generic;
using System;
using System.Buffers;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_Auto : Kameda_StateParent<ISettingState, STrigger>
    {
        public Setting_Auto(ISettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {

        }

        protected override void OnEnter()
        {
            deleteAction += CreatePopupInstance("オートプレイを" +
                (owner.PlayerConfig.AutoPlay ? "OFF" : "ON") + "にします。よろしいですか？");
            Player.enterAction = () =>
            {
                PlayEnterSound();
                owner.PlayerConfig.AutoPlay = !owner.PlayerConfig.AutoPlay;
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