using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PropertyController : MonoBehaviour
{
    //IMusicSelecter mSelecter;
    TextMeshProUGUI mText;
    AudioSource mAudio;
    Image mThumbnail;
    private void Awake()
    {
        mText = transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        mAudio = GetComponent<AudioSource>();
        mThumbnail = transform.GetChild(0).GetComponent<Image>();
        //mSelecter = GameObject.Find("SceneManager").GetComponent<MusicSelectSceneManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetProperty(string text, string audioPath, string imagePath)
    {
        mText.text = text;
        if (audioPath != string.Empty) 
        {
            AudioClip clip = Resources.Load(CreateMusicPath(audioPath)) as AudioClip;
            mAudio.clip = clip;
            mAudio.Play();
        }
        if (imagePath != string.Empty) 
        {
            mThumbnail = Resources.Load<Image>(imagePath);
        }
    }
    string CreateMusicPath(string path_)
    {
        string res = "Music/" + path_;
        return res;
    }
    public static Action CreateInstance()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("MusicSelecter/Property"));
        go.name = "Property";
        go.transform.SetParent(GameObject.Find("Canvas").transform);
        go.transform.localPosition = new Vector2(550, 0);
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = Quaternion.identity;

        Action f = () => { Destroy(go); };
        return f;
    }
}
