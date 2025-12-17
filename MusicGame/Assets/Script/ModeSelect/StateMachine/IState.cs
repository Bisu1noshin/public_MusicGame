using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public abstract class IState : StateBase<ModeSelectSceneManager, Trigger>, IActionDictionary, IModeSelecter
    {
        protected ISceneManager mOwner;
        public List<Action> Actions { get; set; }
        public Dictionary<int, Action> ActionDic { get; set; }
        protected Dictionary<int, string> ButtonNames { get; set; }
        public int[] SelectNum { get; protected set; }
        protected int layer;
        protected Action deleteAction;
        protected GameObject buttonPre;
        public IState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            mOwner = owner;
            Actions = new();
            SelectNum = new int[6];
            ActionDic = new();
            ButtonNames = new();
            layer = 0;
            deleteAction = null;
            buttonPre = Resources.Load<GameObject>("ModeSelect/ModeButton");
        }
        protected override void OnUpdate(float deltaTime)
        {
            ReplaceEnterAction(Actions[SelectNum[layer]]);
        }
        protected void PlayEnterSound()
        {
            mOwner.mAudio.PlayOneShot(mOwner.mAudioClips[0]);
        }
        protected void PlayCancelSound()
        {
            mOwner.mAudio.PlayOneShot(mOwner.mAudioClips[1]);
        }
        protected void PlayShiftSound()
        {
            mOwner.mAudio.PlayOneShot(mOwner.mAudioClips[2]);
        }
        protected void PlayBeepSound()
        {
            mOwner.mAudio.PlayOneShot(mOwner.mAudioClips[3]);
        }
        protected void Scroll(Vector2 vector2)
        {
            SelectNum[layer] += vector2.y < 0.0f ? 1 : -1;
            if (SelectNum[layer] < 0)
            {
                SelectNum[layer] = 0;
                PlayBeepSound();
            }
            else if (SelectNum[layer] > Actions.Count - 1)
            {
                SelectNum[layer] = Actions.Count - 1;
                PlayBeepSound();
            }
            else
            {
                PlayShiftSound();
            }
        }
        protected void SetButtonAction(int id_, string name_,  Action action_)
        {
            if (ActionDic.ContainsKey(id_)) { Debug.Log($"Error! : id({id_}) is already exist");  return; }
            ActionDic.Add(id_, action_);
            ButtonNames.Add(id_, name_);
        }
        protected void ReplaceEnterAction(Action action_)
        {
            ModeSelect.Player.enterAction = deleteAction;
            ModeSelect.Player.enterAction += action_;
        }
        protected void ReserveNullActionList(int cap)
        {
            for (int i = 0; i < cap; ++i)
            {
                Actions.Add(null);
            }
        }
        protected void CreatePopup(string msg)
        {
            (GameObject, Action) toggle = PopupController.CreateInstance(msg);
            GameObject go = toggle.Item1;
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            go.transform.localScale = Vector3.one;
            go.GetComponentInChildren<TextMeshProUGUI>().text = msg;
            deleteAction += () => { toggle.Item2.Invoke(); };
        }
    }
}
