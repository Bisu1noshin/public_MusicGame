using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public class SingleState : IState
    {
        public SingleState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {

        }
        protected override void OnEnter()
        {
            Actions = new List<Action>(1);
            deleteAction += () => { Debug.Log("CreateInstance"); };
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
