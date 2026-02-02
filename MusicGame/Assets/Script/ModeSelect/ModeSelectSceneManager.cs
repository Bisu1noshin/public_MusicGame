using System;
using UnityEngine;
using System.Collections.Generic;
using ModeSelect.StateMachine;
using Notes;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using LoadForAsync;

namespace ModeSelect
{
    public enum State
    {
        None = -1, Home, Single, Multi, Setting, Intro
    }
    public enum Trigger
    {
        Home, Single, Multi, Setting, Intro
    }

    public class ModeSelectSceneManager : MonoBehaviour, ISceneManager
    {
        IResourceManager resManager;
        public IResourceManager Resource => resManager;

        AudioSource mAudio;
        AudioClip[] mAudioClips;

        public NotesManagerDatabase mNotesManager;

        public StateMachine<State, Trigger> mStateMachine { get; set; }

        RectTransform CursorRect, PopupCursorRect;
        GameObject mCursor, mCursorRes;
        GameObject mPopupCursor, mPopupCursorRes;
        GameObject mPlayer, mPlayerRes;

        [SerializeField] bool DebugMode;
        public bool _DebugMode => DebugMode;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GameObject.Find("CursorCanvas").GetComponent<Canvas>().sortingOrder = 999;
            resManager = GameObject.Find("ResourceManager").GetComponent<Kameda_ResourceManager>();
            resManager.StartLoadSync();
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
            mStateMachine.SetupState(State.Intro, new IntroState(this, mStateMachine));

            mStateMachine.AddTransition(State.None, State.Home, Trigger.Home);
            mStateMachine.AddTransition(State.Home, State.Single, Trigger.Single);
            mStateMachine.AddTransition(State.Home, State.Multi, Trigger.Multi);
            mStateMachine.AddTransition(State.Home, State.Setting, Trigger.Setting);
            mStateMachine.AddTransition(State.Home, State.Intro, Trigger.Intro);

            mStateMachine.AddTransition(State.Single, State.Home, Trigger.Home);
            mStateMachine.AddTransition(State.Multi, State.Home, Trigger.Home);
            mStateMachine.AddTransition(State.Setting, State.Home, Trigger.Home);
            mStateMachine.AddTransition(State.Intro, State.Home, Trigger.Home);

            mStateMachine.ExecuteTriggerAction(Trigger.Home);
        }
        public void CreateCursor()
        {
            if (mCursor != null)
            {
                Debug.LogWarning("警告：画面には既にカーソルが存在しています。\n複数のカーソルのインスタンス化は不可逆的且つ可視的なガーベッジの蓄積を意味します。");
                return;
            }
            mCursor = Instantiate(mCursorRes);
            mCursor.transform.SetParent(GameObject.Find("Canvas").transform);
            mCursor.transform.localScale = Vector3.one;
            mCursor.transform.localRotation = Quaternion.identity;
            CursorRect = mCursor.GetComponent<RectTransform>();
            CursorRect.anchoredPosition = new(-350f, 0f);
        }

        public void TryDeleteCursor()
        {
            if (mCursor == null) return;
            Destroy(mCursor);
            mCursor = null;
        }

        public void TrySetCursorPos(int curr, int max)
        {
            if (mCursor) CursorRect.anchoredPosition = new(-350f, -(curr - (max - 1) / 2f) * (540 / max * 2));
        }
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
            mPopupCursorRes = resManager.GetGameObject("mPopupCursor", true);
        }
        public void CreatePopupCursor()
        {
            mPopupCursor = Instantiate(mPopupCursorRes);
            mPopupCursor.transform.SetParent(GameObject.Find("CursorCanvas").transform);
            mPopupCursor.transform.localScale = Vector3.one;
            mPopupCursor.transform.localRotation = Quaternion.identity;
            PopupCursorRect = mPopupCursor.GetComponent<RectTransform>();
            PopupCursorRect.anchoredPosition = Vector2.zero;
        }
        public void TryDeletePopupCursor()
        {
            if (mPopupCursor) Destroy(mPopupCursor);
        }
        public void TrySetPopupCursorPos(bool forSprite, bool currEnter, bool currChangeElement)
        {
            if (!PopupCursorRect) return;
            if (forSprite)
            {
                if (currEnter)
                {
                    PopupCursorRect.anchoredPosition = new(-300f, -400f);
                }
                else
                {
                    PopupCursorRect.anchoredPosition = new(300f, -400f);
                }
            }
            else
            {
                if (currChangeElement)
                {
                    //要素変更中
                    //画面中央
                    PopupCursorRect.anchoredPosition = Vector2.zero;
                }
                else
                {
                    //決定orキャンセル
                    //下
                    if (currEnter)
                    {
                        PopupCursorRect.anchoredPosition = new(-300f, -200f);
                    }
                    else
                    {
                        PopupCursorRect.anchoredPosition = new(300f, -200f);
                    }
                }
            }
            
        }
    }
    public interface ICursorController
    {
        void CreateCursor();
        void TryDeleteCursor();
        void TrySetCursorPos(int curr, int max);
    }

    public interface ISceneManager : ICursorController, IPopupCursorController
    {
        StateMachine<State, Trigger> mStateMachine { get; set; }

        NotesManagerPlayerConfig GetPlayerConfig();
        bool _DebugMode { get; }
        IResourceManager Resource { get; }
        void PlaySound(int value);
    }
    public interface IPopupCursorController
    {
        void CreatePopupCursor();
        void TryDeletePopupCursor();
        void TrySetPopupCursorPos(bool forSprite, bool currEnter = true, bool currChangeElement = true);
        
    }
}

