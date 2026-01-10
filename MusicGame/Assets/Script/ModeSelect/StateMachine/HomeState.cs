using ModeSelect.StateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModeSelect.StateMachine
{
<<<<<<< HEAD
    public class HomeState : Kameda_StateParent<ISceneManager, Trigger>
    {
        PropertyController mProperty;
        int mPrevSelectNum = 0;
        readonly string[] explaination = new string[]
=======
    public class HomeState : StateParent
    {
        PropertyController mProperty;
        int mPrevSelectNum = 0;
        string[] explaination = new string[]
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
        {
            "シングルプレイで遊びます",
            "マルチプレイで遊びます",
            "ゲームプレイの設定ができます",
            "タイトルに戻ります"
        };
        public HomeState(ISceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
<<<<<<< HEAD
            mActions = new List<Action>()
            {
                () => stateMachine.ExecuteTriggerAction(Trigger.Single),
                () => stateMachine.ExecuteTriggerAction(Trigger.Multi),
                () => stateMachine.ExecuteTriggerAction(Trigger.Setting),
                () => SceneManager.LoadScene("Ooo_Title")
            };
            mSelectNum = 0;
            mPrevSelectNum = 0;
=======
            ReserveNullActionList(4);
            Actions = new List<Action>()
            {
                () => { mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Single); },
                () => { mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Multi); },
                () => { mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Setting); },
                () => { SceneManager.LoadScene("Ooo_Title"); }
            };
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
        }
        protected override void OnEnter()
        {
            InitAction();
            
<<<<<<< HEAD
            deleteAction += Button.ButtonManager.CreateInstance(0, 4, "シングルプレイ");
            deleteAction += Button.ButtonManager.CreateInstance(1, 4, "マルチプレイ");
            deleteAction += Button.ButtonManager.CreateInstance(2, 4, "設定");
            deleteAction += Button.ButtonManager.CreateInstance(3, 4, "タイトルに戻る");
            (PropertyController, Action) tuple = PropertyController.CreateInstance();
            mProperty = tuple.Item1;
            deleteAction += tuple.Item2;
            mSceneManager.CreateCursol();
            mPrevSelectNum = mSelectNum + 1;
=======
            deleteAction += Button.ButtonManager.CreateInstance(this, 0, 4, "シングルプレイ");
            deleteAction += Button.ButtonManager.CreateInstance(this, 1, 4, "マルチプレイ");
            deleteAction += Button.ButtonManager.CreateInstance(this, 2, 4, "設定");
            deleteAction += Button.ButtonManager.CreateInstance(this, 3, 4, "タイトルに戻る");
            (PropertyController, Action) tuple = PropertyController.CreateInstance();
            mProperty = tuple.Item1;
            deleteAction += tuple.Item2;
            mOwner.CreateCursol();
            ReplaceEnterAction(Actions[mSelectNum]);
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
        }

        protected override void OnUpdate(float deltaTime)
        {
<<<<<<< HEAD
            if (stateMachine == null) Debug.Log("StateMachine is NULL!!");
            mProperty?.SetText(explaination[mSelectNum]);
            if (mSceneManager.CursolRect != null) mSceneManager.CursolRect.anchoredPosition = new(-350.0f, -(mSelectNum - (4 - 1) / 2.0f) * (540 / 4 * 2));
            Player.backAction ??= () => PlayBeepSound();

            if (mSelectNum != mPrevSelectNum) ReplaceEnterAction(mSelectNum);
=======
            mProperty.SetText(explaination[mSelectNum]);
            if (mOwner.CursolRect != null) mOwner.CursolRect.anchoredPosition = new(-350.0f, -(mSelectNum - (4 - 1) / 2.0f) * (540 / 4 * 2));
            Player.backAction ??= () => PlayBeepSound();
            if (mSelectNum != mPrevSelectNum)
            {
                Player.enterAction = Actions[mSelectNum];
            }
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
            mPrevSelectNum = mSelectNum;
        }
        protected override void OnExit()
        {
<<<<<<< HEAD
            deleteAction?.Invoke();
            deleteAction = null;
            mSceneManager.TryDeleteCursol();
=======
            mOwner.TryDeleteCursol();
            deleteAction?.Invoke();
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
        }
        void InitAction()
        {
            Player.vecAction = (vector2) => Scroll(vector2);
<<<<<<< HEAD
            Player.backAction = null;
            ReplaceEnterAction(mSelectNum);
        }
        void ReplaceEnterAction(int value)
        {
            Player.enterAction = () =>
            {
                PlayEnterSound();
                mActions[value].Invoke();
            };
=======
            deleteAction = null;
            Player.backAction = () => PlayBeepSound();
>>>>>>> fd1bb35 (ノートPCのコミット忘れ　すぐ消す)
        }
    }
}
