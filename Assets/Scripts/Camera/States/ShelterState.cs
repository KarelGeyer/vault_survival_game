using UnityEngine;

/// <summary>
/// This State gives the player limited control over the camera and is the
/// gate to the Building mode.
/// When in this mode, the player is allowed to move the camera around
/// to see the full scale of the Vault without the dependence on player's
/// character position.
/// The mode/state is controlled in <see cref="CameraSwitcher"/>
/// </summary>
public class ShelterState : CameraBaseState
{
    private float movementX;
    private float movementY;
    private float speed = 25f;

    public override void EnterState(CameraMovement cameraMovement, Transform transform)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10f);
    }

    public override void UpdateState(CameraMovement cameraMovement, Transform transform)
    {
        movementX = Input.GetAxis(Constants.HORIZONTAL);
        movementY = Input.GetAxis(Constants.VERTICAL);

        transform.position += speed * Time.deltaTime * new Vector3(movementX, movementY, 0f);
    }
}
