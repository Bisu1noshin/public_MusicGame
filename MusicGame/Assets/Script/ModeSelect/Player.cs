using DG.Tweening;
using Player;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ModeSelect
{
    public class Player : PlayerParent
    {
        public static Action enterAction { get; set; }
        public static Action backAction { get; set; }
        public static Action<Vector2> vecAction { get; set; }
        protected override void OnButtonA()
        {
            enterAction?.Invoke();
            enterAction = null;
        }
        protected override void OnButtonB()
        {
            backAction?.Invoke();
            backAction = null;
        }
        
        protected override void LeftStickStarted(Vector2 vec)
        {
            if (vec.y / vec.x < 1.0f) return;
            float Y = vec.y < 0.0f ? 1.0f : -1.0f;
            vecAction?.Invoke(new(0.0f, Y));
        }
        protected override void OnButtonX() { }
        protected override void OnButtonY() { }
        protected override void UpButtonA() { }
        protected override void UpButtonB() { }
        protected override void UpButtonX() { }
        protected override void UpButtonY() { }
        protected override void LeftStickCanceled(Vector2 vec)
        {
            
        }
        protected override void LeftStickPerformed(Vector2 vec) { }
        
        protected override void RightStickStarted(Vector2 vec) { }
        protected override void RightStickPerformed(Vector2 vec) { }
        protected override void RightStickCanceled(Vector2 vec) { }
    }
}
