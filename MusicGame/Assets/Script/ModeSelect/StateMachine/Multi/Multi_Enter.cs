using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeSelect.StateMachine.Multi
{
    public class Multi_Enter : Kameda_StateParent<IMultiState, MTrigger>
    {
        public Multi_Enter(IMultiState _owner, IStateMachine<MTrigger> _st) : base(_owner, _st)
        {

        }
        protected override void OnEnter()
        {
            deleteAction = null;
            deleteAction += CreatePopupInstance("オンラインモードを開始しますか？");
            Player.enterAction = () =>
            {
                PlayEnterSound();
                //目的のシーンに飛ぶ
            };
            Player.backAction = () =>
            {
                deleteAction?.Invoke();
                PlayCancelSound();
                owner.BackToHome();
            };
            Player.vecAction = null;
        }
        protected override void OnUpdate(float deltaTime)
        {
            
        }
        protected override void OnExit()
        {
            deleteAction?.Invoke();
        }
    }
}
