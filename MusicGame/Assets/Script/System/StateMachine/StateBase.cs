using System;
using UnityEngine;

public abstract class StateBase<OwnerClass,TTrigger> : IStateMapping
    where OwnerClass : class
    where TTrigger : struct, IConvertible, IComparable
{
    public Action onEnter => OnEnter;
    public Action onExit => OnExit;
    public Action<float> onUpdate => OnUpdate;

    // 抽象メソッド

    abstract protected void OnEnter();
    abstract protected void OnExit();
    abstract protected void OnUpdate(float deltaTime);

    // メンバー変数

    protected OwnerClass owner;
    protected IStateMachine<TTrigger> stateMachine;

    // コンストラクタ

    public StateBase(OwnerClass owner,IStateMachine<TTrigger> st)
    {
        this.owner = owner;
        stateMachine = st;
    }
}
