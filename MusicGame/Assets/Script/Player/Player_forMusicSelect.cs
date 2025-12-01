using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public class Player_forMusicSelect : PlayerParent
    {
        IMusicSelecter musicSelecter;
        float LStick_onTime;

        Vector2 LStick_prev;

        private void Start()
        {
            musicSelecter = GameObject.Find("SceneManager").GetComponent<MusicSelectSceneManager>();
            LStick_onTime = 0;
        }

        private void Update()
        {
            if(LStick_onTime > 0.5f)
            {
                if (LStick_prev.y > 0)
                {
                    musicSelecter.GoBack();
                }
                else
                {
                    musicSelecter.GoForward();
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
            musicSelecter.Enter();
        }
        protected override void OnButtonB()
        {
            musicSelecter.Undo();
        }
        protected override void OnButtonX() { }
        protected override void OnButtonY() { }
        protected override void UpButtonA() { }
        protected override void UpButtonB() { }
        protected override void UpButtonX() { }
        protected override void UpButtonY() { }

        protected override void LeftStickStarted(Vector2 vec)
        {
            if (vec.y > 0)
            {
                musicSelecter.GoBack();
            }
            else
            {
                musicSelecter.GoForward();
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
