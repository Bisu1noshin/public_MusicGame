using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using GameInfo;
using UnityEngine.SceneManagement;

public class Onishi_ResultSceneManager : MonoBehaviour
{
    [SerializeField] private Text Txt_Score;                        //レガシーテキスト版スコア
    [SerializeField] private TMP_Text Txt_PerfectCnt;               //パーフェクト数
    [SerializeField] private TMP_Text Txt_GoodCnt;                  //グッド数
    [SerializeField] private TMP_Text Txt_MissCnt;                  //ミス数
    [SerializeField] private GameObject Img_ClearLamp;              //クリアランプ
    [SerializeField] private RawImage Img_MusicJacket;              //楽曲のジャケット
    [SerializeField] private TMP_Text Txt_MusicName;                //楽曲名
    bool isRainbow = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //スコア、判定数の取得
        int score = 0;
        int[] scoreCnt = new int[3];

        foreach(var sc in SingletonDataManager.instance.score){
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
            Txt_PerfectCnt.transform.DOLocalMoveX(-380f, 1f).SetEase(Ease.InOutQuart).SetDelay(1f).OnComplete(() => isRainbow = true);
            Txt_PerfectCnt.DOFade(1f, 1f).SetEase(Ease.InOutQuart).SetDelay(1f);
            Txt_GoodCnt.transform.DOLocalMoveX(-380f, 1f).SetEase(Ease.InOutQuart).SetDelay(1.5f);
            Txt_GoodCnt.DOFade(1f, 1f).SetEase(Ease.InOutQuart).SetDelay(1.5f);
            Txt_MissCnt.transform.DOLocalMoveX(-380f, 1f).SetEase(Ease.InOutQuart).SetDelay(2f);
            Txt_MissCnt.DOFade(1f, 1f).SetEase(Ease.InOutQuart).SetDelay(2f);
        }

        //FC/APの画像変更
        //メソッドは完成、仮画像を使用-2025/11/11
        {
            Texture2D newSprite;
            if (scoreCnt[0] == 0)
            {
                if (scoreCnt[1] == 0) newSprite = Resources.Load("Image/Result/AP") as Texture2D;
                else newSprite = Resources.Load("Image/Result/FC") as Texture2D;
            }
            else if ((float)(score / (scoreCnt[0] + scoreCnt[1] + scoreCnt[2]) * 2) >= 0.7)
            {
                newSprite = Resources.Load("Image/Result/Clear") as Texture2D;
            }
            else newSprite = Resources.Load("Image/Result/Failed") as Texture2D;
            Img_ClearLamp.GetComponent<RawImage>().texture = newSprite;
        }

        //楽曲情報の取得
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

    private void Update()
    {
        if(isRainbow) GamingColor(Txt_PerfectCnt);

        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Ooo_Title");
        }
    }

    private void OnDestroy()
    {
        SingletonDataManager.instance.DestroyInstance();
    }

    private void GamingColor(MaskableGraphic ui)
    {
        float addValue = 1f / 256f * 16f;
        float maxValue = 1f;

        float r = ui.color.r;
        float g = ui.color.g;
        float b = ui.color.b;

        if (r == maxValue && g == 0)
        {
            b += addValue;
        }

        if (g == 0 && b == maxValue)
        {
            r -= addValue;
        }

        if (r == 0 && b == maxValue)
        {
            g += addValue;
        }

        if (r == 0 && g == maxValue)
        {
            b -= addValue;
        }

        if (b == 0 && g == maxValue)
        {
            r += addValue;
        }

        if (b == 0 && r == maxValue)
        {
            g -= addValue;
        }

        ui.color = new Color(r, g, b);
    }
}
