using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
 

public class Ooo_Fade : MonoBehaviour
{
    [SerializeField] public Image fadeImage;  //フェード画像
    [SerializeField] public TextMeshProUGUI fadeText;    //フェードテキスト
    [SerializeField] private float fadeDuration;   //フェード時間

    void Update()
    {
        fadeImage.DOFade(0f, fadeDuration);
        fadeText.DOFade(0f, fadeDuration);
    }
}
