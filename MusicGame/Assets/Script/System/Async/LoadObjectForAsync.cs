using UnityEngine;
using Cysharp.Threading.Tasks;

namespace LoadForAsync
{
    public class LoadObjectForAsync<TClass> : MonoBehaviour, SceneInfromation<TClass>
        where TClass : UnityEngine.Object
    {
        public string NewSceneName => "LoadObjectForAsyncScene";

        public LoadObjectTable<TClass> ObjectTable => new();
        private LoadObject<TextAsset> TextAsset;

        private void Start()
        {
            // オブジェクトテーブルの初期化
            ObjectTable.Initilize();

            // アセットの追加
            ObjectTable.AddGameObject(TextAsset);
        }

        private void Update()
        {
            
        }
    }
}

