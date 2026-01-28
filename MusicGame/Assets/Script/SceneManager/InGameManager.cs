using GameInfo;
using LoadForAsync;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SceneControllore
{
    public class InGameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _combo;
        [SerializeField] private Image _score;

        private void Awake()
        {
            Application.targetFrameRate = 240;
        }

        private void Update()
        {
            // UIの表示
            {
                var score = SingletonDataManager.instance.TotalScore;

                // コンボの表示
                _combo.text = score.ToString();

                // スコアゲージの更新
                _score.fillAmount = 1;
            }
        }
    }
}

