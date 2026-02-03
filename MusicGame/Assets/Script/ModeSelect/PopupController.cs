using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace ModeSelect
{
    public enum PopupType
    {
        Normal, Text, Sprite
    }
    public class PopupController : MonoBehaviour
    {
        public TextMeshProUGUI mText;
        TextMeshProUGUI mCurrValue; //普段使いはしないこと！！！エラーになります
        Image mCurrImage;
        PopupType type = PopupType.Normal;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            
        }
        void SetInfo(PopupType n_)
        {
            type = n_;
            switch (n_)
            {
                case PopupType.Normal:
                    mText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                    break;
                case PopupType.Text:
                    mText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                    mCurrValue = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                    break;
                case PopupType.Sprite:
                    mCurrImage = transform.GetChild(0).GetComponent<Image>();
                    //try
                    //{
                    //    mCurrImage = transform.GetChild(0).GetComponent<Image>();
                    //}
                    //catch(NullReferenceException nre)
                    //{
                    //    throw new Exception("NullReferenceException", nre);
                    //}
                    break;
                default:
                    break;
            }
        }
        public void SetText(string str_, int size)
        {
            if (type == PopupType.Sprite) return;
            mText.text = str_;
            mText.fontSize = size;
        }
        public void SetValue(string v_, int size = 96)
        {
            if (type != PopupType.Text) { return; }
            mCurrValue.text = "現在：" + v_;
            mCurrValue.fontSize = size;
        }
        public void SetImage(Sprite img_)
        {
            if (type != PopupType.Sprite) return;
            mCurrImage.sprite = img_;
        }
        public static Action CreateInstance(GameObject res, string msg, int size = 96)
        {
            GameObject go = Instantiate(res);
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            go.transform.localScale = Vector3.one;
            var ppc = go.GetComponent<PopupController>();
            ppc.SetInfo(PopupType.Normal);
            ppc.SetText(msg, size);
            Action f = () => Destroy(go);
            return f;
        }

        public static (PopupController, Action) CreateInstanceForNotesSpeed(GameObject res, string msg, int size = 96)
        {
            GameObject go = Instantiate(res);
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            go.transform.localScale = Vector3.one;
            var ppc = go.GetComponent<PopupController>();
            ppc.SetInfo(PopupType.Text);
            ppc.SetText(msg, size);
            Action f = () => Destroy(go);
            return (ppc, f);
        }
        public static (PopupController, Action) CreateInstanceImage(GameObject res, Sprite image_)
        {
            GameObject go = Instantiate(res);
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            go.transform.localScale = Vector3.one;
            var ppc = go.GetComponent<PopupController>();
            ppc.SetInfo(PopupType.Sprite);
            ppc.SetImage(image_);
            Action f = () => Destroy(go);
            return (ppc, f);
        }
    }
}