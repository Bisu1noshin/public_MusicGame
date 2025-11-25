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
        string musicFilePath = default; //曲情報を管理するファイルのパス

        // コンストラクタ
        public TextEditor(string m_path, string n_path) {
            musicFilePath = m_path;
            NotesRootPath = n_path;
        }

        /// <summary>
        /// テキストからデータを取得変換
        /// </summary>
        /// <param name="data"></param>
        //public void GetData(DataClass data1, DataClass data2)
        //{

        //    // 処理に失敗したらnull
        //    if (data1 == null || data2 == null) return;

        //    TextAsset ta = Resources.Load(filePath) as TextAsset;
        //    string[] strs = ta.text.Split('\n');
        //    int textCnt = 0;
        //    foreach (string str in strs)
        //    {
        //        if (textCnt == 1)
        //        {
        //            int bpm_ = int.Parse(str);
        //        }
        //        if (textCnt > 1)
        //        {
        //            string[] notesInfo = str.Split(",");
        //            int infoCnt = 0;
        //            int time = 0, dirN = 0, typeN = 0, laneN = 0;
        //            foreach (string note in notesInfo)
        //            {
        //                int value = int.Parse(note);
        //                switch (infoCnt)
        //                {
        //                    case 0:
        //                        time = value;
        //                        break;
        //                    case 1:
        //                        dirN = value;
        //                        break;
        //                    case 2:
        //                        typeN = value;
        //                        break;
        //                    case 3:
        //                        laneN = value;
        //                        break;
        //                    default:
        //                        break;
        //                }
        //                infoCnt++;
        //            }
        //            Notes.Notes notes = new(time, dirN, typeN);
        //            if (laneN == 0)
        //            {
        //                data1.AddNotes(notes);
        //            }
        //            else
        //            {
        //                data2.AddNotes(notes);
        //            }
        //        }
        //        textCnt++;
        //    }
        //}

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

            List<string[]> textDatas = new List<string[]>();

            textFile = Resources.Load(NotesRootPath) as TextAsset;

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

            int BPM = (int)Convert.ToSingle(textDatas[1][0]);

            NotesData data = new NotesData(BPM);

            for (int i = 2; i < height; i++)
            {
                int lane = (int)Convert.ToSingle(textDatas[i][3]);

                //[i]は行数。
                int time = (int)Convert.ToSingle(textDatas[i][0]);
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
