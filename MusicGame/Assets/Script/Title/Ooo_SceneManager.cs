using UnityEngine;
using UnityEngine.SceneManagement;

public class Ooo_SceneManager : MonoBehaviour
{
   public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log(sceneName + "へ移動");
    }
}
