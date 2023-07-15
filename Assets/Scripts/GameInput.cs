using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternate;
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        this.playerInputActions = new PlayerInputActions();
        this.playerInputActions.Player.Enable();
        this.playerInputActions.Player.Interact.performed += Interact_performed;
        this.playerInputActions.Player.InterractAlternate.performed += InterractAlternate_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InterractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNomolized()
    {
        Vector2 inputVector = this.playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
