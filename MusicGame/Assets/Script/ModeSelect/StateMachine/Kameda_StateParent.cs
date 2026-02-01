using Cysharp.Threading.Tasks.Triggers;
using LoadForAsync;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public abstract class Kameda_StateParent<OwnerClass, SPTrigger> : StateBase<OwnerClass, SPTrigger>
            where OwnerClass : class
            where SPTrigger : struct, Enum
    {
        protected Dictionary<string, GameObject> mObjectRes;
        protected ISceneManager mSceneManager;
        protected List<Action> mActions;
        protected int mSelectNum;
        protected Action deleteAction;

        public Action ReleaseAll { get; set; }

        public Kameda_StateParent(OwnerClass owner, IStateMachine<SPTrigger> st) : base(owner, st)
        {
            mSceneManager = GameObject.Find("SceneManager").GetComponent<ModeSelectSceneManager>();
            mActions = new();
            mSelectNum = 0;
            deleteAction = null;
            SetObjects();   
        }

        protected void PlayEnterSound() => mSceneManager.PlaySound(0);
        protected void PlayCancelSound() => mSceneManager.PlaySound(1);
        protected void PlayShiftSound() => mSceneManager.PlaySound(2);
        protected void PlayBeepSound() => mSceneManager.PlaySound(3);

        protected void Scroll(Vector2 vector2)
        {
            if (vector2 == Vector2.zero) return;

            mSelectNum += vector2.y < 0.0f ? 1 : -1;
            if (mSelectNum < 0)
            {
                mSelectNum = 0;
                PlayBeepSound();
            }
            else if (mSelectNum > mActions.Count - 1)
            {
                mSelectNum = mActions.Count - 1;
                PlayBeepSound();
            }
            else
            {
                PlayShiftSound();
            }
        }

        void SetObjects()
        {
            mObjectRes = new()
            {
                { "Button", mSceneManager.Resource.GetGameObject("Button", true) },
                { "Popup", mSceneManager.Resource.GetGameObject("Popup", true) },
                { "Popup_Speed", mSceneManager.Resource.GetGameObject("Popup_Speed", true) },
                { "Property", mSceneManager.Resource.GetGameObject("Property", true) }
            };

        }

        protected Action CreateButtonInstance(int cur, int max, string msg)
        {
            return Button.ButtonManager.CreateInstance(cur, max, mObjectRes.GetValueOrDefault("Button"), msg);
        }
        protected (PropertyController, Action) CreatePropertyInstance()
        {
            return PropertyController.CreateInstance(mObjectRes.GetValueOrDefault("Property"));
        }
        protected Action CreatePopupInstance(string str, int size = 96)
        {
            return PopupController.CreateInstance(str, mObjectRes.GetValueOrDefault("Popup"), size);
        }
        protected (PopupController, Action) CreateSpeedPopupInstance(bool isKeyboard)
        {
            return PopupController.CreateInstanceForNotesSpeed(isKeyboard, mObjectRes.GetValueOrDefault("Popup_Speed"));
        }
    }
    public interface IParentState
    {
        void BackToHome();
    }
}
