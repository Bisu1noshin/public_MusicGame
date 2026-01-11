using Notes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public enum SState
    {
        None = -1, Home, Auto, Lane, LR, UD, Device, Speed
    }
    public enum STrigger
    {
        Home, Auto, Lane, LR, UD, Device, Speed
    }
    public class SettingState : Kameda_StateParent<ISceneManager, Trigger>, ISettingState
    {
        public NotesManagerPlayerConfig PlayerConfig { get; set; }
        public PropertyController mProperty { get; private set; }

        StateMachine<SState, STrigger> mStateMachine;
        
        public SettingState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            PlayerConfig = owner.GetPlayerConfig();
            
        }
        protected override void OnEnter()
        {
            owner.CreateCursol();
            deleteAction += Button.ButtonManager.CreateInstance(0, 6, "オートプレイ");
            deleteAction += Button.ButtonManager.CreateInstance(1, 6, "レーン反転");
            deleteAction += Button.ButtonManager.CreateInstance(2, 6, "上下反転");
            deleteAction += Button.ButtonManager.CreateInstance(3, 6, "左右反転");
            deleteAction += Button.ButtonManager.CreateInstance(4, 6, "デバイス変更");
            deleteAction += Button.ButtonManager.CreateInstance(5, 6, "ノーツ速度");
            (PropertyController, Action) tuple = PropertyController.CreateInstance();
            mProperty = tuple.Item1;
            deleteAction += tuple.Item2;
            InitStates();
        }
        protected override void OnUpdate(float deltaTime)
        {
            mStateMachine.Update(deltaTime);
        }
        protected override void OnExit()
        {
            owner.TryDeleteCursol();
            deleteAction?.Invoke();
            deleteAction = null;
        }

        void InitStates()
        {
            mStateMachine = new(SState.None, null);

            mStateMachine.SetupState(SState.Home, new Setting.Setting_Home(this, mStateMachine));
            mStateMachine.SetupState(SState.Auto, new Setting.Setting_Auto(this, mStateMachine));
            mStateMachine.SetupState(SState.Lane, new Setting.Setting_Lane(this, mStateMachine));
            mStateMachine.SetupState(SState.LR, new Setting.Setting_LR(this, mStateMachine));
            mStateMachine.SetupState(SState.UD, new Setting.Setting_UD(this, mStateMachine));
            mStateMachine.SetupState(SState.Device, new Setting.Setting_Device(this, mStateMachine));
            mStateMachine.SetupState(SState.Speed, new Setting.Setting_Speed(this, mStateMachine));

            mStateMachine.AddTransition(SState.None, SState.Home, STrigger.Home);
            mStateMachine.AddTransition(SState.Home, SState.Auto, STrigger.Auto);
            mStateMachine.AddTransition(SState.Auto, SState.Home, STrigger.Home);

            mStateMachine.AddTransition(SState.Home, SState.Lane, STrigger.Lane);
            mStateMachine.AddTransition(SState.Lane, SState.Home, STrigger.Home);

            mStateMachine.AddTransition(SState.Home, SState.LR, STrigger.LR);
            mStateMachine.AddTransition(SState.LR, SState.Home, STrigger.Home);

            mStateMachine.AddTransition(SState.Home, SState.UD, STrigger.UD);
            mStateMachine.AddTransition(SState.UD, SState.Home, STrigger.Home);

            mStateMachine.AddTransition(SState.Home, SState.Device, STrigger.Device);
            mStateMachine.AddTransition(SState.Device, SState.Home, STrigger.Home);

            mStateMachine.AddTransition(SState.Home, SState.Speed, STrigger.Speed);
            mStateMachine.AddTransition(SState.Speed, SState.Home, STrigger.Home);

            mStateMachine.ExecuteTriggerAction(STrigger.Home);
        }
        public void BackToHome()
        {
            stateMachine.ExecuteTriggerAction(Trigger.Home);
        }
    }

    public interface ISettingState
    {
        NotesManagerPlayerConfig PlayerConfig { get; set; }
        PropertyController mProperty { get; }
        void BackToHome();
    }
}