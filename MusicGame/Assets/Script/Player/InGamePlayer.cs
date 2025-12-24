using Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Player
{
    public enum PlayerState {

        None = -1,
        Up,
        Right,
        Down,
        Left,
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

        private PlayerState[] state;

        public ReceiveInput input;

        private NotesObject[] notes;
        private Notes.NotesManager manager;
        public static Action<PlayerState, float>[] NotesAction = new Action<PlayerState, float>[2];

        private void Start()
        {
            input = new ReceiveInput();

            state = new PlayerState[2];
            for (int i = 0; i < state.Length; i++)
            {
                state[i] = PlayerState.Idle;
            }

            notes = new NotesObject[2];
        }

        private void FixedUpdate()
        {
            // ノーツの処理
            {
                for (int i = 0; i < state.Length; i++)
                {
                    if (state[i] != PlayerState.Idle)
                    {
                        NotesAction[i]?.Invoke(state[i], manager.InGameTime);
                    }
                }        
            }
        }

        // 入力アクション

        protected override void OnButtonA() { }
        protected override void UpButtonA() { }
        protected override void OnButtonB() { }
        protected override void UpButtonB() { }
        protected override void OnButtonX() { }
        protected override void UpButtonX() { }
        protected override void OnButtonY() { }
        protected override void UpButtonY() { }

        protected override void LeftStickStarted(Vector2 vec)
        {
            state[0] = InputAction(vec);
        }

        protected override void LeftStickPerformed(Vector2 vec)
        {
            state[0] = InputAction(vec);
        }

        protected override void LeftStickCanceled(Vector2 vec)
        {
            state[0] = InputAction(vec);
        }

        protected override void RightStickStarted(Vector2 vec)
        {
            state[1] = InputAction(vec);
        }

        protected override void RightStickPerformed(Vector2 vec)
        {
            state[1] = InputAction(vec);
        }

        protected override void RightStickCanceled(Vector2 vec)
        {
            state[1] = InputAction(vec);
        }

        protected override void SetPlayerInput()
        {
            manager = GameObject.Find("NotesGenarator").GetComponent<Notes.NotesManager>();
            this.inputDevice = manager.NotesManagerData.PlayerConfig.InputDevice;
        }

        private PlayerState InputAction(Vector3 vec) {

            PlayerState state = PlayerState.Idle;

            if (vec.x > 0.9f) { state = PlayerState.Right; }
            if (vec.x < -0.9f) { state = PlayerState.Left; }
            if (vec.y > 0.9f) { state = PlayerState.Up; }
            if (vec.y < -0.9f) { state = PlayerState.Down; }

            return state;
        }
    }
}