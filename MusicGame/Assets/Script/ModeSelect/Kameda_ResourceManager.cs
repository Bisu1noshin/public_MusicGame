using LoadForAsync;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Kameda_ResourceManager : MonoBehaviour, IResourceManager
{
    [SerializeField] AssetLoadConfig mAssetConfig;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public AssetReference GetObject(string path)
    {
        if (path == null)
        {
            Debug.LogError("Error! Path is NULL");
            return null;
        }
        var asset = mAssetConfig.ReferencesAssets.Find(x => x.ObjectPath == path);
        if (asset == null)
        {
            Debug.LogError($"Error! Path[{path}] is NOT exist");
        }
        return asset.AssetReference;
    }
}

public interface IResourceManager
{
     AssetReference GetObject(string path);
}