using UnityEngine;
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_Speed : PopupAdmin<SettingState, STrigger>
    {
        float mCurrSpeed;
        public Setting_Speed(SettingState owner, IStateMachine<STrigger> st) : base(owner, st, true)
        {
            
        }

        protected override void OnEnter()
        {
            Player.enterAction = null;
            CreateSpeedPopupInstance(ref mPopup,
                (owner.PlayerConfig.InputDevice == Notes.InputDevice.KyeBord ? "WキーとSキー" : "左スティック上下") + "で\nノーツ速度の調整ができます。");
            CreateCursor();
            mCurrSpeed = owner.PlayerConfig.NotesSpeed;
            Action right = () =>
            {
                mCurrSpeed += 0.1f;
                if (mCurrSpeed > 3f) { mCurrSpeed = 3f; }
            };
            Action left = () =>
            {
                mCurrSpeed -= 0.1f;
                if (mCurrSpeed < 0.3f) mCurrSpeed = 0.3f;
            };
            Action ent = () =>
            {
                owner.PlayerConfig.NotesSpeed = mCurrSpeed;
                stateMachine.ExecuteTriggerAction(STrigger.Home);
            };
            Action back = () =>
            {
                stateMachine.ExecuteTriggerAction(STrigger.Home);
            };
            SetChangeElementAction(right, left);
            SetEnterAndCancelAction(ent, back);
            Player.vecAction = (vec) => ChangeState(vec);
        }
        protected override void OnUpdate(float deltaTime)
        {
            mPopup.SetValue(mCurrSpeed.ToString(), 72);
        }
        protected override void OnExit()
        {
            mSceneManager.TryDeletePopupCursor();
            deleteAction?.Invoke();
            deleteAction = null;
        }
    }
}