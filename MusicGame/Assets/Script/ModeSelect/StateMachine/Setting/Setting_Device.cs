using UnityEngine;
using System;

namespace ModeSelect.StateMachine.Setting
{
    public class Setting_Device : PopupAdmin<SettingState, STrigger>
    {
        bool instantBool;
        public Setting_Device(SettingState owner, IStateMachine<STrigger> st) : base(owner, st, true)
        {
        }

        protected override void OnEnter()
        {
            Player.enterAction = null;
            instantBool = owner.PlayerConfig.InputDevice == Notes.InputDevice.Controller;
            CreateSpeedPopupInstance(ref mPopup, "使用デバイスを変更します。よろしいですか？");
            CreateCursor();
            Action change = () =>
            {
                instantBool = !instantBool;
            };
            Action ent = () =>
            {
                //プレイヤーを作り直す
                owner.PlayerConfig.InputDevice = instantBool ? Notes.InputDevice.Controller : Notes.InputDevice.KyeBord;
                Player.destroyAction?.Invoke();
                GameObject.Instantiate(Resources.Load<GameObject>("ModeSelect/Player"));
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
            mPopup.SetValue(instantBool ? "コントローラー" : "キーボード", 64);
        }
        protected override void OnExit()
        {
            mSceneManager.TryDeletePopupCursor();
            deleteAction?.Invoke();
            deleteAction = null;
        }
    }
}