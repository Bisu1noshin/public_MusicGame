using Cysharp.Threading.Tasks.Triggers;
using JetBrains.Annotations;
using LoadForAsync;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ModeSelect.StateMachine
{
    public abstract class Kameda_StateBase<OwnerClass, SPTrigger> : StateBase<OwnerClass, SPTrigger>
            where OwnerClass : class
            where SPTrigger : struct, Enum
    {
        protected Dictionary<string, GameObject> mObjectRes;
        protected ISceneManager mSceneManager;
        
        protected Action deleteAction;

        public Action ReleaseAll { get; set; }

        public Kameda_StateBase(OwnerClass owner, IStateMachine<SPTrigger> st) : base(owner, st)
        {
            mSceneManager = GameObject.Find("SceneManager").GetComponent<ModeSelectSceneManager>();
            deleteAction = null;
            SetObjects();   
        }

        protected void PlayEnterSound() => mSceneManager.PlaySound(0);
        protected void PlayCancelSound() => mSceneManager.PlaySound(1);
        protected void PlayShiftSound() => mSceneManager.PlaySound(2);
        protected void PlayBeepSound() => mSceneManager.PlaySound(3);



        void SetObjects()
        {
            mObjectRes = new()
            {
                { "Button", mSceneManager.Resource.GetGameObject("Button", true) },
                { "Popup", mSceneManager.Resource.GetGameObject("Popup", true) },
                { "Popup_Speed", mSceneManager.Resource.GetGameObject("Popup_Speed", true) },
                { "Property", mSceneManager.Resource.GetGameObject("Property", true) },
                { "Popup_Intro", mSceneManager.Resource.GetGameObject("Popup_Intro", true) }
            };

        }

        protected Action CreateButtonInstance(int cur, int max, string msg)
        {
            return Button.ButtonManager.CreateInstance(cur, max, mObjectRes.GetValueOrDefault("Button"), msg);
        }
        protected void CreatePropertyInstance(ref PropertyController prc)
        {
            (PropertyController, Action) tuple = PropertyController.CreateInstance(mObjectRes.GetValueOrDefault("Property"));
            prc = tuple.Item1;
            deleteAction += tuple.Item2;
        }
        protected Action CreatePopupInstance(string str, int size = 96)
        {
            return PopupController.CreateInstance(mObjectRes.GetValueOrDefault("Popup"), str, size);
        }
        protected void CreateSpeedPopupInstance(ref PopupController pc, string msg, int size = 96)
        {
            (PopupController, Action) tuple = PopupController.CreateInstanceForNotesSpeed(mObjectRes.GetValueOrDefault("Popup_Speed"), msg, size);
            pc = tuple.Item1;
            deleteAction += tuple.Item2;
        }
        protected void CreateImagePopupInstance(ref PopupController pc, Sprite image)
        {
            (PopupController, Action) tuple = PopupController.CreateInstanceImage(mObjectRes.GetValueOrDefault("Popup_Intro"), image);
            pc = tuple.Item1;
            deleteAction += tuple.Item2;
        }
    }
    public interface IParentState
    {
        void BackToHome();
    }
}
