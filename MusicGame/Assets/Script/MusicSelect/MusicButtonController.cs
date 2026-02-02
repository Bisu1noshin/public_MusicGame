using UnityEngine;
using DG.Tweening;
using TMPro;
using System;
using Unity.VisualScripting;
using System.Collections;

namespace MusicSelect
{
    public enum ButtonState
    {
        None = -1,
        Appear,
        Active,
        Dead
    }
    public class MusicButtonController : MonoBehaviour
    {
        [SerializeField] public ButtonState mState { get; private set; }
        IMusicSelecter mSelecter;
        const float buttonPadding = 180;
        TextMeshProUGUI mText;
        RectTransform mRect;
        TextScroller mTextScroller;
        int id;
        Coroutine appear;
        //Coroutine mCurrCoroutine;

        void Awake()
        {
            mText = GetComponentInChildren<TextMeshProUGUI>();
            mRect = GetComponent<RectTransform>();
            mSelecter = GameObject.Find("SceneManager").GetComponent<MusicSelectSceneManager>();
            mTextScroller = GetComponentInChildren<TextScroller>();
            mState = ButtonState.Appear;
            appear = StartCoroutine(AppearMove());
        }
        void Update()
        {
            if (mState == ButtonState.Active)
            {
                Vector2 pos = new(-350.0f, 0.0f);
                pos.y += (mSelecter.SelectNum[0] - id) * 1.3f * buttonPadding;
                mRect.anchoredPosition = pos;
                if (mSelecter.SelectNum[0] == id)
                {
                    transform.localScale = Vector3.one * 1.2f;
                    if (!mTextScroller.enabled)
                    {
                        mTextScroller.enabled = true;
                    }
                }
                else
                {
                    transform.localScale = Vector3.one;
                    if (mTextScroller.enabled)
                    {
                        mTextScroller.enabled = false;
                    }
                }
            }
            if (mState == ButtonState.Dead)
            {
                StopCoroutine(appear);
                StartCoroutine(DestroyMove());
            }
        }
        public void SetInfo(string text_, int id_)
        {
            mText.text = text_;
            id = id_;
        }
        public static Action CreateInstance(GameObject res, string text_, int id_, int currId_)
        {
            if (res == null) res = Resources.Load<GameObject>("MusicSelecter/MusicButton");
            GameObject go = Instantiate(res);
            go.name = text_;
            go.transform.SetParent(GameObject.Find("Canvas").transform.GetChild(1).transform);
            go.transform.SetLocalPositionAndRotation(new(-1360.0f, (currId_ - id_) * 1.3f * buttonPadding, 0.0f), Quaternion.identity);
            go.transform.localScale = Vector3.one;
            MusicButtonController mbc = go.GetComponent<MusicButtonController>();
            mbc.SetInfo(text_, id_);

            Action f = () => { mbc.mState = ButtonState.Dead; };

            return f;
        }
        IEnumerator AppearMove()
        {
            transform.DOLocalMoveX(-350f, 0.4f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.4f);
            if (mState != ButtonState.Dead)
            {
                mState = ButtonState.Active;
            }
            yield break;
        }
        IEnumerator DestroyMove()
        {
            mRect.DOLocalMoveX(-1360f, 0.2f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.2f);

            Destroy(gameObject);
            yield break;
        }
    }
    
}
