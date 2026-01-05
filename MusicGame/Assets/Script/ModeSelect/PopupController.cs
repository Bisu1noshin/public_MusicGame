using System;
using TMPro;
using UnityEngine;

namespace ModeSelect
{
    public class PopupController : MonoBehaviour
    {
        public TextMeshProUGUI mText;
        TextMeshProUGUI mCurrValue; //普段使いはしないこと！！！エラーになります
        bool normal = true;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            mText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        void SetInfo(bool n_)
        {
            normal = n_;
            if (!n_)
            {
                mCurrValue = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            }
        }
        public void SetText(string str_)
        {
            mText.text = str_;
        }
        public void SetValue(float v_)
        {
            if (normal) { return; }
            mCurrValue.text = "現在：" + v_.ToString();
        }
        public static Action CreateInstance(string msg)
        {
            GameObject res = Resources.Load<GameObject>("ModeSelect/Popup");
            GameObject go = Instantiate(res);
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            go.transform.localScale = Vector3.one;
            var ppc = go.GetComponent<PopupController>();
            ppc.SetText(msg);
            Action f = () => Destroy(go);
            return f;
        }

        public static (PopupController, Action) CreateInstanceForNotesSpeed(bool isController)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("ModeSelect/Popup_forNotesSpeed"));
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            go.transform.localScale = Vector3.one;
            var ppc = go.GetComponent<PopupController>();
            ppc.SetInfo(false);
            ppc.SetText((isController ? "左スティック上下" : "WキーとSキー") + "で\nノーツ速度の調整ができます。");
            Action f = () => Destroy(go);
            return (ppc, f);
        }
    }
}