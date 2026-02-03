using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModeSelect.StateMachine
{
    public class SingleState : PopupAdmin<ISceneManager, Trigger>
    {
        public SingleState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st, false, CursorState.EnterORCancel)
        {
            
        }
        protected override void OnEnter()
        {
            CreateCursor();
            Player.enterAction = null;
            deleteAction = null;
            deleteAction += CreatePopupInstance("楽曲選択に移動します。\nよろしいですか？");
            Action ent = () => SceneManager.LoadScene("Test_MusicSelectScene");
            Action back = () => stateMachine.ExecuteTriggerAction(Trigger.Home);
            SetEnterAndCancelAction(ent, back);
            Player.vecAction = (vec) => ChangeState(vec);
            Player.enterAction = () => PlayEnterSound();
            Player.enterAction += ent;
        }
        protected override void OnUpdate(float deltaTime)
        {
        }
        protected override void OnExit()
        {
            mCursorOwner.TryDeletePopupCursor();
            deleteAction?.Invoke();
        }
    }
}
