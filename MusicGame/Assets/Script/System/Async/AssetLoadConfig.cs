using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LoadForAsync
{
    /// <summary>
    /// ロードさせるオブジェクトのファイル名型を登録
    /// </summary>
    [CreateAssetMenu(fileName = "AssetLoadConfig", menuName = "Scriptable Objects/AssetLoadConfig")]
    public class AssetLoadConfig : ScriptableObject
    {
        /// <summary>
        /// ロードさせるオブジェクトのList
        /// </summary>
        public List<LoadObject> ReferencesAssets;
    }

    [System.Serializable]
    public class LoadObject
    {
        [Tooltip("オブジェクトのパッシュ")]
        public string ObjectPath;

        [Tooltip("読み込むオブジェクト")]
        public AssetReference AssetReference;
    }
}
