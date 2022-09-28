using UnityEngine;

/// <summary>
/// This State serves as a way to give the player full control over the character's
/// movement and when not in the Building mode
/// The Building mode is switched through a UI element <see cref="CameraSwitcher.SwitchCameraView"/>.
/// </summary>
public class OnCameraMovementState : MovementState
{
    float walkSpeed = 3f;
    float runSpeed = 7f;
    float jumpForce = 300f;

    float movementX;
    float speed;
    bool isGrounded;

    Rigidbody rb;
    Animator animator;
    Transform transform;

    public override void EnterState(
        Movement movement,
        Rigidbody playerRb,
        Animator playerAnimator,
        Transform playerTransform
        )
    {
        speed = walkSpeed;
        isGrounded = true;

        rb = playerRb;
        animator = playerAnimator;
        transform = playerTransform;
    }

    public override void UpdateState(
        Movement movement,
        Rigidbody playerRb,
        Animator playerAnimator,
        Transform playerTransform
        )
    {
        MovePlayer();
        Jump();
    }

    /// <summary>
    /// As the game is 2.5 D, I really only care about moving the player along
    /// x axis and y axis.
    /// This function takes care of the x axis.
    /// This function allows character to walk or run and manage apropirate
    /// animations and character rotation as well based on that.
    /// </summary>
    void MovePlayer()
    {
        movementX = Input.GetAxis(Constants.HORIZONTAL);
        transform.position += speed * Time.deltaTime * new Vector3(movementX, 0f, 0f);

        if (movementX < 0)
        {
            Vector3 rotateBackWards = new Vector3(0, -452f, 0f);
            transform.eulerAngles = rotateBackWards;
        }
        else if (movementX > 0)
        {
            Vector3 rotateForwards = new Vector3(0, -270f, 0f);
            transform.eulerAngles = rotateForwards;
        }

        ManageAnimations();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }

    public override void CollisionHandler(Collision collision)
    {
        /// <summary>
        /// This if statement controlls where the character is grounded
        /// or not which impacts whether he can jump or not
        /// <see cref="Jump"/>
        /// </summary>
        if (collision.gameObject.CompareTag(Constants.GROUND)
            || collision.gameObject.CompareTag(Constants.FLOOR))
        {
            isGrounded = true;
        }
    }

    /// <summary>
    /// Functionality controlling the jumping mechanism for the players
    /// character. The character can only jump if he is ground which is
    /// cotnrolled in the <see cref="CollisionHandler"/>
    /// </summary>
    void Jump()
    {
        if (Input.GetButtonDown(Constants.JUMP_KEY))
        {
            if (isGrounded)
            {
                rb.AddForce(transform.up * jumpForce);
                isGrounded = false;
            }
        }
    }

    /// <summary>
    /// Simple animation handler manipulating the character animation based
    /// on wheter the character is moving and how fast
    /// <see cref="MovePlayer"/>
    /// <see cref="MovementState.MoveAnimation"/>
    /// </summary>
    void ManageAnimations()
    {
        if (isGrounded)
        {
            if (movementX < 0 || movementX > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MoveAnimation(2, animator);
                }
                else
                {
                    MoveAnimation(1, animator);
                }
            }

            if (movementX == 0)
            {
                MoveAnimation(0, animator);
            }
        }
        else
        {
            MoveAnimation(0, animator);
        }
    }
}
