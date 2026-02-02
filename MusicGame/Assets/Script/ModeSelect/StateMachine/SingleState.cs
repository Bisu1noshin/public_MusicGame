using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModeSelect.StateMachine
{
    public class SingleState : PopupAdmin<ISceneManager, Trigger>
    {
        public SingleState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st, false)
        {
            
        }
        protected override void OnEnter()
        {
            deleteAction = null;
            deleteAction += CreatePopupInstance("シングルプレイを開始します。\nよろしいですか？");
            Action ent = () => SceneManager.LoadScene("Test_MusicSelectScene");
            Action back = () => stateMachine.ExecuteTriggerAction(Trigger.Home);
            SetEnterAndCancelAction(ent, back);
            Player.vecAction = (vec) => ChangeState(vec);
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
