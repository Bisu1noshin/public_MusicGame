using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using Notes;

namespace GameInfo
{
    public enum MusicLevel
    {
        None, NORMAL, HARD, EXPERT
    };

    public class SingletonDataManager : MonoBehaviour
    {
        public static SingletonDataManager instance;
        public List<NotesScore> score;

        public int ComboCnt { get; private set; }

        public int TotalScore { get; private set; }

        public MusicData MusicData { get; private set; }

        public MusicLevel Level { get; private set; }

        private void Awake()
        {
            if (instance)
            {
                Destroy(this.gameObject);
                return;
            }

            if (instance == null)
                instance = this;

            DontDestroyOnLoad(this.gameObject);

            // 変数の初期化
            {
                score = new();
                TotalScore = 0;
                ComboCnt = 0;
                Level = MusicLevel.None;
            }
        }

        public void SetScore(NotesScore s_)
        {
            score.Add(s_);
            TotalScore += (int)s_;

            if (s_ == NotesScore.Miss)
            {
                ComboCnt = 0;
                return;
            }

            ComboCnt ++;
        }

        public void SetScore(NotesScore[] s_)
        {
            foreach (NotesScore s in s_)
            {
                // 配列から加算する
                score.Add(s);
                TotalScore += (int)s;

                if (s == NotesScore.Miss)
                {
                    ComboCnt = 0;
                }
            }

            ComboCnt ++;
        }

        public void SetMusicId(MusicData data)
        {
            MusicData = data;
        }

        public void SetMusicLevel(string level)
        {
            switch (level)
            {
                case "NORMAL":
                    Level = MusicLevel.NORMAL;
                    break;
                case "HARD":
                    Level = MusicLevel.HARD;
                    break;
                case "EXPERT":
                    Level = MusicLevel.EXPERT;
                    break;
                default:
                    break;
            }
        }

        public void DestroyInstance()
        {
            if (instance != null)
            {
                Destroy(instance.gameObject);
            }
        }
    }
}
