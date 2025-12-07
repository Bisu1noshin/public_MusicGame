using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public class Player_forMusicSelect : PlayerParent
    {
        IMusicSelecter mSelecter;
        float LStick_onTime;

        Vector2 LStick_prev;

        private void Start()
        {
            mSelecter = GameObject.Find("SceneManager").GetComponent<MusicSelectSceneManager>();
            LStick_onTime = 0;
        }

        private void Update()
        {
            if(LStick_onTime > 0.5f)
            {
                if (LStick_prev.y > 0)
                {
                    mSelecter.GoBack();
                }
                else
                {
                    mSelecter.GoForward();
                }
                LStick_onTime = 0.0f;
            }
            if(LStick_prev != Vector2.zero)
            {
                LStick_onTime += Time.deltaTime;
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
        protected override void OnButtonX() { }
        protected override void OnButtonY() { }
        protected override void UpButtonA()
        {
            mSelecter.CanSelect = true;
        }
        protected override void UpButtonB() { }
        protected override void UpButtonX() { }
        protected override void UpButtonY() { }

        protected override void LeftStickStarted(Vector2 vec)
        {
            //横入力はキャンセル
            if (Mathf.Abs(vec.y / vec.x) < 1.0f) { return; }

            if (vec.y > 0)
            {
                mSelecter.GoBack();
            }
            else
            {
                mSelecter.GoForward();
            }
            LStick_prev = vec;
        }

        protected override void LeftStickPerformed(Vector2 vec)
        {

        }

        protected override void LeftStickCanceled(Vector2 vec)
        {
             LStick_prev = Vector2.zero;
            LStick_onTime = 0.0f;
        }

        protected override void RightStickStarted(Vector2 vec)
        {
            
        }

        protected override void RightStickPerformed(Vector2 vec)
        {

        }

        protected override void RightStickCanceled(Vector2 vec)
        {

        }
    }
}
