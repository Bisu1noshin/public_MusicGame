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
        GameObject p = GameObject.Find("Player_forMusicSelect");
        if (p == null)
        {
            player = CreatePlayer();
        }
        else
        {
            player = p.GetComponent<Player.Player_forMusicSelect>();
        }
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
        Debug.Log("Forward");
    }
    public void GoBack()
    {
        Debug.Log("Back");
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
    void GoForward();
    void GoBack();
    void Enter();
    void Undo();
}
