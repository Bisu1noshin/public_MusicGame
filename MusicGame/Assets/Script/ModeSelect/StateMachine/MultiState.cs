using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public class MultiState : Kameda_StateParent<ISceneManager, Trigger>
    {
        public MultiState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            
        }
        protected override void OnEnter()
        {
            deleteAction = null;
            deleteAction += PopupController.CreateInstance("このモードは現在\n利用できません。");
            Player.enterAction = () => PlayBeepSound();
            Player.backAction = () =>
            {
                PlayCancelSound();
                stateMachine.ExecuteTriggerAction(Trigger.Home);
            };
            Player.vecAction = null;
        }
        protected override void OnUpdate(float deltaTime)
        {
            
        }
        protected override void OnExit()
        {
            deleteAction?.Invoke();
        }
    }
}
