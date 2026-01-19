using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MusicSelect
{
    public class LevelButtonController : MonoBehaviour
    {
        public static float posX = 0.0f;
        public ButtonState mState { get; private set; }
        ILevelSelecter mSelecter;
        TextMeshProUGUI mText;
        RectTransform mRect;
        int id;

        void Awake()
        {
            mText = GetComponentInChildren<TextMeshProUGUI>();
            //mSelecter = GameObject.Find("SceneManager").GetComponent<MusicSelectSceneManager>();
            if (GameObject.Find("SceneManager").TryGetComponent<MusicSelectSceneManager>(out var mssm))
            {
                mSelecter = mssm;
            }
            else
            {
                Debug.LogError("Error! : SceneManger is NOT found.");
            }
            mRect = GetComponent<RectTransform>();
            mState = ButtonState.Appear;

        }
        // Update is called once per frame
        void Update()
        {
            switch (mState)
            {
                case ButtonState.Appear:
                    float x = Mathf.Lerp(mRect.anchoredPosition.x, posX, 0.2f);
                    mRect.anchoredPosition = new(x, mRect.anchoredPosition.y);
                    if (Mathf.Abs(x - posX) < 1.0f)
                    {
                        mRect.anchoredPosition = new(posX, mRect.anchoredPosition.y);
                        mState = ButtonState.Active;
                    }
                    break;

                case ButtonState.Active:

                    if (mSelecter.SelectNum[1] == id)
                    {
                        transform.localScale = Vector3.one * 1.2f;
                    }
                    else
                    {
                        transform.localScale = Vector3.one;
                    }
                    mRect.anchoredPosition = new Vector2(posX, SetY(id));
                    break;
                case ButtonState.Dead:
                    mRect.localScale = Vector3.one;
                    switch (mSelecter.mSceneState)
                    {
                        case SceneState.MusicSelect:
                            x = Mathf.Lerp(mRect.anchoredPosition.x, 1360.0f, 0.2f);
                            mRect.anchoredPosition = new(x, mRect.anchoredPosition.y);
                            if (Mathf.Abs(1360.0f - x) < 1.0f)
                            {
                                Destroy(gameObject);
                            }
                            break;
                        case SceneState.LevelSelect:
                            Destroy(gameObject);
                            break;
                        case SceneState.EnterGame:
                            x = Mathf.Lerp(mRect.anchoredPosition.x, -1360.0f, 0.2f);
                            mRect.anchoredPosition = new(x, mRect.anchoredPosition.y);
                            if (Mathf.Abs(-1360.0f - x) < 0.1f)
                            {
                                Destroy(gameObject);
                            }
                            break;
                    }

                    break;
            }

        }
        public void SetProperty(int id_, string str_)
        {
            id = id_;
            mText.text = str_;
            mState = ButtonState.Appear;
        }
        public static float SetY(int id_)
        {
            return (id_ + 1) * -200.0f + 350.0f;
        }
        public static Action CreateInstance(GameObject res, int id_, string str_, SceneState ss = SceneState.LevelSelect, bool cantSelect = false)
        {
            GameObject go = Instantiate(res);
            go.transform.SetParent(GameObject.Find("Canvas").transform.GetChild(1).transform);
            go.transform.localRotation = Quaternion.identity;
            go.name = str_;
            go.transform.localScale = Vector3.one;
            if (cantSelect)
            {
                go.GetComponent<Image>().color = Color.gray;
            }

            LevelButtonController lbc = go.GetComponent<LevelButtonController>();
            lbc.SetProperty(id_, str_);
            if (ss == SceneState.EnterGame)
            {
                lbc.mRect.anchoredPosition = new(-1360.0f, SetY(id_));
            }
            else
            {
                lbc.mRect.anchoredPosition = new(1360.0f, SetY(id_));
            }


            Action action = () => { lbc.mState = ButtonState.Dead; };
            return action;
        }
    }

}
