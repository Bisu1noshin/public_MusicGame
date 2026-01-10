using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public class MultiState : IState
    {
        public MultiState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            ReserveNullActionList(1);
        }
        protected override void OnEnter()
        {
            InitAction();
            deleteAction = null;
            deleteAction += PopupController.CreateInstance("このモードは現在\n利用できません。");
            
            //deleteAction += Button.ButtonManager.CreateInstance(this, 0, 2, "1P", () => { Debug.Log("Multi Owner pressed"); }, false);
            //deleteAction += Button.ButtonManager.CreateInstance(this, 1, 2, "2P", () => { Debug.Log("Multi Client pressed"); }, false);

            
        }
        protected override void OnUpdate(float deltaTime)
        {
            
        }
        protected override void OnExit()
        {
            
        }
        void InitAction()
        {
            layer = 0;
            Player.vecAction = null;
            Player.backAction = () => mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Home);
            Player.backAction += deleteAction;
            Player.enterAction = () => PlayBeepSound();
        }
    }
}
