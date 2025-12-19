using System;
using TMPro;
using UnityEngine;

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
    public static (GameObject, Action) CreateInstance(string msg)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("ModeSelect/Popup"));
        go.GetComponent<PopupController>().mText.text = msg;
        Action f = () => { Destroy(go); };
        return (go, f);
    }
}
