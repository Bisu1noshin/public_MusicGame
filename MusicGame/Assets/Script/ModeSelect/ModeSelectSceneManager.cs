using System;
using UnityEngine;
using System.Collections.Generic;
using ModeSelect.StateMachine;
using Notes;
using System.Data;

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

    public class ModeSelectSceneManager : MonoBehaviour, ISceneManager
    {
        public int[] SelectNum { get; set; }
        public AudioSource mAudio { get; private set; }
        public AudioClip[] mAudioClips { get; private set; }

        [SerializeField] public NotesManagerDatabase mNotesManager;

        public StateMachine<State, Trigger> mStateMachine { get; set; }

        public RectTransform CursolRect { get; set; }
        GameObject Cursol;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            if (!GameObject.Find("Player")) { Instantiate(Resources.Load<GameObject>("ModeSelect/Player")); }
            mAudio = GetComponent<AudioSource>();
            mAudioClips = new AudioClip[4];
            mAudioClips[0] = Resources.Load<AudioClip>("SoundEffect/Enter");
            mAudioClips[1] = Resources.Load<AudioClip>("SoundEffect/Cancel");
            mAudioClips[2] = Resources.Load<AudioClip>("SoundEffect/Scroll");
            mAudioClips[3] = Resources.Load<AudioClip>("SoundEffect/Beep");
            SetupStateMachine();
        }

        // Update is called once per frame
        void Update()
        {
            mStateMachine.Update(Time.deltaTime);
        }
        void SetupStateMachine()
        {
            mStateMachine = new StateMachine<State, Trigger>(State.Home, new HomeState(this, mStateMachine));

            //mStateMachine.SetupState(State.Home, new HomeState(this, mStateMachine));
            mStateMachine.SetupState(State.Single, new SingleState(this, mStateMachine));
            mStateMachine.SetupState(State.Multi, new MultiState(this, mStateMachine));
            mStateMachine.SetupState(State.Setting, new SettingState(this, mStateMachine));

            mStateMachine.AddTransition(State.Home, State.Single, Trigger.Single);
            mStateMachine.AddTransition(State.Home, State.Multi, Trigger.Multi);
            mStateMachine.AddTransition(State.Home, State.Setting, Trigger.Setting);

            mStateMachine.AddTransition(State.Single, State.Home, Trigger.Home);
            mStateMachine.AddTransition(State.Multi, State.Home, Trigger.Home);
            mStateMachine.AddTransition(State.Setting, State.Home, Trigger.Home);

            
        }
        public void CreateCursol()
        {
            Cursol = Instantiate(Resources.Load<GameObject>("ModeSelect/Cursol"));
            Cursol.transform.SetParent(GameObject.Find("Canvas").transform);
            Cursol.transform.localScale = Vector3.one;
            Cursol.transform.localRotation = Quaternion.identity;
            CursolRect = Cursol.GetComponent<RectTransform>();
            CursolRect.anchoredPosition = new(-350, 0);
        }
        public void DeleteCursol() { Destroy(Cursol); }
        public NotesManagerDatabase GetNotesManager() { return mNotesManager; }
    }

    
    public interface IActionDictionary
    {
        Dictionary<int, Action> ActionDic { get; }
    }
    public interface ICursolController
    {
        void CreateCursol();
        void DeleteCursol();
    }

    public interface ISceneManager : ICursolController
    {
        StateMachine<State, Trigger> mStateMachine { get; set; }
        AudioSource mAudio { get; }
        AudioClip[] mAudioClips { get; }
        RectTransform CursolRect { get; set; }
        NotesManagerDatabase GetNotesManager();
    }
}

