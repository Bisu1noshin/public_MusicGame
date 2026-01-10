using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModeSelect.StateMachine
{
    public class SingleState : IState
    {
        public SingleState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            ReserveNullActionList(1);
            Debug.Log("SingleState ready");
        }
        protected override void OnEnter()
        {
            //Actions = new List<Action>(1);
            layer = 0;
            deleteAction = null;
            deleteAction += PopupController.CreateInstance("シングルプレイを開始します。\nよろしいですか？");
            Player.enterAction = () => { deleteAction?.Invoke(); SceneManager.LoadScene("Test_MusicSelectScene"); };
            Player.backAction = () => mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Home);
            Player.backAction += deleteAction;
        }
        protected override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
        }
        protected override void OnExit()
        {
            
        }
    }
}
