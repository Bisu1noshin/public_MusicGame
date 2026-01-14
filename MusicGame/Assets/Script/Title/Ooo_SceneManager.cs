using LoadForAsync;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ooo_SceneManager : MonoBehaviour
{
    [SerializeField] AssetLoadConfig mAssetResources;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        // 適当なボタンで
        if (Input.anyKeyDown)
        {
            // シーン遷移
            //SceneChange("Test_ModeSelectScene");
            LoadSceneRef();
        }
    }

    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log(sceneName + "へ移動");
    }

    private async void LoadSceneRef(/*string musicPath_, string notesPath_*/)
    {
        // 選択された楽曲とノーツのファイルを設定
        //foreach (var obj in mAssetResources.ReferencesAssets)
        //{
        //    if (obj.ObjectPath == "Music")
        //    {
        //        // リストの中から musicPath_ と一致するものを探す
        //        var found = musicList.Find(x => x.key == musicPath_);
        //        if (found != null) obj.AssetReference = found.assetRef;
        //    }

        //    if (obj.ObjectPath == "TextAsset")
        //    {
        //        var found = textList.Find(x => x.key == notesPath_);
        //        if (found != null) obj.AssetReference = found.assetRef;
        //    }
        //}

        string naxtSceneName = "Test_ModeSelectScene";
        await DataTransferSystem.LoadSceneRef(mAssetResources, naxtSceneName);
    }
}
