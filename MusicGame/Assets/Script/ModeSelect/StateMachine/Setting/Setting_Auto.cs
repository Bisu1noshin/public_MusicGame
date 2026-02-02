using UnityEngine;
using System.Collections.Generic;
using System;
using System.Buffers;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_Auto : PopupAdmin<SettingState, STrigger>
    {
        bool instantBool;
        public Setting_Auto(SettingState owner, IStateMachine<STrigger> st) : base(owner, st, true)
        {

        }

        protected override void OnEnter()
        {
            Player.enterAction = null;
            instantBool = owner.PlayerConfig.AutoPlay;
            CreateSpeedPopupInstance(ref mPopup, "オートプレイを変更します。よろしいですか？");
            CreateCursor();
            Action change = () =>
            {
                instantBool = !instantBool;
            };
            Action ent = () =>
            {
                owner.PlayerConfig.AutoPlay = instantBool;
                stateMachine.ExecuteTriggerAction(STrigger.Home);
            };
            Action back = () =>
            {
                stateMachine.ExecuteTriggerAction(STrigger.Home);
            };
            SetChangeElementAction(change, change);
            SetEnterAndCancelAction(ent, back);
            Player.vecAction = (vec) => ChangeState(vec);
        }
        protected override void OnUpdate(float deltaTime)
        {
            mPopup.SetValue(instantBool ? "ON" : "OFF", 72);
        }
        protected override void OnExit()
        {
            mSceneManager.TryDeletePopupCursor();
            deleteAction?.Invoke();
            deleteAction = null;
        }
    }
}