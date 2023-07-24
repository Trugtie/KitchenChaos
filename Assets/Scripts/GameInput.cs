using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternate;
    public event EventHandler OnPauseAction;
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        Instance = this;
        this.playerInputActions = new PlayerInputActions();
        this.playerInputActions.Player.Enable();
        this.playerInputActions.Player.Interact.performed += Interact_performed;
        this.playerInputActions.Player.InterractAlternate.performed += InterractAlternate_performed;
        this.playerInputActions.Player.Pause.performed += Pause_performed;
    }
    private void OnDestroy()
    {
        this.playerInputActions.Player.Interact.performed -= Interact_performed;
        this.playerInputActions.Player.InterractAlternate.performed -= InterractAlternate_performed;
        this.playerInputActions.Player.Pause.performed -= Pause_performed;

        this.playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
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
