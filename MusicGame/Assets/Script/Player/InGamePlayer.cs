using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public enum PlayerState {

        None,
        Up,
        Down,
        Left,
        Right,
        Idle
    }

    public class ReceiveInput {

        public Vector3 LeftVec;

        public Vector3 RightVec;

        public ReceiveInput() {

            LeftVec = new Vector3();
            RightVec = new Vector3();
        }
    } 

    public class InGamePlayer : PlayerParent {

        public PlayerState LeftState { get; private set; }
        public PlayerState RightState { get; private set; }

        public ReceiveInput input;

        private void Start()
        {
            input = new ReceiveInput();

            LeftState = PlayerState.Idle;
            RightState = PlayerState.Idle;
        }

        private void Update()
        {
            LeftState = InputAction(input.LeftVec);
            RightState = InputAction(input.RightVec);
        }

        protected override void OnButtonA() { }
        protected override void UpButtonA() { }
        protected override void OnButtonB() { }
        protected override void UpButtonB() { }
        protected override void OnButtonX() { }
        protected override void UpButtonX() { }
        protected override void OnButtonY() { }
        protected override void UpButtonY() { }
        protected override void MoveUpdate(Vector2 vec) {

            input.LeftVec = vec;
        }
        protected override void LookUpdate(Vector2 vec) {

            input.RightVec = vec;
        }


        private PlayerState InputAction(Vector3 vec) {

            PlayerState state = PlayerState.Idle;

            if (vec.x >= 0.7) { state = PlayerState.Left; }
            if (vec.x <= -0.7) { state = PlayerState.Right; }
            if (vec.y >= 0.7) { state = PlayerState.Up; }
            if (vec.y <= -0.7) { state = PlayerState.Down; }

            return state;
        }
    }
}