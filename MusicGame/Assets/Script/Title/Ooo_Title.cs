using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class Ooo_Title : MonoBehaviour
{
    [SerializeField] Image TitleImage;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //イメージ回転
        transform.DORotate(new Vector3(0f, 0f, 360f), 3f, RotateMode.FastBeyond360);    //5秒でｚ軸を360度回転
        //イメージスケイル
        transform.DOScale(new Vector3(5f, 5f, 0f), 1f);     //1秒後X、Y方向を2倍に変更
    }
}
