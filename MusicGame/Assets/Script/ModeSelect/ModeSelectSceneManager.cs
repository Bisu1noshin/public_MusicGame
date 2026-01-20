using System;
using UnityEngine;
using System.Collections.Generic;
using ModeSelect.StateMachine;
using Notes;
using System.Data;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using LoadForAsync;

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
        IResourceManager resManager;
        public IResourceManager Resource => resManager;
        
        public Action ReleaseAll { get; set; }

        AudioSource mAudio;
        AudioClip[] mAudioClips;

        [SerializeField] public NotesManagerDatabase mNotesManager;

        public StateMachine<State, Trigger> mStateMachine { get; set; }

        public RectTransform CursorRect { get; set; }
        GameObject mCursor, mCursorRes;
        GameObject mPlayer, mPlayerRes;

        [SerializeField] bool DebugMode;
        public bool _DebugMode => DebugMode;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            resManager = GameObject.Find("ResourceManager").GetComponent<Kameda_ResourceManager>();
            SetObjects();
            if (!mPlayer) { mPlayer = Instantiate(mPlayerRes); }
            mAudio = GetComponent<AudioSource>();
            
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
        public void CreateCursor()
        {
            mCursor = Instantiate(mCursorRes);
            mCursor.transform.SetParent(GameObject.Find("Canvas").transform);
            mCursor.transform.localScale = Vector3.one;
            mCursor.transform.localRotation = Quaternion.identity;
            CursorRect = mCursor.GetComponent<RectTransform>();
            CursorRect.anchoredPosition = new(-350, 0);
        }
        public void TryDeleteCursor() { if(mCursor != null) Destroy(mCursor); }
        public NotesManagerPlayerConfig GetPlayerConfig() { return mNotesManager.PlayerConfig; }
        public void PlaySound(int value)
        {
            mAudio.PlayOneShot(mAudioClips[value]);
        }
        void SetObjects()
        {
            mAudioClips = new AudioClip[4];
            mAudioClips[0] = resManager.GetAudioClip("Enter");
            mAudioClips[1] = resManager.GetAudioClip("Cancel");
            mAudioClips[2] = resManager.GetAudioClip("Scroll");
            mAudioClips[3] = resManager.GetAudioClip("Beep");
            mCursorRes = resManager.GetGameObject("mCursor", true);
            mPlayerRes = resManager.GetGameObject("Player", true);
        }
    }
    public interface ICursorController
    {
        void CreateCursor();
        void TryDeleteCursor();
        RectTransform CursorRect { get; set; }
    }

    public interface ISceneManager : ICursorController
    {
        StateMachine<State, Trigger> mStateMachine { get; set; }
        void PlaySound(int value);
        NotesManagerPlayerConfig GetPlayerConfig();
        bool _DebugMode { get; }
        IResourceManager Resource { get; }
    }
}

