using UnityEngine;

namespace OnLine
{
    /// <summary>
    /// StartOnLineSceneのステータス
    /// </summary>
    public enum StartSceneState
    {
        None = 0,
        Decide,
        Host,
        Client,
        Wait
    }

    /// <summary>
    /// StartOnLineSceneのトリガー
    /// </summary>
    public enum StartSceneTrigger
    {
        None = 0,
        Decide,
        Host,
        Client,
        Wait
    }

    public class StartOnLineScene : MonoBehaviour
    {
        private StateMachine<StartSceneState, StartSceneTrigger> st;

        private void Start()
        {
            //st = new();
        }
    }
}

