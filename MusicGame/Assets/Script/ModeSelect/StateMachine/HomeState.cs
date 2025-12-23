using ModeSelect.StateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModeSelect
{
    public class HomeState : IState
    {
        PropertyController mProperty;
        string[] explaination = new string[]
        {
            "シングルプレイで遊びます",
            "マルチプレイで遊びます",
            "ゲームプレイの設定ができます",
            "タイトルに戻ります"
        };
        public HomeState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            ReserveNullActionList(4);
        }
        protected override void OnEnter()
        {
            InitAction();
            
            deleteAction += Button.ButtonManager.CreateInstance(this, 0, 4, "シングルプレイ", () => { mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Single); });
            deleteAction += Button.ButtonManager.CreateInstance(this, 1, 4, "マルチプレイ", () => { mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Multi); });
            deleteAction += Button.ButtonManager.CreateInstance(this, 2, 4, "設定", () => { mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Setting); });
            deleteAction += Button.ButtonManager.CreateInstance(this, 3, 4, "タイトルに戻る", () => { SceneManager.LoadScene("Ooo_Title"); });
            (PropertyController, Action) tuple = PropertyController.CreateInstance();
            mProperty = tuple.Item1;
            deleteAction += tuple.Item2;
            mOwner.CreateCursol();
        }

        protected override void OnUpdate(float deltaTime)
        {
            ReplaceEnterAction(Actions[SelectNum[0]]);
            mProperty.SetText(explaination[SelectNum[0]]);
            mOwner.CursolRect.anchoredPosition = new(-350.0f, -(SelectNum[0] - (4 - 1) / 2.0f) * (540 / 4 * 2));
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
            deleteAction = null;
        }
    }
}
