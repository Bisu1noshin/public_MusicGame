using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public enum CursorState
    {
        None = -1, ChangeElement, EnterORCancel
    }
    public abstract class PopupAdmin<OwnerClass, STrigger> : Kameda_StateBase<OwnerClass, STrigger>
        where OwnerClass : class
        where STrigger : struct, Enum
    {
        protected PopupController mPopup;
        protected IPopupCursorController mCursorOwner;
        CursorState mCursorState;
        bool canChangeElement;
        bool CurrisEnter; //決定orキャンセルの時今が決定か
        Action rightAction, leftAction, enter, back;
        public PopupAdmin(OwnerClass owner, IStateMachine<STrigger> st, bool canChange, CursorState InitialCursorState = CursorState.ChangeElement)
            : base(owner, st)
        {
            mCursorOwner = base.mSceneManager;
            canChangeElement = canChange;
            mCursorState = InitialCursorState;
            CurrisEnter = true;
        }
        protected void SetInitialEnterAction(bool _enter)
        {
            if (_enter)
            {
                Player.enterAction = () => mSceneManager.PlaySound(0);
                Player.enterAction += enter;
                mCursorOwner.TrySetPopupCursorPos(false, true);
            }
            else
            {
                Player.enterAction = () => mSceneManager.PlaySound(1);
                Player.enterAction += back;
                mCursorOwner.TrySetPopupCursorPos(false, false);
            }
        }
        protected void CreateCursor()
        {
            mSceneManager.CreatePopupCursor();
            mSceneManager.TrySetPopupCursorPos(canChangeElement, true);
        }
        protected void ChangeState(Vector2 vec)
        {
            //横入力
            if (Mathf.Abs(vec.y / vec.x) < 1f)
            {
                if (mCursorState == CursorState.ChangeElement)
                {
                    if (vec.x > 0f)
                    {
                        rightAction?.Invoke();
                    }
                    else
                    {
                        leftAction?.Invoke();
                    }
                }
                else
                {
                    CurrisEnter = !CurrisEnter;
                    if (CurrisEnter)
                    {
                        Player.enterAction = () => mSceneManager.PlaySound(0);
                        Player.enterAction += enter;
                        mCursorOwner.TrySetPopupCursorPos(false, true);
                    }
                    else
                    {
                        Player.enterAction = () => mSceneManager.PlaySound(1);
                        Player.enterAction += back;
                        mCursorOwner.TrySetPopupCursorPos(false, false);
                    }
                }
            }
            //縦入力
            else
            {
                if (!canChangeElement) return;
                if (mCursorState == CursorState.ChangeElement)
                {
                    mCursorState = CursorState.EnterORCancel;
                    mCursorOwner.TrySetPopupCursorPos(false, CurrisEnter);

                    if (CurrisEnter)
                    {
                        Player.enterAction = () => mSceneManager.PlaySound(0);
                        Player.enterAction += enter;
                        mCursorOwner.TrySetPopupCursorPos(false, true);
                    }
                    else
                    {
                        Player.enterAction = () => mSceneManager.PlaySound(1);
                        Player.enterAction += back;
                        mCursorOwner.TrySetPopupCursorPos(false, false);
                    }
                }
                else
                {
                    mCursorState = CursorState.ChangeElement;
                    mCursorOwner.TrySetPopupCursorPos(true);
                }
            }
        }
        protected void SetChangeElementAction(Action right, Action left)
        {
            rightAction = right;
            leftAction = left;
        }
        protected void SetEnterAndCancelAction(Action _enter, Action _back)
        {
            enter = _enter;
            back = _back;
        }
    }
}
