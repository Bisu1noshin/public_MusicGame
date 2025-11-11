using UnityEngine;
using TMPro;
using DG.Tweening;


public class Ooo_Fade : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI fadeText;    //フェードテキスト
    [SerializeField] public float fadeDuration;        //フェード時間

    void Update()
    {
        
        fadeText.DOFade(1f, fadeDuration);  //fadeDuration秒かけて透明度を1へ

    }
}
