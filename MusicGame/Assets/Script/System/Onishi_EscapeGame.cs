using UnityEngine;

public class Onishi_EscapeGame : MonoBehaviour
{
    public static Onishi_EscapeGame instance;

    private void Awake()
    {
        if (instance) { Destroy(this.gameObject); }

        if (instance == null)
            instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Escでゲームを抜ける
        if (Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
