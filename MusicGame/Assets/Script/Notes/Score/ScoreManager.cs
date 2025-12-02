using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace Notes
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;
        public List<NotesScore> score;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance) { Destroy(this.gameObject); }

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
    }
}
