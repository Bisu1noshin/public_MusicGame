using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GameInfo;
using UnityEngine.SceneManagement;
using System.Collections;

public class Multi_ResultSceneManager : MonoBehaviour
{
    [SerializeField] Text Txt_Score;                        //レガシーテキスト版スコア
    [SerializeField] TMP_Text Txt_PerfectCnt;               //パーフェクト数
    [SerializeField] TMP_Text Txt_GoodCnt;                  //グッド数
    [SerializeField] TMP_Text Txt_MissCnt;                  //ミス数
    [SerializeField] GameObject Img_ClearLamp;              //クリアランプ
    [SerializeField] RawImage Img_MusicJacket;              //楽曲のジャケット
    [SerializeField] TMP_Text Txt_MusicName;
    [SerializeField] GameObject Img_Bars;                   //ゲージの外枠
    [SerializeField] Image Img_BarRed;                      //赤ゲージ
    [SerializeField] Image Img_BarBlue;                     //青ゲージ
    [SerializeField] RawImage Img_WinLose;                     //勝敗画像

    const float finalScorePos = 40.0f;
    float bluePer;
    [SerializeField] bool DebugMode = default;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //大西君のコードをコピー

        //スコア、判定数の取得
        int score = 0;
        int[] scoreCnt = new int[3];
        if (DebugMode)
        {
            scoreCnt[0] = 0;
            scoreCnt[1] = 0;
            scoreCnt[2] = 0;
        }
        else foreach (var sc in SingletonDataManager.instance.score)
        {
            scoreCnt[(int)sc]++;
        }

        score = scoreCnt[2] * 2 + scoreCnt[1];

        //スコア、判定数の表示
        {
            Txt_Score.DOCounter(0, score, 1f, false).SetEase(Ease.OutExpo);
            Txt_PerfectCnt.text = "Perfect:" + scoreCnt[2].ToString().PadLeft(4, ' ');
            Txt_GoodCnt.text = "Good:" + scoreCnt[1].ToString().PadLeft(4, ' ');
            Txt_MissCnt.text = "Miss:" + scoreCnt[0].ToString().PadLeft(4, ' ');
        }

        //判定数のテキストをフェードインさせる
        {
            Txt_PerfectCnt.transform.DOLocalMoveX(finalScorePos, 1f).SetEase(Ease.InOutQuart).SetDelay(1f);
            Txt_PerfectCnt.DOFade(1f, 1f).SetEase(Ease.InOutQuart).SetDelay(1f);
            Txt_GoodCnt.transform.DOLocalMoveX(finalScorePos, 1f).SetEase(Ease.InOutQuart).SetDelay(1.5f);
            Txt_GoodCnt.DOFade(1f, 1f).SetEase(Ease.InOutQuart).SetDelay(1.5f);
            Txt_MissCnt.transform.DOLocalMoveX(finalScorePos, 1f).SetEase(Ease.InOutQuart).SetDelay(2f);
            Txt_MissCnt.DOFade(1f, 1f).SetEase(Ease.InOutQuart).SetDelay(2f);
        }

        //FC/APの画像変更
        //メソッドは完成、仮画像を使用-2025/11/11
        {
            Texture2D newSprite;
            if (scoreCnt[0] == 0)
            {
                if (scoreCnt[1] == 0) newSprite = Resources.Load("Image/Result/AP_kari") as Texture2D;
                else newSprite = Resources.Load("Image/Result/FC_kari") as Texture2D;
            }
            else if ((float)(score / (scoreCnt[0] + scoreCnt[1] + scoreCnt[2]) * 2) >= 0.7)
            {
                newSprite = Resources.Load("Image/Result/Clear_kari") as Texture2D;
            }
            else newSprite = Resources.Load("Image/Result/Failed_kari") as Texture2D;
            RawImage rawI = Img_ClearLamp.GetComponent<RawImage>();
            rawI.texture = newSprite;
            Img_ClearLamp.transform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.InQuad).SetDelay(2.5f);
            rawI.DOFade(1f, 0.5f).SetEase(Ease.InQuad).SetDelay(2.5f);
            
        }

        //楽曲情報の取得
        {
            if (DebugMode)
            {
                string jacket = "Image/MusicJacket/MusicJacket_kari";
                Texture2D tex = Resources.Load<Texture2D>(jacket);
                Img_MusicJacket.texture = tex;
                Txt_MusicName.text = "デバッグモード";
            }
            else
            {
                var md = SingletonDataManager.instance.MusicData;
                if (md != null)
                {
                    string jacket = "Image/MusicJacket/" + md.jacketPath;
                    Texture2D newTexture = Resources.Load(jacket) as Texture2D;
                    Img_MusicJacket.texture = newTexture;
                    Txt_MusicName.text = md.name;
                }
                else
                {
                    string jacket = "Image/MusicJacket/MusicJacket_kari";
                    Texture2D newTexture = Resources.Load(jacket) as Texture2D;
                    Img_MusicJacket.texture = newTexture;
                    Txt_MusicName.text = "TBD";
                }
            }
        }

        //ゲージを表示
        {
            if (DebugMode)
            {
                bluePer = 0.5f;
            }
            else
            {
                //対戦後のパーセントを入力
            }
            Img_Bars.transform.DOScale(1f, 0.5f).SetEase(Ease.OutQuad).SetDelay(3.5f);
            Img_BarBlue.DOFillAmount(bluePer, 0.7f).SetEase(Ease.InCubic).SetDelay(4f);
            Img_BarRed.DOFillAmount(1f - bluePer, 0.7f).SetEase(Ease.InCubic).SetDelay(4f);
        }

        //ここで勝敗を表示
        {
            if (Img_WinLose == null) return;
            Texture2D tex;
            if (bluePer >= 0.5f)
            {
                tex = Resources.Load<Texture2D>("Image/Result/Win_kari");
            }
            else
            {
                tex = Resources.Load<Texture2D>("Image/Result/Lose_kari");
            }
            if (tex == null) tex = Resources.Load<Texture2D>("Clear_kari");

            Img_WinLose.texture = tex;
            Img_WinLose.transform.DOScaleX(2f, 0.5f).SetDelay(5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Ooo_Title");
        }
    }
}
