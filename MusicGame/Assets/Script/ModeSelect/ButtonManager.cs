using UnityEngine;
using System;
using TMPro;

namespace ModeSelect
{
    namespace Button
    {
        public enum State {
            None = -1,
            Appear,
            Active,
            Dead
        }
        
        public class ButtonManager : MonoBehaviour, IButtonController
        {
            State mState;
            TextMeshProUGUI mText;
            RectTransform mRect;
            void Awake()
            {
                mText = GetComponentInChildren<TextMeshProUGUI>();
                mRect = GetComponent<RectTransform>();
            }
            void Update()
            {
                switch (mState)
                {
                    case State.Appear:
                        break;
                    case State.Active:
                        break;
                    case State.Dead:
                        break;
                    default:
                        break;
                }
            }
            public void DeleteButton(bool animation)
            {
                if (animation)
                {
                    mState = State.Dead;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            public void SetInfo(int id_, string text_, Action action_)
            {

            }
            public static Action CreateInstance(int id_, string text_, Action action_, bool DestroyAnim)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("gameObject_something"));
                ButtonManager bm = go.GetComponent<ButtonManager>();
                bm.SetInfo(id_, text_, action_);
                Action f = () => { bm.DeleteButton(DestroyAnim); };
                return f;
            }
        }
    }
    
}
