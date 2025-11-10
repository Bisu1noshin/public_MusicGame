using System;
using UnityEngine;

namespace TextEditor {

    public class TextEditor<DataClass>
        where DataClass : class
    {
        // コンストラクタ
        public TextEditor() {

        }

        // テキストからデータに変換
        public void GetData(DataClass data) {

            // 処理に失敗したらnull
            data = null;
            return; 
        }

    }

}
