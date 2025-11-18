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
        string NotesRootPath = default; //譜面全体を管理するフォルダのパス
        string musicFilePath = default;

        // コンストラクタ
        public TextEditor(string m_path, string n_path) {
            musicFilePath = m_path;
            NotesRootPath = n_path;
        }

        /// <summary>
        /// テキストからデータを取得変換
        /// </summary>
        /// <param name="data"></param>
        public void GetData() {

            // 処理に失敗したらnull
            //if (data == null) return;


        }
        public string GetFilePath(string musicName, string level)
        {
            string path = NotesRootPath + "/" + musicName + "/" + level + "_savedNotes.txt";
            return path;
        }

        /// <summary>
        /// テキストからノーツデータを取得変換
        /// </summary>
        public NotesData NotesReadTxt(NotesLane lane) {

            TextAsset csvFile;

            List<string[]> csvDatas = new List<string[]>();

            csvFile = Resources.Load(NotesRootPath) as TextAsset;

            int height = 0;

            //読み込んだテキストをString型にして格納
            StringReader reader = new StringReader(csvFile.text);

            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();
                // ,で区切ってCSVに格納
                csvDatas.Add(line.Split(','));
                height++; // 行数加算
            }

            int BPM = (int)Convert.ToSingle(csvDatas[1][0]);

            NotesData data = new NotesData(BPM);

            for (int i = 2; i < height; i++)
            {
                if((int) Convert.ToSingle(csvDatas[i][3]) != (int)lane){ continue; }

                //[i]は行数。
                int time = (int)Convert.ToSingle(csvDatas[i][0]);
                int dirN = (int)Convert.ToSingle(csvDatas[i][1]);
                int kind = (int)Convert.ToSingle(csvDatas[i][2]);

                Notes.Notes n_ = new Notes.Notes(time, dirN, kind);

                //戻り値のリストに加える
                data.notes.Add(n_);
            }

            return data;
        }
    }

}
