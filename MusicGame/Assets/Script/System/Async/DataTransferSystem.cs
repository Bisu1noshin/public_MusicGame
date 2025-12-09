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
    }

    public class DataTransferSystem
    {
        /// <summary>
        /// リソース読み込みシーンに遷移するシーンマネージャー
        /// </summary>
        /// <returns></returns>
        public static async UniTask LoadSceneRef()
        {
            await UniTask.Yield();
        }

        /// <summary>
        /// 任意のゲームシーンに遷移するシーンマネージャー
        /// </summary>
        /// <returns></returns>
        public static async UniTask<T> LoadAndFindSceneInformation<T>(string sceneName)
            where T : SceneInfromation
        {
            await SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Single).ToUniTask();
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
