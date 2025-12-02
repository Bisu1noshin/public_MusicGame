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
        //mSelecter = GameObject.Find("SceneManager").GetComponent<MusicSelectSceneManager>();
        if (GameObject.Find("SceneManager").TryGetComponent<MusicSelectSceneManager>(out var mssm)) {
            mSelecter = mssm;
        }
        else
        {
            Debug.LogError("Error! : SceneManger is NOT found.");
        }
        mRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mSelecter.SelectNum[1] == id)
        {
            transform.localScale = Vector3.one * 1.2f;
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        mRect.anchoredPosition = new Vector2(-350.0f, (id + 1) * -200.0f + 510.0f);
    }
    public void SetProperty(int id_, string str_)
    {
        id = id_;
        mText.text = str_;
    }
    public static Action CreateInstance(int id_, string str_)
    {
        GameObject res = Resources.Load("MusicSelecter/LevelButton") as GameObject;
        GameObject go = Instantiate(res);
        go.transform.SetParent(GameObject.Find("Canvas").transform.GetChild(1).transform);
        go.transform.localPosition = new(-350, 0, 0);
        go.name = str_;
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = Quaternion.identity;
        LevelButtonController lbc = go.GetComponent<LevelButtonController>();
        lbc.SetProperty(id_, str_);

        Action action = () => { Destroy(go); };
        return action;
    }
}
