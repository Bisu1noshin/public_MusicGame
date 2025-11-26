using UnityEngine;
using TMPro;

public class ButtonController : MonoBehaviour
{
    IMusicSelecter musicSelecter;
    const float buttonPadding = 180;
    TextMeshProUGUI mText;
    RectTransform rectT;
    int listNum;
    private void Awake()
    {
        mText = GetComponentInChildren<TextMeshProUGUI>();
        rectT = GetComponent<RectTransform>();
        musicSelecter = GameObject.Find("SceneManager").GetComponent<MusicSelectSceneManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = new(-350, 0);
        pos.y += (musicSelecter.SelectNum - listNum) * 1.3f * buttonPadding;
        rectT.anchoredPosition = pos;
        if (musicSelecter.SelectNum == listNum)
        {
            transform.localScale = Vector3.one * 1.2f;
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
    public void SetProperty(string text, int value = 0)
    {
        mText.text = text;
        listNum = value;
    }
    public static ButtonController CreateButton(string text, int value = 0)
    {
        GameObject go = Instantiate(Resources.Load("MusicSelecter/button") as GameObject);
        go.name = text;
        go.transform.SetParent(GameObject.Find("Canvas").transform.GetChild(2).transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        ButtonController controller = go.GetComponent<ButtonController>();
        controller.SetProperty(text, value);
        return controller;
    }
}
