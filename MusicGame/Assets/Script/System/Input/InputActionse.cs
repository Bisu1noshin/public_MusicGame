using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

//入力した値をを管理する構造体
public class ReciveInputValue
{

    public Vector2 moveVec { get; set; }
    public Vector2 Lookvec { get; set; }
    public bool BottonA { get; set; }
    public bool BottonB { get; set; }
    public bool BottonX { get; set; }
    public bool BottonY { get; set; }

    public ReciveInputValue()
    {

        moveVec = new Vector2(0, 0);
        Lookvec = new Vector2(0, 0);
        BottonA = false;
        BottonB = false;
        BottonX = false;
        BottonY = false;
    }
}

// イベントアクションを管理する構造体
public sealed class InputActionse
{

    public InputAction _LeftStick { get; private set; }
    public InputAction _RightStick { get; private set; }
    public InputAction _ButtonA { get; private set; }
    public InputAction _ButtonB { get; private set; }
    public InputAction _ButtonX { get; private set; }
    public InputAction _ButtonY { get; private set; }

    // コンストラクタ
    public InputActionse()
    {
        InitilizeInputActionse();
    }

    // イベントアクションの有効化
    public void OnEnable()
    {
        _LeftStick.Enable();
        _RightStick.Enable();
        _ButtonA.Enable();
        _ButtonB.Enable();
        _ButtonX.Enable();
        _ButtonY.Enable();
    }

    // イベントアクションの無効化
    public void OnDisable()
    {
        _LeftStick.Disable();
        _RightStick.Disable();
        _ButtonA.Disable();
        _ButtonB.Disable();
        _ButtonX.Disable();
        _ButtonY.Disable();
    }

    // 初期化
    public void InitilizeInputActionse()
    {
        _LeftStick = new InputAction(name: "Move", type: InputActionType.Value, expectedControlType: "Vector2");
        _RightStick = new InputAction(name: "Look", type: InputActionType.Value, expectedControlType: "Vector2");
        _ButtonA = new InputAction(name: "ButtonA", type: InputActionType.Button, expectedControlType: "Button");
        _ButtonB = new InputAction(name: "ButtonB", type: InputActionType.Button, expectedControlType: "Button");
        _ButtonX = new InputAction(name: "ButtonX", type: InputActionType.Button, expectedControlType: "Button");
        _ButtonY = new InputAction(name: "ButtonY", type: InputActionType.Button, expectedControlType: "Button");
    }
}