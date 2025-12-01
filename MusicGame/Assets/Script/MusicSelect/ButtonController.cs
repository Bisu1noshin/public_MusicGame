using UnityEngine;
using TMPro;

public class ButtonController : MonoBehaviour
{
    IMusicSelecter musicSelecter;
    const float buttonPadding = 180;
    TextMeshProUGUI mText;
    RectTransform rectT;
    TextScroller mTextScroller;
    PropertyController mProperty;
    int listNum;
    string audioPath;
    private void Awake()
    {
        mText = GetComponentInChildren<TextMeshProUGUI>();
        rectT = GetComponent<RectTransform>();
        musicSelecter = GameObject.Find("SceneManager").GetComponent<MusicSelectSceneManager>();
        mTextScroller = GetComponentInChildren<TextScroller>();
        mProperty = GameObject.Find("Property").GetComponent<PropertyController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 pos = new(-350, 0);
        pos.y += (musicSelecter.SelectNum - listNum) * 1.3f * buttonPadding;
        rectT.anchoredPosition = pos;
        if (musicSelecter.SelectNum == listNum)
        {
            transform.localScale = Vector3.one * 1.2f;
            if (!mTextScroller.enabled)
            {
                mTextScroller.enabled = true;
                mProperty.SetProperty(mText.text, audioPath, null);
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
    }
    public void SetProperty(string text, int value, string audioPath_)
    {
        mText.text = text;
        listNum = value;
        audioPath = audioPath_;
    }
    public static ButtonController CreateButton(string text, int value, string audioPath_)
    {
        GameObject go = Instantiate(Resources.Load("MusicSelecter/button") as GameObject);
        go.name = text;
        go.transform.SetParent(GameObject.Find("Canvas").transform.GetChild(2).transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        ButtonController controller = go.GetComponent<ButtonController>();
        controller.SetProperty(text, value, audioPath_);
        return controller;
    }
}
