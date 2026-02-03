using LoadForAsync;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Kameda_ResourceManager : MonoBehaviour, IResourceManager, ISetAsyncObjects
{
    public Action ReleaseAll { get; set; }
    Dictionary.Dic<string, GameObject> mModeObjectRes;
    Dictionary.Dic<string, GameObject> mMusicObjectRes;
    Dictionary.Dic<string, AudioClip> mAudioRes;
    Dictionary.Dic<string, Sprite> mSpriteRes;
    bool loaded = false;

    public void StartLoadSync()
    {
        if (!loaded)
        {
            SetSyncObjects();
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this);

        // 自分を破壊する処理の追加
        {
            Action deleateObject = () =>
            {
                if (this.gameObject != null) Destroy(this.gameObject);
            };

            ReleaseAll += deleateObject;
        }
    }

    void OnDisable()
    {
        ReleaseAll?.Invoke();
    }

    public void SetAsyncObjects(LoadObjectTable ObjectTable)
    {
        mModeObjectRes = new(new()
        {
            { "Button", ObjectTable.GetAsset<GameObject>("Mode_Button") },
            { "Popup", ObjectTable.GetAsset<GameObject>("Mode_Popup") },
            { "Popup_Speed", ObjectTable.GetAsset<GameObject>("Mode_Popup_Speed") },
            { "Popup_Intro", ObjectTable.GetAsset<GameObject>("Mode_Popup_Intro") },
            { "Property", ObjectTable.GetAsset<GameObject>("Mode_Property") },
            { "Player", ObjectTable.GetAsset<GameObject>("Mode_Player") },
            { "mCursor", ObjectTable.GetAsset<GameObject>("Mode_Cursor") },
            { "mPopupCursor", ObjectTable.GetAsset<GameObject>("Mode_Cursor_Popup") }
        });
        mMusicObjectRes = new(new()
        {
            { "Music_Button", ObjectTable.GetAsset<GameObject>("Music_Music_Button") },
            { "Property", ObjectTable.GetAsset<GameObject>("Music_Property") },
            { "Popup", ObjectTable.GetAsset<GameObject>("Music_Popup") },
            { "Player", ObjectTable.GetAsset<GameObject>("Music_Player") },
            { "GUI", ObjectTable.GetAsset<GameObject>("Music_GUI") },
            { "Level_Button", ObjectTable.GetAsset<GameObject>("Music_Level_Button") }
        })
        ;
        mAudioRes = new(new()
        {
            { "Enter", ObjectTable.GetAsset<AudioClip>("SE_Enter") },
            { "Cancel", ObjectTable.GetAsset<AudioClip>("SE_Cancel") },
            { "Scroll", ObjectTable.GetAsset<AudioClip>("SE_Scroll") },
            { "Beep", ObjectTable.GetAsset<AudioClip>("SE_Beep") },
            { "ShiningStar", ObjectTable.GetAsset<AudioClip>("Demo_ShiningStar") },
            { "Gengaozo", ObjectTable.GetAsset<AudioClip>("Demo_G_e_n_g_a_o_z_o") },
            { "Chronomia", ObjectTable.GetAsset<AudioClip>("Demo_Chronomia") },
            { "NewYorkBackRaise", ObjectTable.GetAsset<AudioClip>("Demo_NewYorkBackRaise") }
        });
        mSpriteRes = new(new()
        {
            { "ShiningStar", ObjectTable.GetAsset <Sprite>("Jacket_ShiningStar") },
            { "Gengaozo", ObjectTable.GetAsset<Sprite>("Jacket_G_e_n_g_a_o_z_o") },
            { "Chronomia", ObjectTable.GetAsset<Sprite>("Jacket_Chronomia") },
            { "NewYorkBackRaise", ObjectTable.GetAsset<Sprite>("Jacket_NewYorkBackRaise") },
            { "Intro_K", ObjectTable.GetAsset<Sprite>("Intro_Keyboard") },
            { "Intro_C", ObjectTable.GetAsset<Sprite>("Intro_Controller") }
        });

        loaded = true;
    }
    void SetSyncObjects()
    {
        mModeObjectRes = new(new()
        {
            { "Button", Resources.Load<GameObject>("ModeSelect/ModeButton") },
            { "Popup", Resources.Load<GameObject>("ModeSelect/Popup") },
            { "Popup_Speed", Resources.Load<GameObject>("ModeSelect/Popup_forNotesSpeed") },
            { "Popup_Intro", Resources.Load<GameObject>("ModeSelect/Popup_Intro") },
            { "Property", Resources.Load<GameObject>("ModeSelect/Property") },
            { "Player", Resources.Load<GameObject>("ModeSelect/Player") },
            { "mCursor", Resources.Load<GameObject>("ModeSelect/Cursor") },
            { "mPopupCursor", Resources.Load<GameObject>("ModeSelect/Cursor_forPopup") }
        });
        mMusicObjectRes = new(new()
        {
            { "Music_Button", Resources.Load<GameObject>("MusicSelecter/MusicButton") },
            { "Property", Resources.Load<GameObject>("MusicSelecter/Property") },
            { "Popup", Resources.Load<GameObject>("MusicSelecter/Popup_EnterGame") },
            { "Player", Resources.Load<GameObject>("MusicSelecter/Player_forMusicSelect") },
            { "GUI", Resources.Load<GameObject>("MusicSelecter/GUI") },
            { "Level_Button", Resources.Load<GameObject>("MusicSelecter/LevelButton") }
        });
        mAudioRes = new(new()
        {
            { "Enter", Resources.Load<AudioClip>("SoundEffect/Enter") },
            { "Cancel", Resources.Load<AudioClip>("SoundEffect/Cancel") },
            { "Scroll", Resources.Load<AudioClip>("SoundEffect/Scroll") },
            { "Beep", Resources.Load<AudioClip>("SoundEffect/Beep") },
            { "ShiningStar", Resources.Load<AudioClip>("DemoMusic/ShiningStar") },
            { "Gengaozo", Resources.Load<AudioClip>("DemoMusic/G_e_n_g_a_o_z_o") },
            { "Chronomia", Resources.Load<AudioClip>("DemoMusic/Chronomia") },
            { "NewYorkBackRaise", Resources.Load<AudioClip>("DemoMusic/NewYorkBackRaise") }
        });
        mSpriteRes = new(new()
        {
            { "ShiningStar", Resources.Load<Sprite>("Image/MusicJacket/ShiningStar") },
            { "Gengaozo", Resources.Load<Sprite>("Image/MusicJacket/G_e_n_g_a_o_z_o") },
            { "Chronomia", Resources.Load<Sprite>("Image/MusicJacket/Chronomia") },
            { "NewYorkBackRaise", Resources.Load<Sprite>("Image/MusicJacket/NewYorkBackRaise") },
        });
        loaded = true;
    }

    public GameObject GetGameObject(string path, bool isModeRes = true)
    {
        if (path == null)
        {
            Debug.LogError("Error! path is NULL");
            return null;
        }
        if (isModeRes)
        {
            return mModeObjectRes.GetValue(path);
        }
        else
        {
            return mMusicObjectRes.GetValue(path);
        }
    }
    public AudioClip GetAudioClip(string path)
    {
        if (path == null)
        {
            Debug.LogError("Error! path is NULL");
            return null;
        }
        return mAudioRes.GetValue(path);
    }
    public Sprite GetSprite(string path)
    {
        if (path == null)
        {
            Debug.LogError("Error! path is NULL");
            return null;
        }
        return mSpriteRes.GetValue(path);
    }
}

public interface IResourceManager
{
    GameObject GetGameObject(string path, bool isModeRes = true);
    AudioClip GetAudioClip(string path);
    Sprite GetSprite(string path);
    void StartLoadSync();

    Action ReleaseAll { get; set; }
}