using ModeSelect.StateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;

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
            
            deleteAction += Button.ButtonManager.CreateInstance(this, 0, 4, "シングルプレイ", () => { Debug.Log("Button Selected 1"); }, false);
            deleteAction += Button.ButtonManager.CreateInstance(this, 1, 4, "マルチプレイ", () => { Debug.Log("Button Selected 2"); }, false);
            deleteAction += Button.ButtonManager.CreateInstance(this, 2, 4, "設定", () => { Debug.Log("Button Selected 3"); }, false);
            deleteAction += Button.ButtonManager.CreateInstance(this, 3, 4, "タイトルに戻る", () => { Debug.Log("Button Selected 4"); }, false);
        }

        protected override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            mOwner.CursolRect.anchoredPosition = new(-350.0f, -(SelectNum[0] - (4 - 1) / 2.0f) * 250.0f);
        }
        protected override void OnExit()
        {

        }
        void InitAction()
        {
            layer = 0;
            ModeSelect.Player.vecAction = (vector2) => Scroll(vector2);
            
            ModeSelect.Player.backAction = null;
        }
    }
}
