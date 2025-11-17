using System;
using UnityEngine;

namespace TextEditor {

    public class TextEditor<DataClass>
        where DataClass : class
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
        public void GetData(DataClass data) {

            // 処理に失敗したらnull
            data = null;
            return; 
        }
        public string GetFilePath(string musicName, string level)
        {
            string path = NotesRootPath + "/" + musicName + "/" + level + "_savedNotes.txt";
            return path;
        }
    }

}
