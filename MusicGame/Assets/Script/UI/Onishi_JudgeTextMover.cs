using UnityEngine;
using TMPro;
using DG.Tweening;

public class Onishi_JudgeTextMover : MonoBehaviour
{
    private TMP_Text txt;

    void Start()
    {
        txt = GetComponent<TMP_Text>();
        //最初透明
        Color c = txt.color;
        c.a = 0f;
        txt.color = c;

        var seq = DOTween.Sequence();

        //出てくる
        {
            seq.Append(txt.DOFade(1f, 0.5f).SetEase(Ease.OutQuart));
            seq.Join(txt.transform.DOLocalMoveY(100f, 0.5f).SetEase(Ease.OutQuart));
        }

        //消える
        {
            seq.Append(txt.DOFade(0f, 0.5f).SetEase(Ease.OutQuart).OnComplete(() => Destroy(txt.gameObject)));
        }
    }
}