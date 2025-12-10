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
            IModeSelecter mSelecter;
            State mState;
            TextMeshProUGUI mText;
            RectTransform mRect;
            int id;
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
                id = id_;
                mText.text = text_;
                if (mSelecter.Actions.Count < id)
                {
                    Debug.LogError($"Error! : Actions.Count is small than {id}.");
                    return;
                }
                mSelecter.Actions[id] = action_;
            }
            public static Action CreateInstance(IModeSelecter owner, int id_, int maxId_, string text_, Action action_, bool destroyAnim)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("gameObject_something"));
                go.transform.SetParent(GameObject.Find("Canvas").transform.GetChild(1).transform);
                go.transform.localScale = Vector3.one;
                go.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                {
                    float currNum = (maxId_ / 2);
                }
                ButtonManager bm = go.GetComponent<ButtonManager>();
                bm.mSelecter = owner;
                bm.SetInfo(id_, text_, action_);
                Action f = () => { bm.DeleteButton(destroyAnim); };
                return f;
            }
        }
        public interface IButtonController
        {
            void DeleteButton(bool animation = false);
        }
    }
    
}
