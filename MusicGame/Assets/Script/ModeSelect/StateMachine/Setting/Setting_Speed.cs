using UnityEngine;
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_Speed : StateBase<SettingState, STrigger>
    {
        Action deleteAction;
        ISettingState mOwner;
        PopupController mPopup;
        float mCurrSpeed;
        public Setting_Speed(SettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {
            mOwner = owner;
        }

        protected override void OnEnter()
        {
            (PopupController, Action) tuple = PopupController.CreateInstanceForNotesSpeed(true);
            mPopup = tuple.Item1;
            deleteAction += tuple.Item2;
            mCurrSpeed = mOwner.PlayerConfig.NotesSpeed;
            Player.enterAction = () =>
            {
                mOwner.PlayerConfig.NotesSpeed = mCurrSpeed;
                mOwner.StateMachine.ExecuteTriggerAction(STrigger.Home);
            };
            Player.backAction = () =>
            {
                mOwner.StateMachine.ExecuteTriggerAction(STrigger.Home);
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
        }
        /// <summary>
        /// 　ノーツ速度を変える関数　vecActionに突っ込む
        /// 　現在は0.1単位で変更できる
        /// </summary>
        void ChangeCurrSpeed(Vector2 vec)
        {
            mCurrSpeed += vec.y * 0.1f;
            //mCurrSpeed = ((int)(mCurrSpeed * 10.0f)) / 10;
            if (mCurrSpeed > 3.0f) { mCurrSpeed = 3.0f; }
            if (mCurrSpeed < 0.3f) { mCurrSpeed = 0.3f; }
        }
    }
}