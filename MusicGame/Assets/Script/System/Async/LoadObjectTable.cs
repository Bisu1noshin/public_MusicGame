using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace LoadForAsync
{
    public class LoadObjectTable
    {
        // オブジェクト型で保持する
        private Dictionary<string, Object> _loadedCache = new Dictionary<string, Object>();

        // メモリ解放用のハンドルを保存
        private List<AsyncOperationHandle> _handles = new List<AsyncOperationHandle>();

        public async UniTask LoadAllAssetsAsync(AssetLoadConfig config)
        {
            var tasks = new List<UniTask<(string key, Object asset)>>();

            foreach (var reference in config.ReferencesAssets)
            {
                tasks.Add(LoadAsObjectAsync(reference));
            }

            // 全て並列でロード?
            var results = await UniTask.WhenAll(tasks);

            foreach (var result in results)
            {
                if (result.asset == null) continue; // 失敗したら無視
                _loadedCache.TryAdd(result.key, result.asset);// オブジェクトの登録
            }
        }

        /// <summary>
        /// 読み込み非同期処理
        /// </summary>
        /// <param name="reference">AssetReference</param>
        /// <returns></returns>
        private async UniTask<(string, Object)> LoadAsObjectAsync(AssetReferenceObject reference)
        {
            var handle = reference.AssetReference.LoadAssetAsync<Object>();
            _handles.Add(handle); // 解放用にハンドルを保持

            var asset = await handle.ToUniTask();
            return (reference.ObjectPath, asset);
        }

        /// <summary>
        /// 型を指定してキャッシュから取り出すメソッド
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reference">指定のオブジェクトの名前</param>
        /// <returns>取得できないときはnull</returns>
        public T GetAsset<T>(string reference) where T : Object
        {
            return _loadedCache.TryGetValue(reference, out var obj) ? obj as T : null;
        }

        /// <summary>
        /// 全メモリ解放
        /// </summary>
        public void ReleaseAll()
        {
            if (_handles.Count == 0) { return; }

            foreach (var handle in _handles)
            {
                Addressables.Release(handle);
            }
            _loadedCache.Clear();
            _handles.Clear();
        }
    }

    public sealed class LoadObject<T>
        where T : UnityEngine.Object
    {
        public string ObjectPath { get; set; }

        public T Asset { get; set; }

        public LoadObject(string path)
        {
            ObjectPath = path;
            Asset = default(T);
        }
    }

}
