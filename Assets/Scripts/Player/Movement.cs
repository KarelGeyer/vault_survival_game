using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A base class taking care of managing the player movement states
/// through <see cref="OffCameraMovementState"/> and <see cref="OnCameraMovementState"/>
/// which are states managed by <see cref="MovementState"/>
/// </summary>
public class Movement : MonoBehaviour
{
    MovementState movementState;
    public OnCameraMovementState onCameraMovementState;
    public OffCameraMovementState offCameraMovementState;

    [SerializeField] GameObject playerCamera;

    private Rigidbody playerRb;
    private Animator playerAnimator;
    private Transform playerTransform;

    private void Awake()
    {
        onCameraMovementState = new OnCameraMovementState();
        offCameraMovementState = new OffCameraMovementState();

        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        playerTransform = transform;

        movementState = onCameraMovementState;
        movementState.EnterState(this, playerRb, playerAnimator, playerTransform);
    }

    void Update()
    {
        movementState.UpdateState(this, playerRb, playerAnimator, playerTransform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        movementState.CollisionHandler(collision);
    }

    public void SwitchState()
    {
        if (movementState == onCameraMovementState)
        {
            movementState = offCameraMovementState;
        }
        else
        {
            movementState = onCameraMovementState;
        }

        movementState.EnterState(this, playerRb, playerAnimator, playerTransform);
    }
}
