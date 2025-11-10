using System;
using UnityEngine;

namespace TextEditor {

    public class TextEditor<DataClass>
        where DataClass : class
    {
        // メンバー変数

        string filePath = default;

        // コンストラクタ
        public TextEditor() {

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

    }

}
