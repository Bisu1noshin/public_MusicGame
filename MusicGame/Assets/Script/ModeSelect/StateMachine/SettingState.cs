using Notes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public enum SState
    {
        None = -1, Home, Auto, Lane, LR, UD, Device, Speed, Oper
    }
    public enum STrigger
    {
        Home, Auto, Lane, LR, UD, Device, Speed, Oper
    }
    public class SettingState : Kameda_StateBase<ISceneManager, Trigger>, IParentState
    {
        public NotesManagerPlayerConfig PlayerConfig { get; set; }
        PropertyController mProperty;

        StateMachine<SState, STrigger> mStateMachine;
        SState mPrevState;
        
        public SettingState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            PlayerConfig = owner.GetPlayerConfig();
            
        }
        protected override void OnEnter()
        {
            InitButtons();
            mPrevState = SState.Home;
            InitStates();
        }
        protected override void OnUpdate(float deltaTime)
        {
            mStateMachine.Update(deltaTime);
            if (mPrevState != mStateMachine.GetState())
            {
                if (mPrevState == SState.Home && mStateMachine.GetState() == SState.Oper)
                {
                    //owner.TryDeleteCursor();
                    deleteAction?.Invoke();
                    deleteAction = null;
                }
                if (mPrevState == SState.Oper && mStateMachine.GetState() == SState.Home)
                {
                    InitButtons();
                }
            }
            mPrevState = mStateMachine.GetState();
        }
        protected override void OnExit()
        {
            owner.TryDeleteCursor();
            deleteAction?.Invoke();
            deleteAction = null;
            mStateMachine = null;
        }

        void InitStates()
        {
            mStateMachine = new(SState.None, null);

            mStateMachine.SetupState(SState.Home, new Setting.Setting_Home(this, mStateMachine));
            mStateMachine.SetupState(SState.Auto, new Setting.Setting_Auto(this, mStateMachine));
            mStateMachine.SetupState(SState.Device, new Setting.Setting_Device(this, mStateMachine));
            mStateMachine.SetupState(SState.Speed, new Setting.Setting_Speed(this, mStateMachine));
            mStateMachine.SetupState(SState.Oper, new Setting.Setting_Oper(this, mStateMachine));

            mStateMachine.AddTransition(SState.None, SState.Home, STrigger.Home);

            mStateMachine.AddTransition(SState.Home, SState.Auto, STrigger.Auto);
            mStateMachine.AddTransition(SState.Auto, SState.Home, STrigger.Home);

            mStateMachine.AddTransition(SState.Home, SState.Device, STrigger.Device);
            mStateMachine.AddTransition(SState.Device, SState.Home, STrigger.Home);

            mStateMachine.AddTransition(SState.Home, SState.Speed, STrigger.Speed);
            mStateMachine.AddTransition(SState.Speed, SState.Home, STrigger.Home);

            mStateMachine.AddTransition(SState.Home, SState.Oper, STrigger.Oper);
            mStateMachine.AddTransition(SState.Oper, SState.Home, STrigger.Home);

            mStateMachine.ExecuteTriggerAction(STrigger.Home);
        }
        public void BackToHome()
        {
            stateMachine.ExecuteTriggerAction(Trigger.Home);
        }
        void InitButtons()
        {
            owner.CreateCursor();
            deleteAction += CreateButtonInstance(0, 5, "オートプレイ");
            deleteAction += CreateButtonInstance(1, 5, "デバイス変更");
            deleteAction += CreateButtonInstance(2, 5, "ノーツ速度");
            deleteAction += CreateButtonInstance(3, 5, "操作");
            deleteAction += CreateButtonInstance(4, 5, "戻る");
            CreatePropertyInstance(ref mProperty);
        }
        public PropertyController GetProperty()
        {
            return mProperty;
        }
    }
}