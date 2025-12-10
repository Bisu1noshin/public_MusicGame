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

            manager = GameObject.Find("NotesGenarator").GetComponent<Notes.NotesManager>();
        }

        private void Update()
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            int lane = 1;
            if (collision.gameObject.transform.position.x < 0) { lane = 0; }

            if (collision.gameObject.TryGetComponent<Notes.NotesObject>(out var n_))
            {
                if (notes[lane] != null) { notes[lane] = null; }
                notes[lane] = n_;
            }
        }

        private PlayerState InputAction(Vector3 vec) {

            PlayerState state = PlayerState.Idle;

            if (vec.x > 0) { state = PlayerState.Right; }
            if (vec.x < 0) { state = PlayerState.Left; }
            if (vec.y > 0) { state = PlayerState.Up; }
            if (vec.y < 0) { state = PlayerState.Down; }

            return state;
        }
    }
}