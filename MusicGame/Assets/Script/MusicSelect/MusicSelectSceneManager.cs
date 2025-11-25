using NUnit.Framework;
using Player;
using UnityEngine;

public class MusicSelectSceneManager : MonoBehaviour, IMusicSelecter
{
    Music.MusicData musicData;
    int selectNum;
    Player.Player_forMusicSelect player;

    private void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        selectNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadMusics()
    {

    }
    public void GoForward()
    {

    }
    public void GoBack()
    {

    }
    public void Enter()
    {
        
    }
    public void Undo()
    {

    }
}

public interface IMusicSelecter
{
    void GoForward();
    void GoBack();
    void Enter();
    void Undo();
}
