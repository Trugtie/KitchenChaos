using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        this.playerInputActions = new PlayerInputActions();
        this.playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNomolized()
    {
        Vector2 inputVector = this.playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
