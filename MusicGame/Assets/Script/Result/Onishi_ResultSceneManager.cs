using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Onishi_ResultSceneManager : MonoBehaviour
{
    [SerializeField] private Text Txt_Score;                        //レガシーテキスト版スコア
    [SerializeField] private TMP_Text Txt_PerfectCnt;               //パーフェクト数
    [SerializeField] private TMP_Text Txt_GoodCnt;                  //グッド数
    [SerializeField] private TMP_Text Txt_MissCnt;                  //ミス数
    [SerializeField] private GameObject Img_ClearLamp;              //クリアランプ
    [SerializeField] private RawImage Img_MusicJacket;              //楽曲のジャケット
    [SerializeField] private TMP_Text Txt_MusicName;                //楽曲名

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //スコア、判定数の取得
        //仮でint-2025/11/11
        int score = 2356;
        int perfectCnt = 1278;
        int goodCnt = 10;
        int missCnt = 0;

        //スコア、判定数の表示
        {
            Txt_Score.DOCounter(0, score, 0.5f, false);
            Txt_PerfectCnt.text = "Perfect:" + perfectCnt.ToString().PadLeft(4, ' ');
            Txt_GoodCnt.text = "Good:" + goodCnt.ToString().PadLeft(4, ' ');
            Txt_MissCnt.text = "Miss:" + missCnt.ToString().PadLeft(4, ' ');
        }

        //判定数のテキストをフェードインさせる
        { 
            Txt_PerfectCnt.transform.DOLocalMoveX(-380f, 1f).SetDelay(0.5f);
            Txt_PerfectCnt.DOFade(1f, 1f).SetDelay(0.5f);
            Txt_GoodCnt.transform.DOLocalMoveX(-380f, 1f).SetDelay(0.5f);
            Txt_GoodCnt.DOFade(1f, 1f).SetDelay(0.5f);
            Txt_MissCnt.transform.DOLocalMoveX(-380f, 1f).SetDelay(0.5f);
            Txt_MissCnt.DOFade(1f, 1f).SetDelay(0.5f);
        }

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
