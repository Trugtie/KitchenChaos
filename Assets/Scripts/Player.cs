using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNomolized();

        Vector3 moveDir = new Vector3(inputVector.x, 0.0f, inputVector.y);

        transform.position += moveDir * Time.deltaTime * moveSpeed;

        this.isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return this.isWalking;
    }
}
