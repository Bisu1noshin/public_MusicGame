using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MusicSelect
{
    public class Player_forMusicSelect : Player.PlayerParent
    {
        ISceneManager mSelecter;
        float time, lastPerformedTime;
        const float LeastContinuePerformeTime = 0.5f; //長押し判定開始時間
        const float IntervalPerfomeTime = 0.1f; //長押し入力の判定同士の間
        Vector2 moveVec = Vector2.zero;

        protected override void Awake()
        {
            mSelecter = GameObject.Find("SceneManager").GetComponent<MusicSelectSceneManager>();
            SetPlayerInput();
            base.Awake();
            name = "Player";
        }

        private void Start()
        {
            
        }
        protected override void SetPlayerInput()
        {

            if (mSelecter.UseKeyboard)
            {
                inputDevice = Notes.InputDevice.KyeBord;
            }
            else
            {
                inputDevice = Notes.InputDevice.Controller;
            }
        }

        private void Update()
        {
            if (moveVec != Vector2.zero)
            {
                time += Time.deltaTime;
                if (time >= LeastContinuePerformeTime && time - lastPerformedTime >= IntervalPerfomeTime)
                {
                    VecAction(moveVec);
                    lastPerformedTime = time;
                }
            }
        }
        protected override void OnButtonA()
        {
            mSelecter.Enter();
        }
        protected override void OnButtonB()
        {
            mSelecter.Undo();
        }
        protected override void LeftStickStarted(Vector2 vec)
        {
            //横入力はキャンセル
            if (Mathf.Abs(vec.y / vec.x) < 1.0f) { return; }
            float Y = vec.y < 0.0f ? -1.0f : 1.0f;
            moveVec = new(0.0f, Y);
            VecAction(moveVec);
            time = 0;
            lastPerformedTime = 0;
        }

        protected override void LeftStickPerformed(Vector2 vec)
        {
            if (Mathf.Abs(vec.y / vec.x) < 1.0f) { return; }
            float Y = vec.y < 0.0f ? -1.0f : 1.0f;
            if (Y == moveVec.y) { return; }
            moveVec = new(0.0f, Y);
            VecAction(moveVec);
            time = 0;
            lastPerformedTime = 0;
        }

        protected override void LeftStickCanceled(Vector2 vec)
        {
            moveVec = Vector2.zero;
        }

        void VecAction(Vector2 vec)
        {
            if (moveVec.y > 0)
            {
                mSelecter.GoBack();
            }
            else
            {
                mSelecter.GoForward();
            }
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
