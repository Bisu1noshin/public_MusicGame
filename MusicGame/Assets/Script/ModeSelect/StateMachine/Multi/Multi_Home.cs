using ModeSelect.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeSelect.StateMachine.Multi
{
    public class Multi_Home : PopupAdmin<IMultiState, MTrigger>
    {
        public Multi_Home(IMultiState _owner, IStateMachine<MTrigger> _st) : base(_owner, _st, false, CursorState.EnterORCancel)
        {

        }
        protected override void OnEnter()
        {
            Player.enterAction = null;
            deleteAction += CreatePopupInstance("マルチプレイはSteamの機能を\n使用します。\nSteamの起動が完了した場合は\n次に進んでください。", 72);
            CreateCursor();
            Action ent = () =>
            {
                stateMachine.ExecuteTriggerAction(MTrigger.Enter);
            };
            Action back = () =>
            {
                mSceneManager.TryDeletePopupCursor(); 
                deleteAction?.Invoke();
                deleteAction = null;
                owner.BackToHome();
            };
            SetEnterAndCancelAction(ent, back);
            Player.vecAction = (vec) => ChangeState(vec);
        }
        protected override void OnUpdate(float deltaTime)
        {
            //pass
        }
        protected override void OnExit()
        {
            mSceneManager.TryDeletePopupCursor();
            deleteAction?.Invoke();
            deleteAction = null;
        }
    }
}
