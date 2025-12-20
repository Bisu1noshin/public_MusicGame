using LoadForAsync;
using UnityEngine;

public class TestSceneManager : MonoBehaviour
{
    [SerializeField] private AssetLoadConfig loadConfig;
    private string NextSceneName => "NotesTest";

    private async void Start()
    {
        await DataTransferSystem.LoadSceneRef(loadConfig,NextSceneName);
    }
}
