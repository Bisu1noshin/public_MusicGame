using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public abstract class IState : StateBase<ModeSelectSceneManager, Trigger>, IActionDictionary, IModeSelecter
    {
        protected ModeSelectSceneManager mOwner;
        public List<Action> Actions { get; set; }
        public Dictionary<int, Action> ActionDic { get; set; }
        public int SelectNum { get; protected set; }
        public IState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            mOwner = owner;
            Actions = new();
            SelectNum = 0;
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
            SelectNum += vector2.y < 0.0f ? -1 : 1;
            if (SelectNum < 0)
            {
                SelectNum = 0;
                PlayBeepSound();
            }
            else if (SelectNum > Actions.Count - 1)
            {
                SelectNum = Actions.Count - 1;
                PlayBeepSound();
            }
            else
            {
                PlayShiftSound();
            }
        }
    }
}
