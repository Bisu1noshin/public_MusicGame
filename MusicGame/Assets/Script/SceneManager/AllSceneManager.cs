using UnityEngine;

public class AllSceneManager : MonoBehaviour
{
    public static AllSceneManager instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
            return;
        }

        if (instance == null)
            instance = this;

        DontDestroyOnLoad(this.gameObject);

        // マウスカーソル非表示
        Cursor.visible = false;
    }

    private void Update()
    {
        // ゲーム終了の処理
        // エディター上はPlay終了
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
