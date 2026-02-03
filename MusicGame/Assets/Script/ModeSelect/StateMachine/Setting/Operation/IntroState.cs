using System;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public class IntroState : PopupAdmin<ISceneManager, Trigger>
    {
        Sprite intro_K, intro_C;
        bool curr;
        Action enter, back;
        public IntroState(ISceneManager owner, IStateMachine<Trigger> st) : base(owner, st, false, CursorState.EnterORCancel)
        {
            intro_K = mSceneManager.Resource.GetSprite("Intro_K");
            intro_C = mSceneManager.Resource.GetSprite("Intro_C");
            curr = true;
            Debug.Log($"Intro_K : {intro_K.name}\nIntro_C : {intro_C.name}");
        }
        protected override void OnEnter()
        {
            Player.enterAction = null;
            curr = true;
            CreateImagePopupInstance(ref mPopup, intro_K);
            CreateCursor();
            Action shift = () =>
            {
                curr = !curr;
                if (curr)
                {
                    mPopup.SetImage(intro_K);
                }
                else
                {
                    mPopup.SetImage(intro_C);
                }
            };
            Action cancel = () =>
            {
                stateMachine.ExecuteTriggerAction(Trigger.Home);
            };
            enter = shift;
            back = cancel;
            Player.vecAction = (vec) => ChangeIntroImages(vec);
            mCursorOwner.TrySetPopupCursorPos(true, true, false);
        }
        protected override void OnUpdate(float deltaTime)
        {
            
        }
        protected override void OnExit()
        {
            mSceneManager.TryDeletePopupCursor();
            deleteAction?.Invoke();
            deleteAction = null;
        }
        protected void ChangeIntroImages(Vector2 vec)
        {
            //横入力
            if (Mathf.Abs(vec.y / vec.x) < 1f)
            {
                curr = !curr;
                if (curr)
                {
                    Player.enterAction = () => mSceneManager.PlaySound(0);
                    Player.enterAction += enter;
                }
                else
                {
                    Player.enterAction = () => mSceneManager.PlaySound(1);
                    Player.enterAction += back;
                    
                }
                mCursorOwner.TrySetPopupCursorPos(true, curr, false);
            }
        }
    }
}
