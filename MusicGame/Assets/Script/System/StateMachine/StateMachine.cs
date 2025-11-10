using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 各State毎のdelagateを登録しておくインターフェース
public interface IStateMapping
{
    public Action onEnter { get; }
    public Action onExit { get; }
    public Action<float> onUpdate { get; }
}

// トリガーを実行部分をインターフェース化
public interface IStateMachine<TTrigger>
{
    public Action<TTrigger> ExecuteTriggerAction { get; }
}

// 遷移先の条件を保存するクラス
public class Transition<TState, TTrigger>
{
    public TState To { get; set; }
    public TTrigger Trigger { get; set; }
}

public class StateMachine<TState, TTrigger>: IStateMachine<TTrigger>
    where TState : struct, IConvertible, IComparable
    where TTrigger : struct, IConvertible, IComparable
{
    public Action<TTrigger> ExecuteTriggerAction => ExecuteTrigger;

    private TState _stateType;

    private Dictionary<object, IStateMapping> _stateMappings = new Dictionary<object, IStateMapping>();
    private Dictionary<TState, List<Transition<TState, TTrigger>>> _transitionLists = new Dictionary<TState, List<Transition<TState, TTrigger>>>();

    public StateMachine(TState initialState)
    {
        // StateからStateMappingを作成
        var enumValues = Enum.GetValues(typeof(TState));
        for (int i = 0; i < enumValues.Length; i++)
        {
            // 登録しなかった場合のためにnullを入れる
            IStateMapping _mapping = null;
            _stateMappings.Add(enumValues.GetValue(i), _mapping);
        }

        // 最初のStateに遷移
        ChangeState(initialState);
    }

    /// <summary>
    /// トリガーを実行する
    /// </summary>
    private void ExecuteTrigger(TTrigger trigger)
    {
        // 現在の状態からの遷移が登録されていない
        if (!_transitionLists.TryGetValue(_stateType, out var transitions))
        {
            throw new InvalidOperationException(
                GetState().ToString() + "は遷移条件が登録されていません");
        }

        // リストから遷移条件にあったstateに変更する
        foreach (var transition in transitions)
        {
            if (transition.Trigger.Equals(trigger))
            {
                ChangeState(transition.To);
                return;
            }
        }

        throw new InvalidOperationException(
               trigger.ToString() + "は" + GetState().ToString() + "の遷移条件に登録されていません");
    }

    /// <summary>
    /// 遷移情報を登録する
    /// </summary>
    public void AddTransition(TState from, TState to, TTrigger trigger)
    {
        if (!_transitionLists.ContainsKey(from))
        {
            _transitionLists.Add(from, new List<Transition<TState, TTrigger>>());
        }
        var transitions = _transitionLists[from];
        var transition = transitions.FirstOrDefault(x => x.To.Equals(to));
        if (transition == null)
        {
            // 新規登録
            transitions.Add(new Transition<TState, TTrigger> { To = to, Trigger = trigger });
        }
        else
        {
            // 更新
            transition.To = to;
            transition.Trigger = trigger;
        }
    }

    /// <summary>
    /// Stateを初期化する
    /// </summary>
    public void SetupState(TState state, IStateMapping mapping)
    {
        _stateMappings[state] = mapping;
    }
    /// <summary>
    /// 更新する
    /// </summary>
    public void Update(float deltaTime)
    {
        if (_stateMappings[_stateType] != null && _stateMappings[_stateType].onUpdate != null)
        {
            _stateMappings[_stateType].onUpdate(deltaTime);
        }
    }

    /// <summary>
    /// 現在のステータスを取得する
    /// </summary>
    public TState GetState()
    {
        return _stateType;
    }

    /// <summary>
    /// Stateを直接変更する
    /// </summary>
    private void ChangeState(TState to)
    {
        // OnExit
        if (_stateMappings[_stateType] != null && _stateMappings[_stateType].onExit != null)
        {
            _stateMappings[_stateType].onExit();
        }

        // OnEnter
        _stateType = to;
        if (_stateMappings[_stateType] != null && _stateMappings[_stateType].onEnter != null)
        {
            _stateMappings[_stateType].onEnter();
        }
    }
}
