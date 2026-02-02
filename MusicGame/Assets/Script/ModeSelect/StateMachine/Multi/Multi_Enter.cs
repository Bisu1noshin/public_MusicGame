using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeSelect.StateMachine.Multi
{
    public class Multi_Enter : PopupAdmin<IMultiState, MTrigger>
    {
        public Multi_Enter(IMultiState _owner, IStateMachine<MTrigger> _st) : base(_owner, _st, false, CursorState.EnterORCancel)
        {

        }
        protected override void OnEnter()
        {
            Player.enterAction = null;
            deleteAction += CreatePopupInstance("オンラインモードを開始しますか？");
            CreateCursor();
            Action ent = () =>
            {
                //目的のシーンに飛ぶ
            };
            Action back = () =>
            {
                deleteAction?.Invoke();
                owner.BackToHome();
            };
            SetEnterAndCancelAction(ent, back);
            Player.vecAction = (vec) => ChangeState(vec);
        }
        protected override void OnUpdate(float deltaTime)
        {
            
        }
        protected override void OnExit()
        {
            mSceneManager.TryDeletePopupCursor();
            deleteAction?.Invoke();
            deleteAction = null;
        }
    }
}
