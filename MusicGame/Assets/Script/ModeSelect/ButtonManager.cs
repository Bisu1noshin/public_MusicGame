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
            int id;
            void Awake()
            {
                mText = GetComponentInChildren<TextMeshProUGUI>();
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
            public void SetInfo(IModeSelecter owner, int id_, string text_)
            {
                mSelecter = owner;
                id = id_;
                mText.text = text_;
            }
            public float ReturnY(int me, int max)
            {
                return -(me - (max - 1) / 2.0f) * (540 / max * 2);
            }
            public static Action CreateInstance(IModeSelecter owner, int id_, int maxId_, string text_, bool destroyAnim = false)
            {
                GameObject res = Resources.Load<GameObject>("ModeSelect/ModeButton");
                GameObject go = Instantiate(res);
                go.name = text_;
                go.transform.SetParent(GameObject.Find("Canvas").transform.GetChild(1).transform);
                go.transform.localScale = Vector3.one;
                
                ButtonManager bm = go.GetComponent<ButtonManager>();
                go.transform.SetLocalPositionAndRotation(new Vector2(-350.0f, bm.ReturnY(id_, maxId_)), Quaternion.identity);
                bm.SetInfo(owner, id_, text_);
                Action f = () =>  bm.DeleteButton(destroyAnim);
                return f;
            }
            private void OnDisable()
            {
                if (mSelecter.deleteAction != null) mSelecter.deleteAction = null;
                if (Player.enterAction != null) Player.enterAction = null;
                if (Player.backAction != null) Player.backAction = null;
            }
        }
        public interface IButtonController
        {
            void DeleteButton(bool animation = false);
        }
    }
    
}
