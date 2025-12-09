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
        public int musicId { get; private set; }

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
        }

        public void SetScore(NotesScore[] s_)
        {
            foreach (NotesScore s in s_)
            {
                // 配列から加算する
                score.Add(s);
            }
        }

        public void SetMusicId(int num)
        {
            musicId = num;
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
