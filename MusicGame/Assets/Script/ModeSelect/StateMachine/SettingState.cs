using Notes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public enum SState
    {
        Home,
        Auto,
        Lane,
        LR,
        UD,
        Device,
        Speed
    }
    public enum STrigger
    {
        Home,
        Auto,
        Lane,
        LR,
        UD,
        Device,
        Speed
    }
    public class SettingState : IState, ISettingState
    {
        public NotesManagerPlayerConfig PlayerConfig { get; set; }
        public PropertyController mProperty { get; private set; }
        public StateMachine<SState, STrigger> StateMachine { get; set; }

        public ISceneManager SceneManager => mOwner;
        
        public SettingState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            PlayerConfig = mOwner.GetNotesManager().PlayerConfig;
            InitStates();
        }
        protected override void OnEnter()
        {
            deleteAction += Button.ButtonManager.CreateInstance(this, 0, 6, "オートプレイ");
            deleteAction += Button.ButtonManager.CreateInstance(this, 1, 6, "レーン反転");
            deleteAction += Button.ButtonManager.CreateInstance(this, 2, 6, "上下反転");
            deleteAction += Button.ButtonManager.CreateInstance(this, 3, 6, "左右反転");
            deleteAction += Button.ButtonManager.CreateInstance(this, 4, 6, "デバイス変更");
            deleteAction += Button.ButtonManager.CreateInstance(this, 5, 6, "ノーツ速度");
            (PropertyController, Action) tuple = PropertyController.CreateInstance();
            mProperty = tuple.Item1;
            deleteAction += tuple.Item2;
        }
        protected override void OnUpdate(float deltaTime)
        {
            StateMachine.Update(deltaTime);
        }
        protected override void OnExit()
        {
            mOwner.TryDeleteCursol();
            deleteAction?.Invoke();
            deleteAction = null;
        }

        void InitStates()
        {
            StateMachine = new(SState.Home, new Setting.Setting_Home(this, StateMachine));
            StateMachine.SetupState(SState.Auto, new Setting.Setting_Auto(this, StateMachine));
            StateMachine.SetupState(SState.Auto, new Setting.Setting_Lane(this, StateMachine));
            StateMachine.SetupState(SState.Auto, new Setting.Setting_LR(this, StateMachine));
            StateMachine.SetupState(SState.Auto, new Setting.Setting_UD(this, StateMachine));
            StateMachine.SetupState(SState.Auto, new Setting.Setting_Device(this, StateMachine));
            StateMachine.SetupState(SState.Auto, new Setting.Setting_Speed(this, StateMachine));

            StateMachine.AddTransition(SState.Home, SState.Auto, STrigger.Auto);
            StateMachine.AddTransition(SState.Auto, SState.Home, STrigger.Home);

            StateMachine.AddTransition(SState.Home, SState.Lane, STrigger.Lane);
            StateMachine.AddTransition(SState.Lane, SState.Home, STrigger.Lane);

            StateMachine.AddTransition(SState.Home, SState.LR, STrigger.LR);
            StateMachine.AddTransition(SState.LR, SState.Home, STrigger.LR);

            StateMachine.AddTransition(SState.Home, SState.UD, STrigger.UD);
            StateMachine.AddTransition(SState.UD, SState.Home, STrigger.Home);

            StateMachine.AddTransition(SState.Home, SState.Device, STrigger.Device);
            StateMachine.AddTransition(SState.Device, SState.Home, STrigger.Home);

            StateMachine.AddTransition(SState.Home, SState.Speed, STrigger.Speed);
            StateMachine.AddTransition(SState.Speed, SState.Home, STrigger.Home);
        }
    }

    public interface ISettingState
    {
        StateMachine<SState, STrigger> StateMachine { get; set; }
        ISceneManager SceneManager { get; }
        NotesManagerPlayerConfig PlayerConfig { get; set; }
        PropertyController mProperty { get; }
    }
}