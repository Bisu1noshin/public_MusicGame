using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace ModeSelect
{
    public enum State
    {
        Home, Single, Multi, Setting, BacktoTitle
    }
    public enum Trigger
    {
        Home, Single, Multi, Setting, BacktoTitle, Enter, Back
    }

    public class ModeSelectSceneManager : MonoBehaviour
    {
        public int[] SelectNum { get; set; }
        public AudioSource mAudio { get; private set; }
        public AudioClip[] mAudioClips { get; private set; }

        StateMachine<State, Trigger> mStateMachine;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            mAudio = GetComponent<AudioSource>();
            mAudioClips = new AudioClip[4];
            mAudioClips[0] = Resources.Load<AudioClip>("MusicSelect/Sound/Enter");
            mAudioClips[1] = Resources.Load<AudioClip>("MusicSelect/Sound/Cancel");
            mAudioClips[2] = Resources.Load<AudioClip>("MusicSelect/Sound/Scroll");
            mAudioClips[3] = Resources.Load<AudioClip>("MusicSelect/Sound/Beep");
        }

        // Update is called once per frame
        void Update()
        {

        }
        void SetupStateMachine()
        {
            mStateMachine = new StateMachine<State, Trigger>(State.Home);
            mStateMachine.SetupState(State.Home, new HomeState(this, mStateMachine));
        }
        
    }

    public interface IModeSelecter
    {
        List<Action> Actions { get; set; }
        int[] SelectNum { get; }
    }
    public interface IActionDictionary
    {
        Dictionary<int, Action> ActionDic { get; }
    }
}

