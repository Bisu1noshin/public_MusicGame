using Cysharp.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadForAsync
{
    public interface ISetAsyncObjects
    {
        // オブジェクトをロードする関数
        public void SetAsyncObjects(LoadObjectTable ObjectTable);

        public Action ReleaseAll { get; set; }
}

    public class DataTransferSystem
    {
        /// <summary>
        /// リソース読み込みシーンに遷移するシーンマネージャー
        /// </summary>
        /// <returns></returns>
        public static async UniTask LoadSceneRef(AssetLoadConfig loadConfig,string NextSceneName)
        {
            const string sceneName = "LoadScene";

            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).ToUniTask();
            Scene scene = SceneManager.GetSceneByName(sceneName);

            if (!scene.IsValid() || !scene.isLoaded)
            {
                throw new Exception(sceneName + "は存在しないです");
            }

            ILoadSceneManager presenter = scene.GetRootGameObjects()
                       .Select(go => go.GetComponent<ILoadSceneManager>())
                       .FirstOrDefault(p => p != null);

            presenter.AssetConfig = loadConfig;
            presenter.NextSceneName = NextSceneName;
        }

        /// <summary>
        /// 未ロードのリソースが存在するゲームシーンに遷移するシーンマネージャー
        /// </summary>
        /// <returns></returns>
        public static async UniTask LoadAndSetSceneRef(LoadObjectTable ObjectTable, string NextSceneName)
        {
            await SceneManager.LoadSceneAsync(NextSceneName, LoadSceneMode.Single).ToUniTask();
            Scene scene = SceneManager.GetSceneByName(NextSceneName);

            if (!scene.IsValid() || !scene.isLoaded)
            {
                throw new Exception(NextSceneName + "は存在しないです");
            }

            var presenters = scene.GetRootGameObjects()
                            .SelectMany(go => go.GetComponentsInChildren<ISetAsyncObjects>()) 
                            .ToList();

            if (presenters.Any())
            {
                foreach (var p in presenters)
                {
                    p.SetAsyncObjects(ObjectTable);
                    p.ReleaseAll = () => { ObjectTable.ReleaseAll(); };
                }
            }
        }

    }
}
