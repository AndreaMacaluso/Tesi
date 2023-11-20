using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    //singleton
    public static GameInput Instance { get; private set; }

    public event EventHandler OnPauseAction; 
    private PlayerInputActions playerInputActions;
    
  
    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed (UnityEngine.InputSystem.InputAction.CallbackContext obj){

        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    
    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }


}
