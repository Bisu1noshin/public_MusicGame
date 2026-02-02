using System;
using UnityEngine;

namespace ModeSelect
{
    public enum CursorState
    {
        None = -1, ChangeElement, EnterORCancel
    }
    public abstract class PopupAdminAbstract
    {
        CursorState mCursorState, mPrevCursorState;
        bool canChangeElement;
        bool CurrisEnter; //決定orキャンセルの時今が決定か
        Action rightAction, leftAction, enter, back;
        public PopupAdminAbstract(bool canChange, CursorState InitialCursorState)
        {
            canChangeElement = canChange;
            mCursorState = InitialCursorState;
            mPrevCursorState = InitialCursorState;
        }
        protected void UpdateState(Vector2 vec)
        {
            if (Mathf.Abs(vec.y / vec.x) < 1f)
            {
                if (vec.x > 0f)
                {

                }
            }
        }
        protected void SetChangeElementAction(Action right,  Action left)
        {
            rightAction = right;
            leftAction = left;
        }
        protected void SetEnterAndCancelAction(Action enter, Action back)
        {
            this.enter = enter;
            this.back = back;
        }
    }
}