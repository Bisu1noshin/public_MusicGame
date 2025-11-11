using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Onishi_ResultSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Txt_Score;             //スコア
    [SerializeField] private TextMeshProUGUI Txt_PerfectCnt;        //パーフェクト数
    [SerializeField] private TextMeshProUGUI Txt_GoodCnt;           //グッド数
    [SerializeField] private TextMeshProUGUI Txt_MissCnt;           //ミス数
    [SerializeField] private GameObject Img_ClearLamp;              //クリアランプ
    [SerializeField] private RawImage Img_MusicJacket;              //楽曲のジャケット
    [SerializeField] private TextMeshProUGUI Txt_MusicName;         //楽曲名

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //スコア、判定数の取得
        //仮でint-2025/11/11
        int score = 2356;
        int perfectCnt = 1278;
        int goodCnt = 10;
        int missCnt = 0;

        //スコア、判定数の表示変更
        Txt_PerfectCnt.text = score.ToString();
        Txt_PerfectCnt.text = perfectCnt.ToString();
        Txt_GoodCnt.text = goodCnt.ToString();
        Txt_MissCnt.text = missCnt.ToString();

        //FC/APの画像変更
        //メソッドは完成、仮画像を使用-2025/11/11
        {
            Texture2D newSprite;
            if (missCnt == 0)
            {
                if (goodCnt == 0) newSprite = Resources.Load("Image/Result/AP_kari") as Texture2D;
                else newSprite = Resources.Load("Image/Result/FC_kari") as Texture2D;
            }
            else newSprite = Resources.Load("Image/Result/Clear_kari") as Texture2D;
            Img_ClearLamp.GetComponent<RawImage>().texture = newSprite;
        }

        //楽曲情報の取得
        //未着手
        //仮で画面表示変更だけ-2025/11/11
        {
            Texture2D newTexture = Resources.Load("Image/MusicJacket/MusicJacket_kari") as Texture2D;
            Img_MusicJacket.texture = newTexture;
            Txt_MusicName.text = "Totemo Nagai Kyokumei Demo Yoyuu De Nagareru ~Totemo Sugoi~";
        }
    }
}
