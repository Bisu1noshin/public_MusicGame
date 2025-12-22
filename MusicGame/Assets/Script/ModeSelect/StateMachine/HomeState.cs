using ModeSelect.StateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModeSelect
{
    public class HomeState : IState
    {
        public HomeState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            ReserveNullActionList(4);
        }
        protected override void OnEnter()
        {
            InitAction();
            deleteAction = null;
            deleteAction += Button.ButtonManager.CreateInstance(this, 0, 4, "シングルプレイ", () => { mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Single); }, false);
            deleteAction += Button.ButtonManager.CreateInstance(this, 1, 4, "マルチプレイ", () => { mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Multi); }, false);
            deleteAction += Button.ButtonManager.CreateInstance(this, 2, 4, "設定", () => { mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Setting); }, false);
            deleteAction += Button.ButtonManager.CreateInstance(this, 3, 4, "タイトルに戻る", () => { SceneManager.LoadScene("Ooo_Title"); }, false);
            mOwner.CreateCursol();
            
        }

        protected override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            mOwner.CursolRect.anchoredPosition = new(-350.0f, -(SelectNum[0] - (4 - 1) / 2.0f) * 250.0f);
            Player.backAction = () => PlayBeepSound();
        }
        protected override void OnExit()
        {
            mOwner.DeleteCursol();
        }
        void InitAction()
        {
            layer = 0;
            ModeSelect.Player.vecAction = (vector2) => Scroll(vector2);
            
            ModeSelect.Player.backAction = null;
        }
    }
}
