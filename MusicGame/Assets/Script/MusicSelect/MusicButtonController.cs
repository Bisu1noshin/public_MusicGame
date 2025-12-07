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
    float timer = 0.0f;
    string audioPath;

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
            mProperty.SetProperty(mText.text, audioPath, string.Empty);
        }
    }

    void Update()
    {
        switch (mState)
        {
            case ButtonState.Appear:
                timer += Time.deltaTime;
                mRect.localScale = Vector3.Lerp(mRect.localScale, Vector3.one, 0.5f);
                if (1.0f - mRect.localScale.x < 1e-3f)
                {
                    Debug.Log($"Time : {timer}");
                    timer = 0.0f;
                    mRect.localScale = Vector3.one;
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
                        mProperty.SetProperty(mText.text, audioPath, string.Empty);
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
                timer += Time.deltaTime;
                float x = Mathf.Lerp(-350.0f, -1360.0f, timer / 0.2f);
                mRect.anchoredPosition = new(x, mRect.anchoredPosition.y);
                if (timer >= 0.2f)
                {
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
        
    }
    public void SetInfo(string text_, int id_, string audioPath_)
    {
        mText.text = text_;
        id = id_;
        audioPath = audioPath_;
        
    }
    public static Action CreateInstance(string text_, int id_, string audioPath_)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("MusicSelecter/MusicButton"));
        go.name = text_;
        go.transform.SetParent(GameObject.Find("Canvas").transform.GetChild(1).transform);
        go.transform.SetLocalPositionAndRotation(new(-350.0f, id_ * 1.3f * buttonPadding, 0.0f), Quaternion.identity);
        go.transform.localScale = Vector3.zero;
        MusicButtonController mbc = go.GetComponent<MusicButtonController>();
        mbc.SetInfo(text_, id_, audioPath_);

        Action f = () => { mbc.mState = ButtonState.Dead; };

        return f;
    }
}
