using System;
using TMPro;
using UnityEngine;

namespace ModeSelect
{
    public class PopupController : MonoBehaviour
    {
        public TextMeshProUGUI mText;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            mText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void SetText(string str_)
        {
            mText.text = str_;
        }
        public static Action CreateInstance(string msg)
        {
            GameObject res = Resources.Load<GameObject>("ModeSelect/Popup");
            Debug.Log($"Load : {res.name}");
            GameObject go = Instantiate(res);        
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            go.transform.localScale = Vector3.one;
            go.GetComponent<PopupController>().SetText(msg);
            Action f = () => { Destroy(go); };
            return f;
        }
    }
}