using System;
using System.Collections.Generic;
using UnityEngine;

namespace Notes
{
    /// <summary>
    /// スコアを表すための列挙型
    /// </summary>
    public enum NotesScore
    {
        None = -1,
        Good = 1,
        Perfect = 2,
        Miss = 0
    }

    public sealed class NotesScoreData
    {
        private NotesScore[] score;

        public NotesScoreData() {

            score = new NotesScore[1];
            Initialize();
        }

        public NotesScoreData(int lenge) {

            score = new NotesScore[lenge];
            Initialize();
        }

        public void SetScore(NotesScore s_,int index = 0) {

            score[index] = s_;
            Debug.Log(s_.ToString());
        }

        public int GetTotalScore() {

            int totalScore = 0;

            foreach (var f in score) {

                totalScore += (int)f;
            }

            return totalScore;
        }

        private void Initialize()
        {
            for(int i = 0; i < score.Length; i++)
            {
                score[i] = NotesScore.None;
            }
        }
    }
}
