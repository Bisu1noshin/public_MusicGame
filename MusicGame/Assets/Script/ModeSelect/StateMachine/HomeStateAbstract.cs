using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    [Serializable]
    //複数の分岐を管理するホーム画面のクラステンプレート
    public abstract class HomeStateAbstract<OwnerClass, STrigger> : Kameda_StateBase<OwnerClass, STrigger>
        where OwnerClass : class
        where STrigger : struct, Enum
    {
        protected List<Action> mActions;
        protected int mSelectNum, mPrevSelectNum;
        public HomeStateAbstract(OwnerClass owner, IStateMachine<STrigger> st) : base(owner, st)
        {
            mActions = new();
            mSelectNum = 0;
        }

        protected void Scroll(Vector2 vector2)
        {
            if (vector2 == Vector2.zero || Mathf.Abs(vector2.y / vector2.x) < 1f) return;

            mSelectNum += vector2.y < 0f ? 1 : -1;
            if (mSelectNum < 0)
            {
                mSelectNum = 0;
                PlayBeepSound();
            }
            else if (mSelectNum > mActions.Count - 1)
            {
                mSelectNum = mActions.Count - 1;
                PlayBeepSound();
            }
            else
            {
                PlayShiftSound();
            }
        }
    }
}
