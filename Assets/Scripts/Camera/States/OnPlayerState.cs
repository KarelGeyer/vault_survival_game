using UnityEngine;

/// <summary>
/// This State takes care of controlling the behavior when we want the camera
/// to follow the player character.
/// This is pretty much a base state for the camera and it means the player is
/// not in the building mode which is controlled in <see cref="CameraSwitcher"/>
/// </summary>
public class OnPlayerState : CameraBaseState
{
    GameObject player;
    private Vector3 position;
    private Vector3 offset;

    public override void EnterState(CameraMovement cameraMovement, Transform transform)
    {
        player = GameObject.FindGameObjectWithTag(Constants.PLAYER);

        position = new Vector3(player.transform.position.x, player.transform.position.y + 5f, -15f);
        offset = position - player.transform.position;
        transform.position = player.transform.position + offset;
    }

    public override void UpdateState(CameraMovement cameraMovement, Transform transform)
    {
        transform.position = player.transform.position + offset;
    }
}
