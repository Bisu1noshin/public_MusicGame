using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace MusicSelect
{
    public class GUIController : MonoBehaviour
    {
        TextMeshProUGUI mText;
        RectTransform mRect;
        ILevelSelecter mSelecter;
        Vector2 mOffsetPos;
        float timer;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            mText = GetComponentInChildren<TextMeshProUGUI>();
            mRect = GetComponent<RectTransform>();
            mSelecter = GameObject.Find("SceneManager").GetComponent<MusicSelectSceneManager>();
            timer = 0.65f;
        }

        // Update is called once per frame
        void Update()
        {
            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;
                mRect.localScale = Vector3.zero;
            }
            else
            {
                mRect.localScale = Vector3.one * 0.5f;
                switch (mSelecter.mSceneState)
                {
                    case SceneState.MusicSelect:
                        mRect.anchoredPosition = new Vector2(125.0f, 0.0f) + mOffsetPos;
                        break;
                    case SceneState.LevelSelect:
                        mRect.anchoredPosition = new Vector2(475.0f, (mSelecter.SelectNum[1] + 1) * -200.0f + 400.0f) + mOffsetPos;
                        break;
                    default:
                        break;
                }
            }

        }
        public void SetInfo(string str_, Vector2 offset)
        {
            mText.text = str_;
            mOffsetPos = offset;
        }
        public static Action CreateInstance(GameObject res, string str_, Vector2 offset)
        {
            GameObject go = Instantiate(res);
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.transform.SetLocalPositionAndRotation(Vector2.zero, Quaternion.identity);
            go.transform.localScale = Vector3.one * 0.5f;
            GUIController gc = go.GetComponent<GUIController>();
            gc.SetInfo(str_, offset);

            Action f = () => { Destroy(go); };
            return f;
        }
    }

}
