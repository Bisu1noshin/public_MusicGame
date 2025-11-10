using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerParent : MonoBehaviour
{
    private InputKeyBind inputKey;

    private InputAction _LeftStick;
    private InputAction _RightStick;
    private InputAction _ButtonA;
    private InputAction _ButtonB;
    private InputAction _ButtonX;
    private InputAction _ButtonY;

    protected virtual void Awake()
    {
        inputKey = new InputKeyBind();

        _LeftStick = inputKey.inputActionse._LeftStick;
        _RightStick = inputKey.inputActionse._RightStick;
        _ButtonA = inputKey.inputActionse._ButtonA;
        _ButtonB = inputKey.inputActionse._ButtonB;
        _ButtonX = inputKey.inputActionse._ButtonX;
        _ButtonY = inputKey.inputActionse._ButtonY;
    }

    private void OnEnable()
    {
        inputKey.inputActionse.OnEnable();

        _LeftStick.started += OnLeftStick;
        _LeftStick.performed += OnLeftStick;
        _LeftStick.canceled += OnLeftStick;
        _RightStick.started += OnRightStick;
        _RightStick.performed += OnRightStick;
        _RightStick.canceled += OnRightStick;
        _ButtonA.started += EnterButtonA;
        _ButtonA.canceled += ExitButtonA;
        _ButtonB.started += EnterButtonB;
        _ButtonB.canceled += ExitButtonB;
        _ButtonX.started += EnterButtonX;
        _ButtonX.canceled += ExitButtonX;
        _ButtonY.started += ExitButtonY;
        _ButtonY.canceled += EnterButtonY;
    }

    private void OnDisable()
    {
        inputKey.inputActionse.OnDisable();

        _LeftStick.started -= OnLeftStick;
        _LeftStick.performed -= OnLeftStick;
        _LeftStick.canceled -= OnLeftStick;
        _RightStick.started -= OnRightStick;
        _RightStick.performed -= OnRightStick;
        _RightStick.canceled -= OnRightStick;
        _ButtonA.started -= EnterButtonA;
        _ButtonA.canceled -= ExitButtonA;
        _ButtonB.started -= EnterButtonB;
        _ButtonB.canceled -= ExitButtonB;
        _ButtonX.started -= EnterButtonX;
        _ButtonX.canceled -= ExitButtonX;
        _ButtonY.started -= ExitButtonY;
        _ButtonY.canceled -= EnterButtonY;
    }

    // 抽象メソッド

    abstract protected void OnButtonA();
    abstract protected void UpButtonA();
    abstract protected void OnButtonB();
    abstract protected void UpButtonB();
    abstract protected void OnButtonX();
    abstract protected void UpButtonX();
    abstract protected void OnButtonY();
    abstract protected void UpButtonY();
    abstract protected void MoveUpdate(Vector2 vec);
    abstract protected void LookUpdate(Vector2 vec);

    // Actionのメソッド

    private void OnLeftStick(InputAction.CallbackContext context)
    {

        Vector2 vec = context.ReadValue<Vector2>();

        MoveUpdate(vec);
    }

    private void OnRightStick(InputAction.CallbackContext context)
    {
        Vector2 vec = context.ReadValue<Vector2>();

        LookUpdate(vec);
    }

    private void EnterButtonA(InputAction.CallbackContext context)
    {
        OnButtonA();
    }

    private void ExitButtonA(InputAction.CallbackContext context)
    {

        UpButtonA();
    }

    private void EnterButtonB(InputAction.CallbackContext context)
    {
        OnButtonB();
    }

    private void ExitButtonB(InputAction.CallbackContext context)
    {
        UpButtonB();
    }

    private void EnterButtonY(InputAction.CallbackContext context)
    {
        OnButtonY();
    }

    private void ExitButtonY(InputAction.CallbackContext context)
    {
        UpButtonY();
    }

    private void EnterButtonX(InputAction.CallbackContext context)
    {
        OnButtonX();
    }

    private void ExitButtonX(InputAction.CallbackContext context)
    {

        UpButtonX();
    }
}
