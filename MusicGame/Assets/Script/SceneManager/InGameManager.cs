using GameInfo;
using LoadForAsync;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
using Notes;
using System.Collections.Generic;

namespace SceneControllore
{
    public class InGameManager : MonoBehaviour, ISetAsyncObjects
    {
        [SerializeField] private TextMeshProUGUI _comboText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Image _scoreImage;
        [SerializeField] private Image _jaketImage;
        [SerializeField] private TextMeshProUGUI _jakcetText;

        public Action ReleaseAll { get; set; }

        private int maxTotalScore;

        private void Awake()
        {
            Application.targetFrameRate = 240;
            maxTotalScore = 1;
        }

        private void Update()
        {
            // UIの表示
            {
                var score = SingletonDataManager.instance?.TotalScore;
                var combo = SingletonDataManager.instance?.ComboCnt;

                // コンボの表示
                _comboText.text = combo.ToString();

                // コンボの表示
                _scoreText.text = score.ToString();

                // スコアゲージの更新
                float fillscore = (float)(score) / (float)(maxTotalScore);
                _scoreImage.fillAmount = fillscore;
            }
        }

        public void SetAsyncObjects(LoadObjectTable ObjectTable)
        {
            // 生成用にデータを編集
            List<NotesData> notesDatas = new List<NotesData>();
            int index = 1;

            var textAsset = new List<TextAsset>();
            while (true)
            {
                var asset = ObjectTable.GetAsset<TextAsset>("TextAsset_" + index.ToString());
                if (asset == null) break;

                textAsset.Add(asset);
                index++;
            }

            foreach (var path in textAsset)
            {
                TextEditor.TextEditor textEditor = new(null, path);
                NotesData data = textEditor.NotesReadTxt();
                data = NotesDataConversion.NotesDataReSize(data);
                notesDatas.Add(data);
            }

            var notesData = NotesDataConversion.NotesDataSum(notesDatas);

            // 正規のBPMをセットする
            var BPM = SingletonDataManager.instance.MusicData.BPM;

            maxTotalScore = NotesDataConversion.TotalNotesScore(notesData, BPM);

            // ジャケットの変更
            _jaketImage.sprite = ObjectTable.GetAsset<Sprite>("MusicJakcet");
            _jakcetText.text = SingletonDataManager.instance.Level.ToString();
        }
    }
}

