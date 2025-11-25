using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public enum ButtonKind
    {
        A, B, X, Y
    }

    public class Player_forMusicSelect : PlayerParent
    {
        IMusicSelecter musicSelecter;
        float LStick_onTime, RStick_onTime;
        private void Start()
        {
            musicSelecter = GameObject.Find("SceneManager").GetComponent<MusicSelectSceneManager>();
            LStick_onTime = 0;
        }

        private void Update()
        {

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
            if (vec == Vector2.zero)
            {
                LStick_onTime = 0;
                return;
            }


            LStick_onTime += Time.deltaTime;
        }
    }
}
