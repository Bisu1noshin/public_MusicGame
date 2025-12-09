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
    ButtonState mState;
    RectTransform mRect;

    private void Awake()
    {
        mText = transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        mAudio = GetComponent<AudioSource>();
        mThumbnail = transform.GetChild(0).GetComponent<Image>();
        mRect = GetComponent<RectTransform>();
        mState = ButtonState.Appear;
    }

    // Update is called once per frame
    void Update()
    {
        switch (mState)
        {
            case ButtonState.Appear:
                float y = Mathf.Lerp(mRect.anchoredPosition.y, 0.0f, 0.2f);
                mRect.anchoredPosition = new(550.0f, y);
                if (Mathf.Abs(y) < 0.1f)
                {
                    mRect.anchoredPosition = new(550.0f, 0.0f);
                    mState = ButtonState.Active;
                }
                break;
            case ButtonState.Active:
                break;
            case ButtonState.Dead:
                y = Mathf.Lerp(mRect.anchoredPosition.y, -1e4f, 0.1f);
                mRect.anchoredPosition = new(550.0f, y);
                if (Mathf.Abs(y + 1000.0f) < 0.1f)
                {
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
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
        string res = "DemoMusic/" + path_;
        return res;
    }
    public static Action CreateInstance()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("MusicSelecter/Property"));
        go.name = "Property";
        go.transform.SetParent(GameObject.Find("Canvas").transform);
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        PropertyController pc = go.GetComponent<PropertyController>();
        pc.mRect.anchoredPosition = new(550.0f, -1000.0f);

        Action f = () => { pc.mState = ButtonState.Dead; };
        return f;
    }
}
