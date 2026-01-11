using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModeSelect.StateMachine
{
    public class SingleState : Kameda_StateParent<ISceneManager, Trigger>
    {
        public SingleState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            
        }
        protected override void OnEnter()
        {
            deleteAction = null;
            deleteAction += PopupController.CreateInstance("シングルプレイを開始します。\nよろしいですか？");
            Player.enterAction = () => SceneManager.LoadScene("Test_MusicSelectScene");
            Player.backAction = () =>
            {
                PlayCancelSound();
                stateMachine.ExecuteTriggerAction(Trigger.Home);
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
