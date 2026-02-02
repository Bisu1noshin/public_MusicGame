using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModeSelect.StateMachine.Setting.Operation
{
    public class Oper_Intro : PopupAdmin<Setting_Oper, OTrigger>
    {
        Sprite intro_K, intro_C;
        bool curr;
        public Oper_Intro(Setting_Oper owner, IStateMachine<OTrigger> st) : base(owner, st, false, CursorState.EnterORCancel)
        {
            intro_K = mSceneManager.Resource.GetSprite("Intro_K");
            intro_C = mSceneManager.Resource.GetSprite("Intro_C");
        }
        protected override void OnEnter()
        {
            Player.enterAction = null;
            curr = true;
            CreateImagePopupInstance(ref mPopup, intro_K);
            CreateCursor();
            Action shift = () =>
            {
                curr = !curr;
                if (curr)
                {
                    mPopup.SetImage(intro_K);
                }
                else
                {
                    mPopup.SetImage(intro_C);
                }
            };
            Action back = () =>
            {
                stateMachine.ExecuteTriggerAction(OTrigger.Home);
            };
            SetEnterAndCancelAction(shift, back);
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
