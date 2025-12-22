using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
public enum ButtonState
{
    None = -1,
    Appear,
    Active,
    Dead
}
public class MusicButtonController : MonoBehaviour
{
    

    [SerializeField]public ButtonState mState { get; private set; }
    IMusicSelecter mSelecter;
    const float buttonPadding = 180;
    TextMeshProUGUI mText;
    RectTransform mRect;
    TextScroller mTextScroller;
    PropertyController mProperty;
    int id;
    string audioPath, imagePath;

    private void Awake()
    {
        mText = GetComponentInChildren<TextMeshProUGUI>();
        mRect = GetComponent<RectTransform>();
        mSelecter = GameObject.Find("SceneManager").GetComponent<MusicSelectSceneManager>();
        mTextScroller = GetComponentInChildren<TextScroller>();
        mProperty = GameObject.Find("Property").GetComponent<PropertyController>();
        mState = ButtonState.Appear;
    }

    void Start()
    {
        if (id == 0)
        {
            mProperty.SetProperty(mText.text, audioPath, imagePath);
        }
    }

    void Update()
    {
        switch (mState)
        {
            case ButtonState.Appear:
                float x = Mathf.Lerp(mRect.anchoredPosition.x, -350.0f, 0.2f);
                mRect.anchoredPosition = new(x, mRect.anchoredPosition.y);
                if (Mathf.Abs(350.0f + x) < 0.1f)
                {
                    mState = ButtonState.Active;
                }
                break;
            case ButtonState.Active:
                Vector2 pos = new(-350.0f, 0.0f);
                pos.y += (mSelecter.SelectNum[0] - id) * 1.3f * buttonPadding;
                mRect.anchoredPosition = pos;
                if (mSelecter.SelectNum[0] == id)
                {
                    transform.localScale = Vector3.one * 1.2f;
                    if (!mTextScroller.enabled)
                    {
                        mTextScroller.enabled = true;
                        mProperty.SetProperty(mText.text, audioPath, imagePath);
                    }
                }
                else
                {
                    transform.localScale = Vector3.one;
                    if (mTextScroller.enabled)
                    {
                        mTextScroller.enabled = false;
                    }
                }
                break;
            case ButtonState.Dead:
                mRect.localScale = Vector3.one;
                x = Mathf.Lerp(mRect.anchoredPosition.x, -1360.0f, 0.2f);
                mRect.anchoredPosition = new(x, mRect.anchoredPosition.y);
                if (Mathf.Abs(1360.0f + x) < 1.0f)
                {
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
        
    }
    public void SetInfo(string text_, int id_, string audioPath_, string imagePath_)
    {
        mText.text = text_;
        id = id_;
        audioPath = audioPath_;
        imagePath = imagePath_;
    }
    public static Action CreateInstance(string text_, int id_, string audioPath_, string imagePath_)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("MusicSelecter/MusicButton"));
        go.name = text_;
        go.transform.SetParent(GameObject.Find("Canvas").transform.GetChild(1).transform);
        go.transform.SetLocalPositionAndRotation(new(-1360.0f, id_ * 1.3f * buttonPadding, 0.0f), Quaternion.identity);
        go.transform.localScale = Vector3.one;
        MusicButtonController mbc = go.GetComponent<MusicButtonController>();
        mbc.SetInfo(text_, id_, audioPath_, imagePath_);

        Action f = () => { mbc.mState = ButtonState.Dead; };

        return f;
    }
}
