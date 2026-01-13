using ModeSelect.StateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModeSelect.StateMachine
{
    public class HomeState : Kameda_StateParent<ISceneManager, Trigger>
    {
        PropertyController mProperty;
        int mPrevSelectNum;
        string[] explaination = new string[]
        {
            "シングルプレイで遊びます",
            "マルチプレイで遊びます",
            "ゲームプレイの設定ができます",
            "タイトルに戻ります"
        };
        public HomeState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            mActions = new()
            {
                () => stateMachine.ExecuteTriggerAction(Trigger.Single),
                () => stateMachine.ExecuteTriggerAction(Trigger.Multi),
                () => stateMachine.ExecuteTriggerAction(Trigger.Setting),
                () => SceneManager.LoadScene("Ooo_Title")
            };
            
        }
        protected override void OnEnter()
        {
            InitAction();
            
            deleteAction += Button.ButtonManager.CreateInstance(0, 4, "シングルプレイ");
            deleteAction += Button.ButtonManager.CreateInstance(1, 4, "マルチプレイ");
            deleteAction += Button.ButtonManager.CreateInstance(2, 4, "設定");
            deleteAction += Button.ButtonManager.CreateInstance(3, 4, "タイトルに戻る");
            (PropertyController, Action) tuple = PropertyController.CreateInstance();
            mProperty = tuple.Item1;
            deleteAction += tuple.Item2;
            owner.CreateCursol();
        }

        protected override void OnUpdate(float deltaTime)
        {
            mProperty.SetText(explaination[mSelectNum]);
            if (owner.CursolRect) owner.CursolRect.anchoredPosition = new(-350.0f, -(mSelectNum - (4 - 1) / 2.0f) * (540 / 4 * 2));
            if (mSelectNum != mPrevSelectNum)
            {
                ReplaceEnterAction(mSelectNum);
            }
            mPrevSelectNum = mSelectNum;
        }
        protected override void OnExit()
        {
            owner.TryDeleteCursol();
            deleteAction?.Invoke();

        }
        void InitAction()
        {
            Player.vecAction = (vector2) => Scroll(vector2);
            deleteAction = null;
            Player.backAction = () => PlayBeepSound();
            Player.enterAction = mActions[mSelectNum];
            mPrevSelectNum = mSelectNum;
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
