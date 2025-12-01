using System;
using System.Collections.Generic;
using Notes;
using Player;
using Unity.VisualScripting;
using UnityEngine;

enum SceneState
{
    MusicSelect,
    LevelSelect,
    EnterGame
}

public class MusicSelectSceneManager : MonoBehaviour, IMusicSelecter, ILevelSelecter
{
    private Action deleteAction;
    int selectNum, maxValue;
    public int SelectNum => selectNum;
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
        //LoadMusics();
        CreateMusicButtons();
        selectNum = 0;
        mCurrNotesData = new Notes.NotesData[4];
        mSceneState = SceneState.MusicSelect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void LoadMusics()
    //{
    //    //ダミーデータを作成
    //    //musicData = new();
    //    //List<Music.Music> list = new();
    //    //list.Add(new Music.Music("シャイニングスター", "Shining Star.mp3"));
    //    //list.Add(new Music.Music("本来はここに2曲目の曲名が入る", ""));
    //    //list.Add(new Music.Music("本来はここに3曲目の曲名が入る", ""));
    //    //musicData.AddList(list);
    //}
    public void GoForward()
    {
        Debug.Log("Forward");
        selectNum++;
        if (selectNum > mDataBase.musicDatabase.Count - 1)
        {
            selectNum = mDataBase.musicDatabase.Count - 1;
        }
    }
    public void GoBack()
    {
        Debug.Log("Back");
        selectNum--;
        if (selectNum < 0)
        {
            selectNum = 0;
        }
    }
    public void Enter()
    {
        switch (mSceneState)
        {
            case SceneState.MusicSelect:
                mCurrNotesData = mDataBase.musicDatabase[selectNum].notesData;
                deleteAction?.Invoke();
                deleteAction = null;
                //CreateLevelButtons();
                break;
            case SceneState.LevelSelect:

                break;
            case SceneState.EnterGame:
                break;
        }
        if (mSceneState != SceneState.EnterGame)
        {
            mSceneState++;
        }
    }
    public void Undo()
    {
        switch (mSceneState)
        {
            case SceneState.MusicSelect:
                break;
            case SceneState.LevelSelect:
                CreateMusicButtons();
                break;
            case SceneState.EnterGame:
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
            deleteAction += MusicButtonController.CreateButton(m.name, m.id, m.demoMusicPath);
        }
    }

    void CreateLevelButtons()
    {
        for (int i = 0; i < 4; i++)
        {
            if (mCurrNotesData[i] == null) { break; }
            //ここに我参上！！
        }
    }
}

public interface IMusicSelecter
{
    int SelectNum { get; }
    void GoForward();
    void GoBack();
    void Enter();
    void Undo();
}

public interface ILevelSelecter
{
    int SelectNum { get; }
    int MaxValue { get; }
}