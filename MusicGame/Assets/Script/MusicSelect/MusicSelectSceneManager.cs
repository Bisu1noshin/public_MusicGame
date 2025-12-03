using System;
using System.Collections.Generic;
using Notes;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

enum SceneState
{
    MusicSelect,
    LevelSelect,
    EnterGame
}

public class MusicSelectSceneManager : MonoBehaviour, IMusicSelecter, ILevelSelecter
{
    private Action deleteAction = null;
    AudioSource mAudio;

    private AudioClip enter, cancel, scroll, beep;

    int maxValue;
    int[] selectNum;
    bool startSelect;
    public bool CanSelect
    {
        get { return startSelect; }
        set { startSelect = value; }
    }
    public int[] SelectNum => selectNum;
    public int MaxValue => maxValue;
    public MusicDatabase mDataBase;
    Notes.NotesData[] mCurrNotesData;
    SceneState mSceneState;
    readonly string[] levelName =
    {
        "NORMAL",
        "HARD",
        "EXPERT",
        "ULTIMATE"
    };

    private void Awake()
    {
        //Application.targetFrameRate = 60;
        if (!GameObject.Find("Player_forMusicSelect"))
        {
            CreatePlayer();
        }
        
        deleteAction += PropertyController.CreateInstance();
        CreateMusicButtons();

        selectNum = new int[3];
        mCurrNotesData = new Notes.NotesData[4];
        mSceneState = SceneState.MusicSelect;
        startSelect = false;
        mAudio = GetComponent<AudioSource>();
        enter = Resources.Load<AudioClip>("MusicSelecter/Sound/Enter");
        cancel = Resources.Load<AudioClip>("MusicSelecter/Sound/Cancel");
        scroll = Resources.Load<AudioClip>("MusicSelecter/Sound/Scroll");
        beep = Resources.Load<AudioClip>("MusicSelecter/Sound/Beep");
        foreach (MusicData md in mDataBase.musicDatabase)
        {
            for (int i = 0; i < 4; ++i)
            {
                MakeNotesData(ref md.notesData[i]);
            }
        }
    }
    public void GoForward()
    {
        if (!startSelect) return;
        
        switch (mSceneState)
        {
            case SceneState.MusicSelect:
                if (selectNum[0] >= mDataBase.musicDatabase.Count - 1)
                {
                    mAudio.PlayOneShot(beep);
                }
                else
                {
                    selectNum[0]++;
                    mAudio.PlayOneShot(scroll);
                }
                break;
            case SceneState.LevelSelect:
                if (selectNum[1] > maxValue - 1)
                {
                    mAudio.PlayOneShot(beep);
                }
                else
                {
                    selectNum[1]++;
                    mAudio.PlayOneShot(scroll);
                }
                break;
            default:
                break;
        }
        
    }
    public void GoBack()
    {
        if (!startSelect) return;
        switch (mSceneState)
        {
            case SceneState.MusicSelect:
                if (selectNum[0] <= 0)
                {
                    mAudio.PlayOneShot(beep);
                }
                else
                {
                    selectNum[0]--;
                    mAudio.PlayOneShot(scroll);
                }
                break;
            case SceneState.LevelSelect:
                if (selectNum[1] <= 0)
                {
                    mAudio.PlayOneShot(beep);
                }
                else
                {
                    selectNum[1]--;
                    mAudio.PlayOneShot(scroll);
                }
                break;
            default:
                break;
        }
    }
    public void Enter()
    {
        if (!startSelect) return;
        switch (mSceneState)
        {
            case SceneState.MusicSelect:
                mCurrNotesData = mDataBase.musicDatabase[selectNum[0]].notesData;
                DeleteObj();
                CreateLevelButtons();
                mAudio.PlayOneShot(enter);
                break;
            case SceneState.LevelSelect:
                //DeleteObj();
                CreatePopupInstance();
                mAudio.PlayOneShot(enter);
                break;
            case SceneState.EnterGame:

                SceneManager.LoadScene("NotesTest");
                break;
        }
        if (mSceneState != SceneState.EnterGame)
        {
            mSceneState++;
        }
    }
    public void Undo()
    {
        if (!startSelect) return;
        switch (mSceneState)
        {
            case SceneState.MusicSelect:
                break;
            case SceneState.LevelSelect:
                DeleteObj();
                deleteAction += PropertyController.CreateInstance();
                CreateMusicButtons();
                mAudio.PlayOneShot(cancel);
                break;
            case SceneState.EnterGame:
                DeleteObj();
                CreateLevelButtons();
                mAudio.PlayOneShot(cancel);
                break;
        }
        if (mSceneState > SceneState.MusicSelect)
        {
            mSceneState--;
        }
    }
    Player_forMusicSelect CreatePlayer()
    {
        GameObject res = Resources.Load("MusicSelecter/Player_forMusicSelect") as GameObject;
        GameObject instance = Instantiate(res);
        return instance.GetComponent<Player.Player_forMusicSelect>();
    }
    void CreateMusicButtons()
    {
        foreach (MusicData m in mDataBase.musicDatabase)
        {
            deleteAction += MusicButtonController.CreateInstance(m.name, m.id, m.demoMusicPath);
        }
    }

    void CreateLevelButtons()
    {
        for (int i = 0; i < 4; i++)
        {
            if (mCurrNotesData[i] == null) { break; }
            deleteAction += LevelButtonController.CreateInstance(i, levelName[i]);
            maxValue = i;
        }
    }
    void MakeNotesData(ref NotesData md_)
    {
        TextEditor.TextEditor text = new("Music/ShiningStar", "TextData/NotesData/ShiningStar/ShiningStar_NORMAL");
        md_ = text.NotesReadTxt();
    }
    void DeleteObj()
    {
        deleteAction?.Invoke();
        deleteAction = null;
    }
    void CreatePopupInstance()
    {
        var loadObj = Resources.Load<GameObject>("MusicSelecter/Popup_EnterGame");
        Debug.Log($"load: {loadObj.name}");

        GameObject go = Instantiate(loadObj);
        go.transform.SetParent(GameObject.Find("Canvas").transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = Quaternion.identity;
        deleteAction += () => { Destroy(go); };
    }
}

public interface IMusicSelecter
{
    int[] SelectNum { get; }
    bool CanSelect { get; set; }
    void GoForward();
    void GoBack();
    void Enter();
    void Undo();
}

public interface ILevelSelecter
{
    int[] SelectNum { get; }
    int MaxValue { get; }
}