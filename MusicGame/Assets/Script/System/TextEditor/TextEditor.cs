using Notes;
using NUnit.Framework;
using System;
using UnityEngine;

namespace TextEditor {

    public class TextEditor<DataClass>
        where DataClass : class, INotesListEditor
    {
        // メンバー変数
        static string NotesRootPath = default; //譜面全体を管理するフォルダのパス
        string filePath = default;

        // コンストラクタ
        public TextEditor(string path) {
            filePath = path;
        }

        /// <summary>
        /// テキストからデータを取得変換
        /// </summary>
        /// <param name="data"></param>
        public void GetData(DataClass data1, DataClass data2) {

            // 処理に失敗したらnull
            if (data1 == null || data2 == null) return;

            TextAsset ta = Resources.Load(filePath) as TextAsset;
            string[] strs = ta.text.Split('\n');
            int textCnt = 0;
            foreach (string str in strs)
            {
                if (textCnt == 1)
                {
                    int bpm_ = int.Parse(str);
                }
                if (textCnt > 1)
                {
                    string[] notesInfo = str.Split(",");
                    int infoCnt = 0;
                    int time = 0, dirN = 0, typeN = 0, laneN = 0;
                    foreach (string note in notesInfo)
                    {
                        int value = int.Parse(note);
                        switch (infoCnt)
                        {
                            case 0:
                                time = value;
                                break;
                            case 1:
                                dirN = value;
                                break;
                            case 2:
                                typeN = value;
                                break;
                            case 3:
                                laneN = value;
                                break;
                            default:
                                break;
                        }
                        infoCnt++;
                    }
                    Notes.Notes notes = new(time, dirN, typeN);
                    if (laneN == 0)
                    {
                        data1.AddNotes(notes);
                    }
                    else
                    {
                        data2.AddNotes(notes);
                    }
                }
                textCnt++;
            }
        }
        public string GetFilePath(string musicName, string level)
        {
            string path = NotesRootPath + "/" + musicName + "/" + level + "_savedNotes.txt";
            return path;
        }
    }

}
