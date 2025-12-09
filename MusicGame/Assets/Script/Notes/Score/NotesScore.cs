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
        public NotesScore[] score { get; private set; }

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
        }

        public int GetTotalScoreToInt() {

            int totalScore = 0;

            foreach (var f in score) {

                totalScore += (int)f;
            }

            return totalScore;
        }

        public NotesScore[] GetTotalScore()
        {
            return score;
        }

        public int[] GetTotalScoreData()
        {
            int[] ScoreData = new int[3]; //0Perfect 1Good 2Miss
            foreach (var f in score)
            {
                switch (f)
                {
                    case NotesScore.Perfect:
                        ScoreData[0]++;
                        break;
                    case NotesScore.Good:
                        ScoreData[1]++;
                        break;
                    case NotesScore.Miss:
                        ScoreData[2]++;
                        break;
                    default:
                        break;
                }
            }
            return ScoreData;
        }

        public void DebugLogScore()
        {
            string s = "Log :";

            for (int i = 0; i < score.Length; i++)
            {
                int index = i + 1;
                s += ("\nscore"+index + ": " + score[i].ToString());
            }

            Debug.Log(s);
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
