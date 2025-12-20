using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace LoadForAsync
{
    public sealed class LoadSceneManager : MonoBehaviour, ILoadSceneManager
    {
        // 読み込み用のクラス
        private LoadObjectTable loadObjectTable = new();

        public AssetLoadConfig AssetConfig { get; set; }

        public string NextSceneName { get; set; }

        private async void Start()
        {
            await loadObjectTable.LoadAllAssetsAsync(AssetConfig);

            await DataTransferSystem.LoadAndSetSceneRef(loadObjectTable, NextSceneName);
        }
    }

    public interface ILoadSceneManager
    {
        public AssetLoadConfig AssetConfig { get; set; }

        public string NextSceneName { get; set; }
    }
}
