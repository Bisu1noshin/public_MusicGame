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
<<<<<<< HEAD
    public class SettingState : Kameda_StateParent<ISceneManager, Trigger>, ISettingState
=======
    public class SettingState : StateParent, ISettingState
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
    {
        public NotesManagerPlayerConfig PlayerConfig { get; set; }
        public PropertyController mProperty { get; private set; }

        StateMachine<SState, STrigger> mStateMachine;
        
        public SettingState(ISceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            PlayerConfig = mSceneManager.GetPlayerConfig();
        }
        protected override void OnEnter()
        {
<<<<<<< HEAD
            InitStates();
            mSceneManager.CreateCursol();
            deleteAction += Button.ButtonManager.CreateInstance(0, 6, "オートプレイ");
            deleteAction += Button.ButtonManager.CreateInstance(1, 6, "レーン反転");
            deleteAction += Button.ButtonManager.CreateInstance(2, 6, "左右反転");
            deleteAction += Button.ButtonManager.CreateInstance(3, 6, "上下反転");
            deleteAction += Button.ButtonManager.CreateInstance(4, 6, "デバイス変更");
            deleteAction += Button.ButtonManager.CreateInstance(5, 6, "ノーツ速度");
=======
            deleteAction = null;
            deleteAction += Button.ButtonManager.CreateInstance(this, 0, 6, "オートプレイ");
            deleteAction += Button.ButtonManager.CreateInstance(this, 1, 6, "レーン反転");
            deleteAction += Button.ButtonManager.CreateInstance(this, 2, 6, "上下反転");
            deleteAction += Button.ButtonManager.CreateInstance(this, 3, 6, "左右反転");
            deleteAction += Button.ButtonManager.CreateInstance(this, 4, 6, "デバイス変更");
            deleteAction += Button.ButtonManager.CreateInstance(this, 5, 6, "ノーツ速度");
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
            (PropertyController, Action) tuple = PropertyController.CreateInstance();
            mProperty = tuple.Item1;
            deleteAction += tuple.Item2;

            mStateMachine.ExecuteTriggerAction(STrigger.Home);
        }
        protected override void OnUpdate(float deltaTime)
        {
<<<<<<< HEAD
            mStateMachine.Update(deltaTime);
=======
            StateMachine.Update(deltaTime);
            Debug.Log($"Current_SState : {StateMachine.GetState()}");
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
        }
        protected override void OnExit()
        {
            mSceneManager.TryDeleteCursol();
            deleteAction?.Invoke();
<<<<<<< HEAD
            deleteAction = null;
            mStateMachine = null;
=======
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
        }

        void InitStates()
        {
<<<<<<< HEAD
            mStateMachine = new(SState.None, null);
=======
            StateMachine = new(SState.Home, new Setting.Setting_Home(this, StateMachine));
            StateMachine.SetupState(SState.Auto, new Setting.Setting_Auto(this, StateMachine));
            StateMachine.SetupState(SState.Lane, new Setting.Setting_Lane(this, StateMachine));
            StateMachine.SetupState(SState.LR, new Setting.Setting_LR(this, StateMachine));
            StateMachine.SetupState(SState.UD, new Setting.Setting_UD(this, StateMachine));
            StateMachine.SetupState(SState.Device, new Setting.Setting_Device(this, StateMachine));
            StateMachine.SetupState(SState.Speed, new Setting.Setting_Speed(this, StateMachine));
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)

            mStateMachine.SetupState(SState.Home, new Setting.Setting_Home(this, mStateMachine));
            mStateMachine.SetupState(SState.Auto, new Setting.Setting_Auto(this, mStateMachine));
            mStateMachine.SetupState(SState.Lane, new Setting.Setting_Lane(this, mStateMachine));
            mStateMachine.SetupState(SState.LR, new Setting.Setting_LR(this, mStateMachine));
            mStateMachine.SetupState(SState.UD, new Setting.Setting_UD(this, mStateMachine));
            mStateMachine.SetupState(SState.Device, new Setting.Setting_Device(this, mStateMachine));
            mStateMachine.SetupState(SState.Speed, new Setting.Setting_Speed(this, mStateMachine));

            mStateMachine.AddTransition(SState.Home, SState.Auto, STrigger.Auto);
            mStateMachine.AddTransition(SState.Home, SState.Lane, STrigger.Lane);
            mStateMachine.AddTransition(SState.Home, SState.LR, STrigger.LR);
            mStateMachine.AddTransition(SState.Home, SState.UD, STrigger.UD);
            mStateMachine.AddTransition(SState.Home, SState.Device, STrigger.Device);
            mStateMachine.AddTransition(SState.Home, SState.Speed, STrigger.Speed);

            mStateMachine.AddTransition(SState.None, SState.Home, STrigger.Home);
            mStateMachine.AddTransition(SState.Auto, SState.Home, STrigger.Home);
            mStateMachine.AddTransition(SState.Lane, SState.Home, STrigger.Home);
            mStateMachine.AddTransition(SState.LR, SState.Home, STrigger.Home);
            mStateMachine.AddTransition(SState.UD, SState.Home, STrigger.Home);
            mStateMachine.AddTransition(SState.Device, SState.Home, STrigger.Home);            
            mStateMachine.AddTransition(SState.Speed, SState.Home, STrigger.Home);

            
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