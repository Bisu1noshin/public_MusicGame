using DG.Tweening;
using Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

namespace ModeSelect
{
    public class Player : PlayerParent
    {
        public static Action enterAction { get; set; }
        public static Action backAction { get; set; }
        public static Action<Vector2> vecAction { get; set; }

        public static Action destroyAction { get; private set; }

        float time, lastPerformedTime;
        const float LeastContinuePerformeTime = 0.5f; //長押し判定開始時間
        const float IntervalPerfomeTime = 0.1f; //長押し入力の判定同士の間
        Vector2 moveVec = Vector2.zero;
        ISceneManager sceneManager;
        protected override void Awake()
        {
            name = "Player";
            sceneManager = GameObject.Find("SceneManager").GetComponent<ModeSelectSceneManager>();
            base.Awake();
            destroyAction = () => Destroy(this.gameObject);
        }
        private void Update()
        {
            if (moveVec != Vector2.zero)
            {
                time += Time.deltaTime;
                if (time >= LeastContinuePerformeTime && time - lastPerformedTime >= IntervalPerfomeTime)
                {
                    vecAction?.Invoke(moveVec);
                    lastPerformedTime = time;
                }
            }
            
        }

        protected override void SetPlayerInput()
        {
            if (sceneManager._DebugMode)
            {
                inputDevice = Notes.InputDevice.KyeBord;
            }
            else inputDevice = sceneManager.GetPlayerConfig().InputDevice;
        }

        protected override void OnButtonA()
        {
            enterAction?.Invoke();
        }
        protected override void OnButtonB()
        {
            backAction?.Invoke();
        }
        
        protected override void LeftStickStarted(Vector2 vec)
        {
            if (Mathf.Abs(vec.y / vec.x) < 1.0f) return;
            float Y = vec.y < 0.0f ? -1.0f : 1.0f;
            moveVec = new(0.0f, Y);
            vecAction?.Invoke(moveVec);
            time = 0;
            lastPerformedTime = 0;
        }
        protected override void LeftStickPerformed(Vector2 vec)
        {
            if (Mathf.Abs(vec.y / vec.x) < 1.0f) return;
            float Y = vec.y < 0.0f ? -1.0f : 1.0f;
            if (Y == moveVec.y) { return; }
            moveVec = new(0.0f, Y);
            vecAction?.Invoke(moveVec);
            time = 0;
            lastPerformedTime = 0;
        }
        protected override void LeftStickCanceled(Vector2 vec)
        {
            moveVec = Vector2.zero;
        }
        protected override void OnButtonX() { }
        protected override void OnButtonY() { }
        protected override void UpButtonA() { }
        protected override void UpButtonB() { }
        protected override void UpButtonX() { }
        protected override void UpButtonY() { }
        protected override void RightStickStarted(Vector2 vec) { }
        protected override void RightStickPerformed(Vector2 vec) { }
        protected override void RightStickCanceled(Vector2 vec) { }
    }
}
