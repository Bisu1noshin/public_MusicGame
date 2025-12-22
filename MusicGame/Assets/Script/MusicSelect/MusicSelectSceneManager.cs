using System;
using System.Collections.Generic;
using LoadForAsync;
using Notes;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
using GameInfo;

public enum SceneState
{
    MusicSelect,
    LevelSelect,
    EnterGame
}

public class MusicSelectSceneManager : MonoBehaviour, IMusicSelecter, ILevelSelecter
{
    public static float MaxX => 960.0f;
    private Action deleteAction = null, createAction = null;
    AudioSource mAudio;

    private AudioClip enter, cancel, scroll, beep;

    int maxValue;
    int[] selectNum;
    public int[] SelectNum => selectNum;
    public int MaxValue => maxValue;
    public MusicDatabase mDataBase;
    string[] mCurrNotesData;
    string mCurrMusicPath;
    public SceneState mSceneState { get; private set; }
    float timer = 0.0f;
    public float untouchableTimer { get; private set; }
    readonly string[] levelName =
    {
        "NORMAL",
        "HARD",
        "EXPERT",
        "ULTIMATE"
    };

    // 追記
    [SerializeField] private AssetLoadConfig AssetLoadConfig;

    private void Awake()
    {
        Init();

        CreateMusicButtons().Invoke();
    }

    void Update()
    {
        if (timer <= 0.0f && createAction != null)
        {
            CreateObj();
        }
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }
        if (untouchableTimer > 0.0f)
        {
            untouchableTimer -= Time.deltaTime;
        }
    }

    public void GoForward()
    {
        if (untouchableTimer > 0.0f) return;

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
        if (untouchableTimer > 0.0f) return;

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
        if (untouchableTimer > 0.0f) return;

        switch (mSceneState)
        {
            case SceneState.MusicSelect:
                mCurrNotesData[0] = mDataBase.musicDatabase[selectNum[0]].normalPath;
                mCurrNotesData[1] = mDataBase.musicDatabase[selectNum[0]].hardPath;
                mCurrNotesData[2] = mDataBase.musicDatabase[selectNum[0]].expertPath;
                mCurrMusicPath = mDataBase.musicDatabase[selectNum[0]].musicPath;
                DeleteAndExecuteAction(CreateLevelButtons());
                mAudio.PlayOneShot(enter);
                timer = 0.15f;
                untouchableTimer = 0.2f;
                break;
            case SceneState.LevelSelect:
                if (mCurrNotesData[SelectNum[1]] == string.Empty)
                {
                    mAudio.PlayOneShot(beep);
                    return;
                }
                else
                {
                    DeleteAndExecuteAction(CreatePopup());
                    mAudio.PlayOneShot(enter);
                    timer = 0.1f;
                    untouchableTimer = 0.1f;
                }
                break;
            case SceneState.EnterGame:
                SingletonDataManager.instance.SetMusicId(mDataBase.musicDatabase[SelectNum[0]]);
                LoadSceneRef(mCurrMusicPath, mCurrNotesData[SelectNum[1]]);
                break;
        }
        if (mSceneState != SceneState.EnterGame)
        {
            mSceneState++;
        }
    }
    public void Undo()
    {
        if (untouchableTimer > 0.0f) return;

        switch (mSceneState)
        {
            case SceneState.MusicSelect:
                SceneManager.LoadScene("Test_ModeSelectScene");
                break;
            case SceneState.LevelSelect:
                DeleteAndExecuteAction(CreateMusicButtons());
                mAudio.PlayOneShot(cancel);
                timer = 0.02f;
                untouchableTimer = 0.2f;
                break;
            case SceneState.EnterGame:
                DeleteAndExecuteAction(CreateLevelButtons(mSceneState));
                mAudio.PlayOneShot(cancel);
                timer = 0.02f;
                untouchableTimer = 0.1f;
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

    Action CreateMusicButtons()
    {
        Action f = () => {
            deleteAction += PropertyController.CreateInstance();
            foreach (MusicData m in mDataBase.musicDatabase)
            {
                deleteAction += MusicButtonController.CreateInstance(m.name, m.id, m.demoMusicPath, m.jacketPath); ;
            }
            CreateGUI();
        };
        return f;
    }

    Action CreateLevelButtons(SceneState ss = SceneState.LevelSelect)
    {
        Action f = () => {
            for (int i = 0; i < 3; i++)
            {
                deleteAction += LevelButtonController.CreateInstance(i, levelName[i], ss, mCurrNotesData[i] == string.Empty);
                maxValue = i;
            }
            CreateGUI();
        };
        return f;
    }

    void DeleteObj()
    {
        deleteAction?.Invoke();
        deleteAction = null;
    }

    void CreateObj()
    {
        createAction?.Invoke();
        createAction = null;
    }

    void DeleteAndExecuteAction(Action action)
    {
        DeleteObj();
        createAction += action;
    }

    Action CreatePopup()
    {
        Action f = () => { deleteAction += MakePopupInstance(); };
        return f;
    }

    Action MakePopupInstance()
    {
        var loadObj = Resources.Load<GameObject>("MusicSelecter/Popup_EnterGame");

        GameObject go = Instantiate(loadObj);
        go.transform.SetParent(GameObject.Find("Canvas").transform);
        go.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        go.transform.localScale = Vector3.one;
        
        Action f = () => { Destroy(go); };
        return f;
    }
    void CreateGUI()
    {
        deleteAction += GUIController.CreateInstance("決定(A)", new(-180.0f, -50.0f));
        deleteAction += GUIController.CreateInstance("戻る(B)", new(0.0f, -50.0f));
    }
    void Init()
    {
        if (!GameObject.Find("Player_forMusicSelect"))
        {
            CreatePlayer();
        }
        selectNum = new int[3];
        mCurrNotesData = new string[4];
        mSceneState = SceneState.MusicSelect;
        mAudio = GetComponent<AudioSource>();
        enter = Resources.Load<AudioClip>("SoundEffect/Enter");
        cancel = Resources.Load<AudioClip>("SoundEffect/Cancel");
        scroll = Resources.Load<AudioClip>("SoundEffect/Scroll");
        beep = Resources.Load<AudioClip>("SoundEffect/Beep");
    }

    // 追記
    private async void LoadSceneRef(string musicPath_, string notesPath_)
    {
        // 選択された楽曲とノーツのファイルを設定
        foreach (var obj in AssetLoadConfig.ReferencesAssets)
        {
            if (obj.ObjectPath == "Music")
            {
                var guid = AssetDatabase.AssetPathToGUID("Assets/Prefab/Music/" + musicPath_);
                obj.AssetReference = new AssetReference(guid);
            }

            if (obj.ObjectPath == "TextAsset")
            {
                var guid = AssetDatabase.AssetPathToGUID("Assets/Prefab/TextData/NotesData/" + notesPath_);
                obj.AssetReference = new AssetReference(guid);
            }
        }

        string naxtSceneName = "NotesTest";
        await DataTransferSystem.LoadSceneRef(AssetLoadConfig, naxtSceneName);
    }
}

public interface IMusicSelecter
{
    
    int[] SelectNum { get; }
    void GoForward();
    void GoBack();
    void Enter();
    void Undo();
}

public interface ILevelSelecter
{
    SceneState mSceneState { get; }
    int[] SelectNum { get; }
    int MaxValue { get; }
    float untouchableTimer { get; }
}