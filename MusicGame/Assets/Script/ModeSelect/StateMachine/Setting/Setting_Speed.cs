using UnityEngine;
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_Speed : Kameda_StateParent<SettingState, STrigger>
    {
        PopupController mPopup;
        float mCurrSpeed;
        public Setting_Speed(SettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {
            
        }

        protected override void OnEnter()
        {
            (PopupController, Action) tuple = CreateSpeedPopupInstance(true);
            mPopup = tuple.Item1;
            deleteAction += tuple.Item2;
            mCurrSpeed = owner.PlayerConfig.NotesSpeed;
            Player.enterAction = () =>
            {
                PlayEnterSound();
                owner.PlayerConfig.NotesSpeed = mCurrSpeed;
                stateMachine.ExecuteTriggerAction(STrigger.Home);
            };
            Player.backAction = () =>
            {
                PlayCancelSound();
                stateMachine.ExecuteTriggerAction(STrigger.Home);
            };
            Player.vecAction = (Vector2) => ChangeCurrSpeed(Vector2);
        }
        protected override void OnUpdate(float deltaTime)
        {
            mPopup.SetValue(mCurrSpeed);
        }
        protected override void OnExit()
        {
            deleteAction?.Invoke();
            deleteAction = null;
        }
        /// <summary>
        /// 　ノーツ速度を変える関数　vecActionに突っ込む
        /// 　現在は0.1単位で変更できる
        /// </summary>
        void ChangeCurrSpeed(Vector2 vec)
        {
            mCurrSpeed += vec.y * 0.1f;
            if (mCurrSpeed > 3.0f) { mCurrSpeed = 3.0f; }
            if (mCurrSpeed < 0.3f) { mCurrSpeed = 0.3f; }
        }
    }
}