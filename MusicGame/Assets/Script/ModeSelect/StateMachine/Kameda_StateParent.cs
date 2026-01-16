using Cysharp.Threading.Tasks.Triggers;
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

        protected ISceneManager mSceneManager;
        protected List<Action> mActions;
        protected int mSelectNum;
        protected Action deleteAction;

        public Kameda_StateParent(OwnerClass owner, IStateMachine<SPTrigger> st) : base(owner, st)
        {
            mSceneManager = GameObject.Find("SceneManager").GetComponent<ModeSelectSceneManager>();
            mActions = new();
            mSelectNum = 0;
            deleteAction = null;
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
    }
}
