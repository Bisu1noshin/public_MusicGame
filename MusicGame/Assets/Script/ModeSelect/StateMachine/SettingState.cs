using Notes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public class SettingState : IState
    {
        
        public SettingState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            ReserveNullActionList(1);
        }
        protected override void OnEnter()
        {
            
            ModeSelect.Player.enterAction = () => { Debug.Log("A pressed"); };
            ModeSelect.Player.backAction = () => mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Home);
        }
        protected override void OnUpdate(float deltaTime)
        {
            
        }
        protected override void OnExit()
        {
            
        }
    }
}
