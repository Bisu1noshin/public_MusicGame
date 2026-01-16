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
        None = -1, Home, Single, Multi, Setting, BacktoTitle
    }
    public enum Trigger
    {
        Home, Single, Multi, Setting, BacktoTitle, Enter, Back
    }

    public class ModeSelectSceneManager : MonoBehaviour, ISceneManager
    {
        IResourceManager resource;
        public IResourceManager mResource => resource;
        public int[] SelectNum { get; set; }
        public AudioSource mAudio { get; private set; }
        public AudioClip[] mAudioClips { get; private set; }

        [SerializeField] public NotesManagerDatabase mNotesManager;

        public StateMachine<State, Trigger> mStateMachine { get; set; }

        public RectTransform CursolRect { get; set; }
        GameObject Cursol;

        [SerializeField] bool DebugMode;
        public bool _DebugMode => DebugMode;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            if (!GameObject.Find("Player")) { Instantiate(Resources.Load<GameObject>("ModeSelect/Player")); }
            mAudio = GetComponent<AudioSource>();
            resource = GameObject.Find("ResourceManager").GetComponent<Kameda_ResourceManager>();
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
            mStateMachine = new StateMachine<State, Trigger>(State.None, null);

            mStateMachine.SetupState(State.Home, new HomeState(this, mStateMachine));
            mStateMachine.SetupState(State.Single, new SingleState(this, mStateMachine));
            mStateMachine.SetupState(State.Multi, new MultiState(this, mStateMachine));
            mStateMachine.SetupState(State.Setting, new SettingState(this, mStateMachine));

            mStateMachine.AddTransition(State.None, State.Home, Trigger.Home);
            mStateMachine.AddTransition(State.Home, State.Single, Trigger.Single);
            mStateMachine.AddTransition(State.Home, State.Multi, Trigger.Multi);
            mStateMachine.AddTransition(State.Home, State.Setting, Trigger.Setting);

            mStateMachine.AddTransition(State.Single, State.Home, Trigger.Home);
            mStateMachine.AddTransition(State.Multi, State.Home, Trigger.Home);
            mStateMachine.AddTransition(State.Setting, State.Home, Trigger.Home);

            mStateMachine.ExecuteTriggerAction(Trigger.Home);
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
        public void TryDeleteCursol() { if(Cursol != null) Destroy(Cursol); }
        public NotesManagerPlayerConfig GetPlayerConfig() { return mNotesManager.PlayerConfig; }
        public void PlaySound(int value)
        {
            mAudio.PlayOneShot(mAudioClips[value]);
        }
    }
    public interface ICursolController
    {
        void CreateCursol();
        void TryDeleteCursol();
        RectTransform CursolRect { get; set; }
    }

    public interface ISceneManager : ICursolController
    {
        StateMachine<State, Trigger> mStateMachine { get; set; }
        void PlaySound(int value);
        NotesManagerPlayerConfig GetPlayerConfig();
        bool _DebugMode { get; }
        IResourceManager mResource { get; }
    }
}

