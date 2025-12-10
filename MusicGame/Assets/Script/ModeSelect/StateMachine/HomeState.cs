using ModeSelect.StateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect
{
    public class HomeState : IState
    {
        public HomeState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {

        }
        protected override void OnEnter()
        {
            ModeSelect.Player.vecAction += (vector2) => Scroll(vector2);
            SetEnterAction(Actions[SelectNum[layer]]);
            ModeSelect.Player.enterAction += Actions[SelectNum[layer]];
            ModeSelect.Player.backAction += () => { };
        }
        protected override void OnUpdate(float deltaTime)
        {

        }
        protected override void OnExit()
        {

        }
    }
}
