using Cysharp.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadForAsync
{
    public interface SceneInfromation
    {
        public string NewSceneName { get; }

        /// <summary>
        /// オブジェクトをロードする関数
        /// </summary>
        //public async UniTask LoadAsyncObjects();    
}

    public class DataTransferSystem
    {
        /// <summary>
        /// リソース読み込みシーンに遷移するシーンマネージャー
        /// </summary>
        /// <returns></returns>
        public static async UniTask<T> LoadSceneRef<T>()
            where T : DataTransferSystem
        {
            const string sceneName = "LoadScene";

            var task = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            Scene scene = SceneManager.GetSceneByName(sceneName);

            if (!scene.IsValid() || !scene.isLoaded)
            {
                throw new Exception(sceneName + "は存在しないです");
            }

            T presenter = scene.GetRootGameObjects()
                       .Select(go => go.GetComponent<T>())
                       .FirstOrDefault(p => p != null);

            return presenter;
        }

        /// <summary>
        /// 未ロードのリソースが存在するゲームシーンに遷移するシーンマネージャー
        /// </summary>
        /// <returns></returns>
        public static async UniTask<T> LoadAndFindSceneInformation<T>(string sceneName)
            where T : UnityEngine.Object
        {
            var task = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Single);
            Scene scene = SceneManager.GetSceneByName(sceneName);

            if (!scene.IsValid() || !scene.isLoaded)
            {
                throw new Exception(sceneName + "は存在しないです");
            }

            T presenter = scene.GetRootGameObjects()
                       .Select(go => go.GetComponent<T>())
                       .FirstOrDefault(p => p != null);

            return presenter;
        }

    }
}
