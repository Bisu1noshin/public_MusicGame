using UnityEngine;
using TMPro;

public class ButtonController : MonoBehaviour
{
    TextMeshProUGUI mText;
    private void Awake()
    {
        mText = GetComponentInChildren<TextMeshProUGUI>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetName(string text)
    {
        mText.text = text;
    }
    public static ButtonController CreateButton(string text)
    {
        GameObject go = Instantiate(Resources.Load("MusicSelecter/button") as GameObject);
        go.name = text;
        go.transform.SetParent(GameObject.Find("Canvas").transform.GetChild(2).transform);
        ButtonController controller = go.GetComponent<ButtonController>();
        controller.SetName(text);
        return controller;
    }
}
