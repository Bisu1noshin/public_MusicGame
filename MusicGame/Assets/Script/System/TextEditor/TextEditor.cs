using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Notes;

namespace TextEditor {

    public class TextEditor
    {
        // メンバー変数
        private string NotesRootPath = default; //譜面全体を管理するフォルダのパス
        private string musicFilePath = default; //曲情報を管理するファイルのパス

        private TextAsset TextAsset;

        // コンストラクタ
        public TextEditor(string m_path, string n_path) {
            musicFilePath = m_path;
            NotesRootPath = n_path;
            TextAsset = null;
        }

        public TextEditor(string m_path, TextAsset textAsset)
        {
            musicFilePath = m_path;
            TextAsset = textAsset;
        }

        public string GetFilePath(string musicName, string level)
        {
            string path = NotesRootPath + "/" + musicName + "/" + level + "_savedNotes.txt";
            return path;
        }

        /// <summary>
        /// テキストからノーツデータを取得変換
        /// </summary>
        public NotesData NotesReadTxt() {

            TextAsset textFile;

            if (TextAsset != null) { textFile = TextAsset; }
            else
            {
                textFile = Resources.Load(NotesRootPath) as TextAsset;
            }

            //Dataは複数形ないよ
            List<string[]> textDatas = new List<string[]>();

            int height = 0;

            //読み込んだテキストをString型にして格納
            StringReader reader = new StringReader(textFile.text);

            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();
                // ,で区切ってCSVに格納
                textDatas.Add(line.Split(','));
                height++; // 行数加算
            }

            float BPM = (float)Convert.ToSingle(textDatas[1][0]);

            NotesData data = new NotesData(BPM);

            for (int i = 2; i < height; i++)
            {
                int lane = (int)Convert.ToSingle(textDatas[i][3]);

                //[i]は行数。
                float time = (float)Convert.ToSingle(textDatas[i][0]);
                int dirN = (int)Convert.ToSingle(textDatas[i][1]);
                int kind = (int)Convert.ToSingle(textDatas[i][2]);

                Notes.Notes n_ = new Notes.Notes(time, dirN, kind);

                //戻り値のリストに加える
                data.notes[lane].Add(n_);
            }

            return data;
        }
    }

}
