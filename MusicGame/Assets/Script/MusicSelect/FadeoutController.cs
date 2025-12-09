using UnityEngine;
using UnityEngine.UI;

public class FadeoutController : MonoBehaviour
{
    Image mImage;
    float alpha = 0.0f;
    float timer = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        mImage = GetComponent<Image>();
        mImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        alpha = Mathf.Clamp(0.0f, 1.0f, timer / 2.0f);
        mImage.color = new(1.0f, 1.0f, 1.0f, alpha);
    }
}
