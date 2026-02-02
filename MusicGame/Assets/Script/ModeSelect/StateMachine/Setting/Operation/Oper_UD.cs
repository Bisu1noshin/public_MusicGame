using System;
using Unity.VisualScripting;

namespace ModeSelect.StateMachine.Setting.Operation
{
    public class Oper_UD : PopupAdmin<Setting_Oper, OTrigger>
    {
        bool instantBool;
        public Oper_UD(Setting_Oper owner, IStateMachine<OTrigger> st) : base(owner, st, true)
        {

        }
        protected override void OnEnter()
        {
            Player.enterAction = null;
            instantBool = owner.PlayerConfig.UpDownCahge;
            CreateSpeedPopupInstance(ref mPopup, "上下反転を変更します。");
            CreateCursor();
            Action change = () =>
            {
                instantBool = !instantBool;
            };
            Action ent = () =>
            {
                owner.PlayerConfig.UpDownCahge = instantBool;
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