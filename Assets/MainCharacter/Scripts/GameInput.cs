using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }

    public bool GetJump()
    {
        return _playerInputActions.Player.Jump.WasPressedThisFrame();
    }


    public bool GetDash()
    {
        if(_playerInputActions.Player.Dash.IsPressed())
        {
            return true;
        }
        
        return false;
    }

    public bool GetDig()
    {
        if(_playerInputActions.Player.Dig.IsPressed())
        {
            return true;
        }
        return false;
    }
}
