using ModeSelect.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeSelect.StateMachine.Multi
{
    public class Multi_Home : Kameda_StateParent<IMultiState, MTrigger>
    {
        public Multi_Home(IMultiState _owner, IStateMachine<MTrigger> _st) : base(_owner, _st)
        {

        }
        protected override void OnEnter()
        {
            deleteAction = null;
            deleteAction += CreatePopupInstance("マルチプレイはSteamの機能を\n使用します。\nSteamの起動が完了した場合は\n次に進んでください。", 72);
            Player.enterAction = () =>
            {
                PlayEnterSound();
                stateMachine.ExecuteTriggerAction(MTrigger.Enter);
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
            //pass
        }
        protected override void OnExit()
        {
            deleteAction?.Invoke();
        }
    }
}
