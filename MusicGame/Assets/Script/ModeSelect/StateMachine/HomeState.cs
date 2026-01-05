using ModeSelect.StateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModeSelect.StateMachine
{
    public class HomeState : Kameda_StateParent
    {
        PropertyController mProperty;
        int mPrevSelectNum = 0;
        readonly string[] explaination = new string[]
        {
            "シングルプレイで遊びます",
            "マルチプレイで遊びます",
            "ゲームプレイの設定ができます",
            "タイトルに戻ります"
        };
        public HomeState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            mActions = null;
        }
        protected override void OnEnter()
        {
            InitAction();
            
            deleteAction += Button.ButtonManager.CreateInstance(this, 0, 4, "シングルプレイ");
            deleteAction += Button.ButtonManager.CreateInstance(this, 1, 4, "マルチプレイ");
            deleteAction += Button.ButtonManager.CreateInstance(this, 2, 4, "設定");
            deleteAction += Button.ButtonManager.CreateInstance(this, 3, 4, "タイトルに戻る");
            (PropertyController, Action) tuple = PropertyController.CreateInstance();
            mProperty = tuple.Item1;
            deleteAction += tuple.Item2;
            mOwner.CreateCursol();

        }

        protected override void OnUpdate(float deltaTime)
        {
            if (stateMachine == null) Debug.Log("StateMachine is NULL!!");
            mProperty?.SetText(explaination[mSelectNum]);
            if (mOwner.CursolRect != null) mOwner.CursolRect.anchoredPosition = new(-350.0f, -(mSelectNum - (4 - 1) / 2.0f) * (540 / 4 * 2));
            Player.backAction ??= () => PlayBeepSound();

            if (mSelectNum != mPrevSelectNum) Player.enterAction = mActions[mSelectNum];
            mPrevSelectNum = mSelectNum;
        }
        protected override void OnExit()
        {
            deleteAction?.Invoke();
            deleteAction = null;
            mOwner.TryDeleteCursol();
        }
        void InitAction()
        {
            
            mSelectNum = 0;
            mPrevSelectNum = 0;
            mActions = new List<Action>()
            {
                () => stateMachine.ExecuteTriggerAction(Trigger.Single),
                () => stateMachine.ExecuteTriggerAction(Trigger.Multi),
                () => stateMachine.ExecuteTriggerAction(Trigger.Setting),
                () => SceneManager.LoadScene("Ooo_Title")
            };
            Player.vecAction = (vector2) => Scroll(vector2);
            Player.backAction = null;
            Player.enterAction = mActions[0];
        }
    }
}
