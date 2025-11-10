using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
public sealed class InputKeyBind {

    public InputActionse inputActionse { get; private set; }

    public InputKeyBind() {

        inputActionse = new InputActionse();

        // 仮
        InitilizeBindGamepad();
    }

    /// <summary>
    /// カスタマイズしたキーバインドを割り当てる
    /// </summary>
    public void SetKeyBind(InputAction action) {

        
    }

    /// <summary>
    /// ゲームパッドとバインド
    /// </summary>
    public void InitilizeBindGamepad()
    {
        // すでにバインドされていたら処理なし

        if (inputActionse._LeftStick.bindings.Count == 0) 
            inputActionse._LeftStick.AddBinding("<Gamepad>/leftStick");

        if (inputActionse._RightStick.bindings.Count == 0)
            inputActionse._RightStick.AddBinding("<Gamepad>/rightStick");

        if (inputActionse._ButtonA.bindings.Count == 0)
            inputActionse._ButtonA.AddBinding("<Gamepad>/buttonSouth");

        if (inputActionse._ButtonB.bindings.Count == 0)
            inputActionse._ButtonB.AddBinding("<Gamepad>/buttonEast");

        if (inputActionse._ButtonX.bindings.Count == 0)
            inputActionse._ButtonX.AddBinding("<Gamepad>/buttonWest");

        if (inputActionse._ButtonY.bindings.Count == 0)
            inputActionse._ButtonY.AddBinding("<Gamepad>/buttonNorth");
    }

    /// <summary>
    /// マウスとキーボードバインド
    /// </summary>
    public void InitilizeBindKeyboard() {

        // すでにバインドされていたら処理なし

        if (inputActionse._LeftStick.bindings.Count == 0)
            inputActionse._LeftStick.AddCompositeBinding("2DVector")
        .With("Up", "<Keyboard>/w")
        .With("Down", "<Keyboard>/s")
        .With("Left", "<Keyboard>/a")
        .With("Right", "<Keyboard>/d");

        if (inputActionse._RightStick.bindings.Count == 0)
            inputActionse._RightStick.AddBinding("<Mouse>/Position");

        if (inputActionse._ButtonA.bindings.Count == 0)
            inputActionse._ButtonA.AddBinding("<Keyboard>/space");

        if (inputActionse._ButtonB.bindings.Count == 0)
            inputActionse._ButtonB.AddBinding("<Mouse>/leftButton");

        if (inputActionse._ButtonX.bindings.Count == 0)
            inputActionse._ButtonX.AddBinding("<Keyboard>/leftShift");

        if (inputActionse._ButtonY.bindings.Count == 0)
            inputActionse._ButtonY.AddBinding("<Mouse>/rightButton");
    }

    public void ReMoveBinding(InputAction action) {

        action.RemoveAllBindingOverrides();
    }
}
