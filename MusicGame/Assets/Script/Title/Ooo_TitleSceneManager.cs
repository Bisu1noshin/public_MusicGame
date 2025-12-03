using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ooo_TitleSceneManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI companyText;
    void Start()
    {
        companyText.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(companyText.alpha == 1f)
        {
            SceneManager.LoadScene("Ooo_Title");
        }
    }
}
