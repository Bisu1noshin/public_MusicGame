using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect.StateMachine
{
<<<<<<< HEAD
    public class MultiState : Kameda_StateParent<ISceneManager, Trigger>
=======
    public class MultiState : StateParent
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
    {
        public MultiState(ISceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {

        }
        protected override void OnEnter()
        {
            InitAction();
            deleteAction = null;
            deleteAction += PopupController.CreateInstance("このモードは現在\n利用できません。");
        }
        protected override void OnUpdate(float deltaTime)
        {
            Player.backAction ??= () =>
            {
                PlayCancelSound();
                stateMachine.ExecuteTriggerAction(Trigger.Home);
            };
            Player.enterAction ??= () => PlayBeepSound();
        }
        protected override void OnExit()
        {
            deleteAction?.Invoke();
<<<<<<< HEAD
            deleteAction = null;
=======
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
        }
        void InitAction()
        {
            Player.vecAction = null;
<<<<<<< HEAD
            Player.backAction = () =>
            {
                PlayCancelSound();
                stateMachine.ExecuteTriggerAction(Trigger.Home);
            };
=======
            Player.backAction = () => mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Home);
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
            Player.enterAction = () => PlayBeepSound();
        }
    }
}
