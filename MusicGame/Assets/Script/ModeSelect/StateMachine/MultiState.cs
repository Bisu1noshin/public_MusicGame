using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public class MultiState : IState
    {
        public MultiState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {

        }
        protected override void OnEnter()
        {
            InitAction();
            Actions = new List<Action>(2);
            deleteAction += Button.ButtonManager.CreateInstance(this, 0, 2, "1P", () => { Debug.Log("Multi Owner pressed"); }, false);
            deleteAction += Button.ButtonManager.CreateInstance(this, 1, 2, "2P", () => { Debug.Log("Multi Client pressed"); }, false);
        }
        protected override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
        }
        protected override void OnExit()
        {
            
        }
        void InitAction()
        {
            layer = 0;
            ModeSelect.Player.vecAction += (vector2) => Scroll(vector2);
            ReplaceEnterAction(Actions[SelectNum[layer]]);
            ModeSelect.Player.backAction += () => { };
        }
    }
}
