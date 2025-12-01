using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;

public class MusicSelectSceneManager : MonoBehaviour, IMusicSelecter
{
    Music.MusicData musicData;
    int selectNum;
    public int SelectNum => selectNum;
    Player.Player_forMusicSelect player;

    private void Awake()
    {
        //Application.targetFrameRate = 60;
        GameObject p = GameObject.Find("Player_forMusicSelect");
        if (p == null)
        {
            player = CreatePlayer();
        }
        else
        {
            player = p.GetComponent<Player.Player_forMusicSelect>();
        }
        LoadMusics();
        int i = 0;
        foreach (Music.Music m in musicData.music)
        {
            //ButtonController.CreateButton(m.name, i);
            i++;
        }
        selectNum = 0;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadMusics()
    {
        //ダミーデータを作成
        musicData = new();
        List<Music.Music> list = new();
        list.Add(new Music.Music("シャイニングスター", "Shining Star.mp3"));
        list.Add(new Music.Music("本来はここに2曲目の曲名が入る", ""));
        list.Add(new Music.Music("本来はここに3曲目の曲名が入る", ""));
        musicData.AddList(list);
    }
    public void GoForward()
    {
        Debug.Log("Forward");
        selectNum++;
        if (selectNum > musicData.music.Count - 1)
        {
            selectNum = musicData.music.Count - 1;
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
        Debug.Log("Enter");
    }
    public void Undo()
    {
        Debug.Log("Undo");
    }
    Player_forMusicSelect CreatePlayer()
    {
        GameObject res = Resources.Load("MusicSelecter/Player_forMusicSelect") as GameObject;
        GameObject instance = Instantiate(res);
        return instance.GetComponent<Player.Player_forMusicSelect>();
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
