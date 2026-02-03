using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;
using System.Collections;

namespace MusicSelect
{
    public class PropertyController : MonoBehaviour
    {
        //IMusicSelecter mSelecter;
        TextMeshProUGUI mText;
        AudioSource mAudio;
        [SerializeField] Image mThumbnail;
        ButtonState mState;
        RectTransform mRect;
        Coroutine appear;

        private void Awake()
        {
            mText = transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
            mAudio = GetComponent<AudioSource>();
            mThumbnail = transform.GetChild(0).GetComponent<Image>();
            mRect = GetComponent<RectTransform>();
            mState = ButtonState.Appear;
            appear = StartCoroutine(AppearMove());
        }
        private void Update()
        {
            if (mState == ButtonState.Dead)
            {
                StopCoroutine(appear);
                StartCoroutine(DestroyMove());
            }
        }

        public void SetProperty(string text, AudioClip audio, Sprite image)
        {
            if (mState == ButtonState.Dead) return;
            mText.text = text;
            if (audio != null)
            {
                mAudio.clip = audio;
                mAudio.Play();
            }
            if (image != null)
            {
                mThumbnail.sprite = image;
            }
        }

        public static (PropertyController, Action) CreateInstance(GameObject res)
        {
            if (res == null) res = Resources.Load<GameObject>("MusicSelecter/Property");
            GameObject go = Instantiate(res);
            go.name = "Property";
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            PropertyController pc = go.GetComponent<PropertyController>();
            pc.mRect.anchoredPosition = new(550.0f, -1000.0f);

            Action f = () => { pc.mState = ButtonState.Dead; };
            return (pc, f);
        }
        IEnumerator AppearMove()
        {
            transform.DOLocalMoveY(0f, 0.3f).SetEase(Ease.OutCubic);
            yield return new WaitForSeconds(0.3f);
            if (mState != ButtonState.Dead)
            {
                mState = ButtonState.Active;
            }
            yield break;
        }
        IEnumerator DestroyMove()
        {
            transform.DOLocalMoveY(-1000f, 0.1f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
            yield break;
        }
    }

}
