using System;
using TMPro;
using UnityEngine;

public class LevelButtonController : MonoBehaviour
{
    ILevelSelecter mSelecter;
    TextMeshProUGUI mText;
    RectTransform mRect;
    int id;

    void Awake()
    {
        mText = GetComponentInChildren<TextMeshProUGUI>();
        mSelecter = GameObject.Find("MusicSelectSceneManager").GetComponent<MusicSelectSceneManager>();
        mRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mSelecter.SelectNum == id)
        {
            transform.localScale = Vector3.one * 1.2f;
        }
        else
        {
            transform.localScale = Vector3.one;
        }
        if (mSelecter.MaxValue == 4)
        {
            mRect.anchoredPosition = ((id + 1) * -4) * 250 * Vector2.up;
        }
        else
        {
            
        }
    }
    public void SetProperty(int id_, string str_)
    {
        id = id_;
        mText.text = str_;
    }
    public static Action CreateButton(int id_, string str_)
    {
        GameObject res = Resources.Load("MusicSelecter/LevelButton") as GameObject;
        GameObject go = Instantiate(res);
        go.transform.SetParent(GameObject.Find("Canvas").transform, false);
        go.transform.localPosition = new(-350, 0, 0);
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = Quaternion.identity;
        LevelButtonController lbc = go.GetComponent<LevelButtonController>();
        lbc.SetProperty(id_, str_);

        Action action = () => { Destroy(go); };
        return action;
    }
}
