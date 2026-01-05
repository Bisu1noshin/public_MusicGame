using UnityEngine;
using System.Collections.Generic;
using System;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_Home : StateBase<SettingState, STrigger>
    {
        Action[] actions;
        ISettingState mOwner;
        int mSelectNum, mPrevSelectNum;
        PropertyController mProperty;
        public Setting_Home(SettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {
            mOwner = owner;
            actions = new Action[6]
            {
                () => { stateMachine.ExecuteTriggerAction(STrigger.Auto); },
                () => { stateMachine.ExecuteTriggerAction(STrigger.Lane); },
                () => { stateMachine.ExecuteTriggerAction(STrigger.LR); },
                () => { stateMachine.ExecuteTriggerAction(STrigger.UD); },
                () => { stateMachine.ExecuteTriggerAction(STrigger.Device); },
                () => { stateMachine.ExecuteTriggerAction(STrigger.Speed); }
            };
            
        }

        protected override void OnEnter()
        {
            Debug.Log("Setting_Home started");
            mSelectNum = 0;
            mPrevSelectNum = 0;
            Player.vecAction = (Vector2) => Scroll(Vector2);
            Player.backAction = () => mOwner.SceneManager.mStateMachine.ExecuteTriggerAction(Trigger.Home);
            Player.enterAction = actions[0];
            mProperty = mOwner.mProperty;
        }
        protected override void OnUpdate(float deltaTime)
        {
            SetPropertyText(mSelectNum);
            if (mOwner.SceneManager.CursolRect != null)
            {
                mOwner.SceneManager.CursolRect.anchoredPosition = new(-350, -(mSelectNum - (6 - 1) / 2.0f) * (540 / 6 * 2));
            }
            if (mSelectNum != mPrevSelectNum)
            {
                Player.enterAction = actions[mSelectNum];
            }
            mPrevSelectNum = mSelectNum;
        }
        protected override void OnExit()
        {

        }
        void Scroll(Vector2 v)
        {
            mSelectNum += v.y < 0.0f ? 1 : -1;
            if (mSelectNum < 0)
            {
                mSelectNum = 0;
            }
            else if (mSelectNum > 5)
            {
                mSelectNum = 5;
            }
        }

        //ホーム画面の説明を管理
        void SetPropertyText(int v)
        {
            if (mProperty == null) return;
            string str = v switch
            {
                0 => "オートプレイの設定ができます。\n現在：" + (mOwner.PlayerConfig.AutoPlay ? "ON" : "OFF"),
                1 => "レーン反転の設定ができます。\n現在：" + (mOwner.PlayerConfig.LaneCahge ? "ON" : "OFF"),
                2 => "操作の上下反転の設定ができます。\n現在：" + (mOwner.PlayerConfig.UpDownCahge ? "ON" : "OFF"),
                3 => "操作の左右反転の設定ができます。\n現在：" + (mOwner.PlayerConfig.LeftRightCahge ? "ON" : "OFF"),
                4 => "デバイスの変更ができます。\n現在：" + (mOwner.PlayerConfig.InputDevice == Notes.InputDevice.Controller ? "コントローラー" : "キーボード"),
                5 => "ノーツ速度の設定ができます。\n現在：" + mOwner.PlayerConfig.NotesSpeed.ToString(),
                _ => string.Empty,
            };
            mProperty.SetText(str);
        }
    }
}