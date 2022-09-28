using UnityEngine;

/// <summary>
/// Class serves as template for Players states
/// </summary>
public abstract class MovementState
{
    /// <summary>
    /// Function to be run when the state is changed or on Awake
    /// </summary>
    public abstract void EnterState(
        Movement movement,
        Rigidbody playerRb,
        Animator playerAnimator,
        Transform playerTransform
        );

    /// <summary>
    /// Function to be run as an update method
    /// </summary>
    public abstract void UpdateState(
        Movement movement,
        Rigidbody playerRb,
        Animator playerAnimator,
        Transform playerTransform
        );

    /// <summary>
    /// Base collision Handler
    /// </summary>
    public abstract void CollisionHandler(Collision collision);

    /// <summary>
    /// This function manages the players movement animations
    /// </summary>
    protected void MoveAnimation(int value, Animator animator)
    {
        animator.SetInteger(Constants.MOVEMENT, value);
    }
}
