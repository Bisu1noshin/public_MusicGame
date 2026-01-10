using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModeSelect.StateMachine
{
<<<<<<< HEAD
    public class SingleState : Kameda_StateParent<ISceneManager, Trigger>
=======
    public class SingleState : StateParent
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
    {
        public SingleState(ISceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
<<<<<<< HEAD

        }
        protected override void OnEnter()
        {
            Debug.Log("SingleState started");
            deleteAction += PopupController.CreateInstance("シングルプレイを開始します。\nよろしいですか？");
            Player.enterAction = () =>
            {
                PlayEnterSound();
                SceneManager.LoadScene("Test_MusicSelectScene");
            };
            Player.backAction = () =>
            {
                PlayCancelSound();
                stateMachine.ExecuteTriggerAction(Trigger.Home);
            };
            Player.vecAction = null;
        }
        protected override void OnUpdate(float deltaTime)
        {
            Player.enterAction ??= () =>
            {
                PlayEnterSound();
                SceneManager.LoadScene("Test_MusicSelectScene");
            };
            Player.backAction ??= () =>
            {
                PlayCancelSound();
                stateMachine.ExecuteTriggerAction(Trigger.Home);
            };
=======
        }
        protected override void OnEnter()
        {
            //Actions = new List<Action>(1);
            deleteAction = null;
            deleteAction +=
                PopupController.CreateInstance("シングルプレイを開始します。\nよろしいですか？");
            Player.enterAction = () => { SceneManager.LoadScene("Test_MusicSelectScene"); };
            Player.backAction = () => mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Home);
        }
        protected override void OnUpdate(float deltaTime)
        {
            
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
        }
        protected override void OnExit()
        {
            deleteAction?.Invoke();
<<<<<<< HEAD
            deleteAction = null;
=======
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
        }
    }
}
