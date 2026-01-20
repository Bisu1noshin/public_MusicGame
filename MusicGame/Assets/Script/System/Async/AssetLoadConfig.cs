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
        public List<AssetReferenceObject> ReferencesAssets;

        public int MusicBPM;
    }

    [System.Serializable]
    public class AssetReferenceObject
    {
        public AssetReferenceObject(string path, AssetReference reference)
        {
            this.ObjectPath = path;
            this.AssetReference = reference;
        }

        [Tooltip("オブジェクトのパッシュ")]
        public string ObjectPath;

        [Tooltip("読み込むオブジェクト")]
        public AssetReference AssetReference;
    }
}
