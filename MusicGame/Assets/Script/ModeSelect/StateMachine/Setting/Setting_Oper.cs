using UnityEngine;


namespace ModeSelect.StateMachine.Setting
{
    public enum OState
    {
        None = -1, Home, Lane, LR, UD
    }
    public enum OTrigger
    {
        Home, Lane, LR, UD
    }


    public class Setting_Oper : Kameda_StateParent<SettingState, STrigger>, IParentState
    {
        StateMachine<OState, OTrigger> mStateMachine;
        public Setting_Oper(SettingState owner, IStateMachine<STrigger> st) : base(owner, st)
        {

        }
        protected override void OnEnter()
        {
            SetupStates();
            
        }
        protected override void OnUpdate(float deltaTime)
        {
            
        }
        protected override void OnExit()
        {

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
    }

}