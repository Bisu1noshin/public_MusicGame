using System;
using TMPro;
using UnityEngine;

namespace ModeSelect
{
    public class PropertyController : MonoBehaviour
    {
        TextMeshProUGUI mText;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            mText = GetComponentInChildren<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void SetText(string msg)
        {
            mText.text = msg;
        }
        public static (PropertyController, Action) CreateInstance(GameObject res)
        {
            GameObject go = Instantiate(res);
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.transform.SetLocalPositionAndRotation(new Vector2(500, 0), Quaternion.identity);
            go.transform.localScale = Vector3.one;
            Action f = () => Destroy(go);
            return (go.GetComponent<PropertyController>(), f);
        }
    }
}

