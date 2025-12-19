using UnityEngine;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

namespace LoadForAsync
{
    public class LoadObjectForAsync : MonoBehaviour
    {
        [SerializeField] private AssetLoadConfig loadConfig;
        [SerializeField] private string ObjectKye;
        private LoadObjectTable loadObjectTable = new();

        public string NewSceneName => "LoadObjectForAsyncScene";

        private async void Start()
        {
            await loadObjectTable.LoadAllAssetsAsync(loadConfig);

            var Prefab = loadObjectTable.GetAsset<GameObject>(ObjectKye);

            if (Prefab != null) Debug.Log("Ok.Prefab");
            else Debug.Log("Non.Prefab");
        }

        private void Update()
        {
            
        }
    }
}

