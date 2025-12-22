using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public class SingleState : IState
    {
        public SingleState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            ReserveNullActionList(1);
            Debug.Log("SingleState ready");
        }
        protected override void OnEnter()
        {
            //Actions = new List<Action>(1);
            layer = 0;
            deleteAction = null;
            PopupController.CreateInstance("シングルプレイを開始します。\nよろしいですか？", out deleteAction);
            ModeSelect.Player.backAction = () => mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Home);
        }
        protected override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
        }
        protected override void OnExit()
        {
            
        }
    }
}
