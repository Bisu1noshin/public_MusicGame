using LoadForAsync;
using UnityEngine;
using UnityEngine.Audio;

namespace SceneControllore
{
    public class InGameManager : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        } 
    }
}

