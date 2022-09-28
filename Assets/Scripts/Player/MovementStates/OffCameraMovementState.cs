using UnityEngine;

/// <summary>
/// This State serves as a way to controll the Players movement
/// when in the Building mode.
/// The building mode is switched through a UI element <see cref="CameraSwitcher.SwitchCameraView"/>.
/// This State takes care of automating the Players activities and
/// movement while not being controlled by the player.
/// </summary>
public class OffCameraMovementState : MovementState
{
    float speed;
    float previousXPosition;
    float consoleXPosition;
    float consoleZPosition;
    float playerPositionY;
    float playerPositionZ;

    Rigidbody rb;
    Animator animator;
    Transform transform;

    GameObject nearestFloor;
    GameObject[] _floors;

    public override void EnterState(
        Movement movement,
        Rigidbody playerRb,
        Animator playerAnimator,
        Transform playerTransform
        )
    {
        speed = 3f;

        rb = playerRb;
        animator = playerAnimator;
        transform = playerTransform;

        playerPositionY = transform.position.y;
        playerPositionZ = transform.position.z;

        _floors = GameObject.FindGameObjectsWithTag(Constants.FLOOR_ROOM);

        FindNearestElevatorRoom();
        GetCoordinates();
    }

    public override void UpdateState(
        Movement movement,
        Rigidbody playerRb,
        Animator playerAnimator,
        Transform playerTransform
        )
    {
        MoveToNearestFloor();
        RotatePlayerInDirection();
        previousXPosition = transform.position.x;
    }

    public override void CollisionHandler(Collision collision){}

    /// <summary>
    /// This locates the room near the elevator based on the floor
    /// the player is currently in when the state is switched to
    /// Building mode
    /// </summary>
    void FindNearestElevatorRoom()
    {
        float objectDistance = 0;

        for (int i = 0; i < _floors.Length; i++)
        {
            if (i == 0)
            {
                nearestFloor = _floors[i];
                objectDistance = Vector3.Distance(_floors[i].transform.position, transform.position);
            } else if (i > 0)
            {
                float iterationDistance = Vector3.Distance(_floors[i].transform.position, transform.position);
                if(objectDistance > iterationDistance)
                {
                    objectDistance = iterationDistance;
                    nearestFloor = _floors[i];
                }
            }
        }
    }

    /// <summary>
    /// This gets the console coordinates when the state is switched to
    /// a Building mode
    /// </summary>
    void GetCoordinates()
    {
        Transform nearestConsole = nearestFloor.transform.Find(Constants.CONSOLE);
        consoleXPosition = nearestConsole.transform.position.x - 2f;
        consoleZPosition = nearestConsole.transform.position.z;
    }

    /// <summary>
    /// While in the Building mode, the player character is navigated
    /// to a nearest elevator room with the help of <see cref="FindNearestElevatorRoom"/>
    /// and <see cref="GetCoordinates"/>.
    /// Also takes care of using correct animations <see cref="MovementState.MoveAnimation"/>
    /// </summary>
    void MoveToNearestFloor()
    {
        Vector3 newXPosition = new Vector3(consoleXPosition, playerPositionY, playerPositionZ);
        Vector3 newZPosition = new Vector3(consoleXPosition, playerPositionY, consoleZPosition);
        float step = speed * Time.deltaTime;

        if(transform.position.x != newXPosition.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, newXPosition, step);
            MoveAnimation(1, animator);
        } else
        {
            MoveAnimation(0, animator);
            // Create Animation to interact with the console
        }
    }

    /// <summary>
    /// This function rotates the player based on its current direction.
    /// If the x position is decreasing, it rotates to the left.
    /// If the x position is increasing, it rotates to the right.
    /// </summary>
    void RotatePlayerInDirection()
    {
        if(previousXPosition > transform.position.x)
        {
            Vector3 rotateBackWards = new Vector3(0, -452f, 0f);
            transform.eulerAngles = rotateBackWards;
        }
        else if (previousXPosition < transform.position.x)
        {
            Vector3 rotateForwards = new Vector3(0, -270f, 0f);
            transform.eulerAngles = rotateForwards;
        }
    }
}
