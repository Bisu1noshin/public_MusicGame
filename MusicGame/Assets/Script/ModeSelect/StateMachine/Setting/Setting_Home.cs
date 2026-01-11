using UnityEngine;
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_Home : Kameda_StateParent<ISettingState, STrigger>
    {
        int mPrevSelectNum;
        PropertyController mProperty;
        public Setting_Home(ISettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {
            mActions = new()
            {
                () => { stateMachine.ExecuteTriggerAction(STrigger.Auto); },
                () => { stateMachine.ExecuteTriggerAction(STrigger.Lane); },
                () => { stateMachine.ExecuteTriggerAction(STrigger.LR); },
                () => { stateMachine.ExecuteTriggerAction(STrigger.UD); },
                () => { stateMachine.ExecuteTriggerAction(STrigger.Device); },
                () => { stateMachine.ExecuteTriggerAction(STrigger.Speed); }
            };
            mSelectNum = 0;
            mPrevSelectNum = 0;
        }

        protected override void OnEnter()
        {
            Player.vecAction = (Vector2) => Scroll(Vector2);
            Player.backAction = () =>
            {
                PlayCancelSound();
                owner.BackToHome();
            };
            ReplaceEnterAction(mSelectNum);
            mProperty = owner.mProperty;
            mPrevSelectNum = mSelectNum;
        }
        protected override void OnUpdate(float deltaTime)
        {
            SetPropertyText(mSelectNum);
            if (mSceneManager.CursolRect != null)
            {
                mSceneManager.CursolRect.anchoredPosition = new(-350, -(mSelectNum - (6 - 1) / 2.0f) * (540 / 6 * 2));
            }

            if (mSelectNum != mPrevSelectNum)
            {
                ReplaceEnterAction(mSelectNum);
            }
            mPrevSelectNum = mSelectNum;
        }
        protected override void OnExit()
        {

        }

        //ホーム画面の説明を管理
        void SetPropertyText(int v)
        {
            if (mProperty == null) return;
            string str = v switch
            {
                0 => "オートプレイの設定ができます。\n現在：" + (owner.PlayerConfig.AutoPlay ? "ON" : "OFF"),
                1 => "レーン反転の設定ができます。\n現在：" + (owner.PlayerConfig.LaneCahge ? "ON" : "OFF"),
                2 => "操作の左右反転の設定ができます。\n現在：" + (owner.PlayerConfig.LeftRightCahge ? "ON" : "OFF"),
                3 => "操作の上下反転の設定ができます。\n現在：" + (owner.PlayerConfig.UpDownCahge ? "ON" : "OFF"),
                4 => "デバイスの変更ができます。\n現在：" + (owner.PlayerConfig.InputDevice == Notes.InputDevice.Controller ? "コントローラー" : "キーボード"),
                5 => "ノーツ速度の設定ができます。\n現在：" + owner.PlayerConfig.NotesSpeed.ToString(),
                _ => string.Empty,
            };
            mProperty.SetText(str);
        }
        void ReplaceEnterAction(int value)
        {
            Player.enterAction = () =>
            {
                PlayEnterSound();
                mActions[value].Invoke();
            };
        }
    }
}