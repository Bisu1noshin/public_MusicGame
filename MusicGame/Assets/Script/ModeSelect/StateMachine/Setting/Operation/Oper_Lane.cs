using System;

namespace ModeSelect.StateMachine.Setting.Operation
{
    public class Oper_Lane : PopupAdmin<Setting_Oper, OTrigger>
    {
        bool instantBool;
        public Oper_Lane(Setting_Oper owner, IStateMachine<OTrigger> st) : base(owner, st, true)
        {

        }
        protected override void OnEnter()
        {
            Player.enterAction = null;
            instantBool = owner.PlayerConfig.LaneCahge;
            CreateSpeedPopupInstance(ref mPopup, "レーン反転を変更します。よろしいですか？");
            CreateCursor();
            Action change = () =>
            {
                instantBool = !instantBool;
            };
            Action ent = () =>
            {
                owner.PlayerConfig.LaneCahge = instantBool;
                stateMachine.ExecuteTriggerAction(OTrigger.Home);
            };
            Action back = () =>
            {
                stateMachine.ExecuteTriggerAction(OTrigger.Home);
            };
            SetChangeElementAction(change, change);
            SetEnterAndCancelAction(ent, back);
            Player.vecAction = (vec) => ChangeState(vec);
        }
        protected override void OnUpdate(float deltaTime)
        {
            mPopup.SetValue(instantBool ? "ON" : "OFF", 72);
        }
        protected override void OnExit()
        {
            mSceneManager.TryDeletePopupCursor();
            deleteAction?.Invoke();
            deleteAction = null;
        }
    }
}