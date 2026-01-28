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
    public class SingletonDataManager : MonoBehaviour
    {
        public static SingletonDataManager instance;
        public List<NotesScore> score;
        public int TotalScore { get; private set; }
        public MusicData MusicData { get; private set; }

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
            score = new();
            TotalScore = 0;
        }

        public void SetScore(NotesScore[] s_)
        {
            foreach (NotesScore s in s_)
            {
                // 配列から加算する
                score.Add(s);
                TotalScore += (int)s;
            }
        }

        public void SetMusicId(MusicData data)
        {
            MusicData = data;
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
