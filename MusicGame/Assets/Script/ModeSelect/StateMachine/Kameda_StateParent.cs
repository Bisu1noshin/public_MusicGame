using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace ModeSelect
{
    namespace StateMachine
    {
        public abstract class Kameda_StateParent : StateBase<ModeSelectSceneManager, Trigger>, IModeSelecter
        {
            protected ISceneManager mOwner;
            public List<Action> mActions { get; set; }
            public int mSelectNum { get; protected set; }
            public Action deleteAction { get; set; }
            protected GameObject buttonPre;
            public Kameda_StateParent(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
            {
                mOwner = owner;
                mActions = new();
                mSelectNum = 0;
                deleteAction = null;
            }
            protected void PlayEnterSound()
            {
                mOwner.mAudio.PlayOneShot(mOwner.mAudioClips[0]);
            }
            protected void PlayCancelSound()
            {
                mOwner.mAudio.PlayOneShot(mOwner.mAudioClips[1]);
            }
            protected void PlayShiftSound()
            {
                mOwner.mAudio.PlayOneShot(mOwner.mAudioClips[2]);
            }
            protected void PlayBeepSound()
            {
                mOwner.mAudio.PlayOneShot(mOwner.mAudioClips[3]);
            }
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
            protected void ReserveNullActionList(int cap)
            {
                for (int i = 0; i < cap; ++i)
                {
                    mActions.Add(null);
                }
            }
            protected void ReplaceNullActionList(int cap)
            {
                mActions.Clear();
                ReserveNullActionList(cap);
            }
        }
    }
    

    public interface IModeSelecter
    {
        List<Action> mActions { get; set; }
        int mSelectNum { get; }
        Action deleteAction { get; set; }
    }
}
