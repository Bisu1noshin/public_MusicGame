using Notes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModeSelect.StateMachine.Setting
{
    public enum OState
    {
        None = -1, Home, Lane, LR, UD, Intro
    }
    public enum OTrigger
    {
        Home, Lane, LR, UD, Intro
    }


    public class Setting_Oper : Kameda_StateBase<SettingState, STrigger>
    {
        StateMachine<OState, OTrigger> mStateMachine;
        PropertyController mProperty;
        public NotesManagerPlayerConfig PlayerConfig { get; set; }
        public Setting_Oper(SettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {
            PlayerConfig = owner.PlayerConfig;
        }
        protected override void OnEnter()
        {
            deleteAction += CreateButtonInstance(0, 4, "レーン反転");
            deleteAction += CreateButtonInstance(1, 4, "左右反転");
            deleteAction += CreateButtonInstance(2, 4, "上下反転");
            deleteAction += CreateButtonInstance(3, 4, "戻る");
            CreatePropertyInstance(ref mProperty);
            //mSceneManager.CreateCursor();
            SetupStates();
        }
        protected override void OnUpdate(float deltaTime)
        {
            mStateMachine.Update(deltaTime);
        }
        protected override void OnExit()
        {
            deleteAction?.Invoke();
            deleteAction = null;
            mSceneManager.TryDeleteCursor();
            mStateMachine = null;
        }
        public void BackToHome()
        {
            stateMachine.ExecuteTriggerAction(STrigger.Home);
        }
        void SetupStates()
        {
            mStateMachine = new(OState.None, null);
            mStateMachine.SetupState(OState.Home, new Operation.Oper_Home(this, mStateMachine));
            mStateMachine.SetupState(OState.Lane, new Operation.Oper_Lane(this, mStateMachine));
            mStateMachine.SetupState(OState.LR, new Operation.Oper_LR(this, mStateMachine));
            mStateMachine.SetupState(OState.UD, new Operation.Oper_UD(this, mStateMachine));

            mStateMachine.AddTransition(OState.None, OState.Home, OTrigger.Home);
            mStateMachine.AddTransition(OState.Home, OState.Lane, OTrigger.Lane);
            mStateMachine.AddTransition(OState.Home, OState.LR, OTrigger.LR);
            mStateMachine.AddTransition(OState.Home, OState.UD, OTrigger.UD);

            mStateMachine.AddTransition(OState.Lane, OState.Home, OTrigger.Home);
            mStateMachine.AddTransition(OState.LR, OState.Home, OTrigger.Home);
            mStateMachine.AddTransition(OState.UD, OState.Home, OTrigger.Home);

            mStateMachine.ExecuteTriggerAction(OTrigger.Home);
        }

        public PropertyController GetProperty() => mProperty;
    }

}