using ModeSelect.StateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModeSelect.StateMachine
{
    public class HomeState : HomeStateAbstract<ISceneManager, Trigger>
    {
        PropertyController mProperty;
        string[] explaination = new string[]
        {
            "シングルプレイで遊びます",
            "マルチプレイで遊びます",
            "ゲームプレイの設定ができます",
            "ゲーム制作者・作曲者を表示します",
            "タイトルに戻ります"
        };
        public HomeState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            mActions = new()
            {
                () => stateMachine.ExecuteTriggerAction(Trigger.Single),
                () => stateMachine.ExecuteTriggerAction(Trigger.Multi),
                () => stateMachine.ExecuteTriggerAction(Trigger.Setting),
                () => { owner.Resource.ReleaseAll?.Invoke(); SceneManager.LoadScene("Onishi_Credit"); },
                () => { owner.Resource.ReleaseAll?.Invoke(); SceneManager.LoadScene("Ooo_Title");  }
            };
            
        }
        protected override void OnEnter()
        {
            InitAction();
            
            deleteAction += CreateButtonInstance(0, 5, "シングルプレイ");
            deleteAction += CreateButtonInstance(1, 5, "マルチプレイ");
            deleteAction += CreateButtonInstance(2, 5, "設定");
            deleteAction += CreateButtonInstance(3, 5, "クレジット");
            deleteAction += CreateButtonInstance(4, 5, "タイトルに戻る");
            
            CreatePropertyInstance(ref mProperty);
            owner.CreateCursor();
        }

        protected override void OnUpdate(float deltaTime)
        {
            mProperty.SetText(explaination[mSelectNum]);
            mSceneManager.TrySetCursorPos(mSelectNum, 5);
            if (mSelectNum != mPrevSelectNum)
            {
                ReplaceEnterAction(mSelectNum);
            }
            mPrevSelectNum = mSelectNum;
        }
        protected override void OnExit()
        {
            owner.TryDeleteCursor();
            deleteAction?.Invoke();

        }
        void InitAction()
        {
            Player.vecAction = (vector2) => Scroll(vector2);
            deleteAction = null;
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
