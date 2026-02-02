using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public enum MState {
        None, Home, Enter
    }
    public enum MTrigger
    {
        Home, Enter
    }
    public class MultiState : Kameda_StateBase<ISceneManager, Trigger>, IMultiState
    {
        StateMachine<MState, MTrigger> mStateMachine;
        public MultiState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            
        }
        protected override void OnEnter()
        {
            InitStateMachine();
        }
        protected override void OnUpdate(float deltaTime)
        {
            mStateMachine.Update(deltaTime);
        }
        protected override void OnExit()
        {
            deleteAction?.Invoke();
            mStateMachine = null;
        }
        public void BackToHome()
        {
            stateMachine.ExecuteTriggerAction(Trigger.Home);
        }
        void InitStateMachine()
        {
            mStateMachine = new(MState.None, null);
            mStateMachine.SetupState(MState.Home, new Multi.Multi_Home(this, mStateMachine));
            mStateMachine.SetupState(MState.Enter, new Multi.Multi_Enter(this, mStateMachine));

            mStateMachine.AddTransition(MState.None, MState.Home, MTrigger.Home);
            mStateMachine.AddTransition(MState.Home, MState.Enter, MTrigger.Enter);
            mStateMachine.AddTransition(MState.Enter, MState.Home, MTrigger.Home);

            mStateMachine.ExecuteTriggerAction(MTrigger.Home);
        }
    }
    public interface IMultiState
    {
        void BackToHome();
    }
}
