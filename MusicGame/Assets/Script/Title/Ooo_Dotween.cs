using UnityEngine;
using DG.Tweening;
using TMPro;

public class Ooo_Dotween : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI startText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        startText.transform.DOLocalMoveY(-355f, 1.5f); //1秒かけてY座標を-355へ
    }
}
