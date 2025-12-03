using UnityEngine;
using UnityEngine.SceneManagement;

public class Ooo_SceneManager : MonoBehaviour
{
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
            SceneChange("Test_MusicSelectScene");
        }
    }

    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log(sceneName + "へ移動");
    }
}
