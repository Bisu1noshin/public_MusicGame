using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public class MultiState : Kameda_StateParent<ISceneManager, Trigger>
    {
        public MultiState(ISceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {

        }
        protected override void OnEnter()
        {
            InitAction();
            deleteAction = null;
            deleteAction += PopupController.CreateInstance("このモードは現在\n利用できません。");
        }
        protected override void OnUpdate(float deltaTime)
        {
            Player.backAction ??= () =>
            {
                PlayCancelSound();
                stateMachine.ExecuteTriggerAction(Trigger.Home);
            };
            Player.enterAction ??= () => PlayBeepSound();
        }
        protected override void OnExit()
        {
            deleteAction?.Invoke();
            deleteAction = null;
        }
        void InitAction()
        {
            Player.vecAction = null;
            Player.backAction = () =>
            {
                PlayCancelSound();
                stateMachine.ExecuteTriggerAction(Trigger.Home);
            };
            Player.enterAction = () => PlayBeepSound();
        }
    }
}
