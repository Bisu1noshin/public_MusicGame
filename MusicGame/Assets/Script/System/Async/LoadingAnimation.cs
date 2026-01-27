using UnityEngine;
using TMPro;
using System.Collections;

public class LoadingAnimation : MonoBehaviour
{
    TextMeshProUGUI mText;
    private void Awake()
    {
        mText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Animation());
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Animation()
    {
        while (true)
        {
            SetText("Now Loading");
            yield return new WaitForSeconds(0.05f);
            SetText("Now Loading.");
            yield return new WaitForSeconds(0.05f);
            SetText("Now Loading..");
            yield return new WaitForSeconds(0.05f);
            SetText("Now Loading...");
            yield return new WaitForSeconds(0.05f);
        }
    }
    void SetText(string str)
    {
        mText.text = str;
    }
}
