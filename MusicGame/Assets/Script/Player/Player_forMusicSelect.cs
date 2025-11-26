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
            Debug.Log("Is MusicSelecter NULL? : " + musicSelecter == null);
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

        protected override void LookUpdate(Vector2 vec)
        {
            
        }
        protected override void MoveUpdate(Vector2 vec)
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
        protected override void StopMove()
        {
            LStick_prev = Vector2.zero;
            LStick_onTime = 0.0f;
        }
    }
}
