using UnityEngine;

/// <summary>
/// This class serves as a State template for Camera Position and behavior.
/// <see cref="OnPlayerState"/>
/// <see cref="ShelterState"/>
/// </summary>
public abstract class CameraBaseState
{
    public abstract void EnterState(CameraMovement cameraMovement, Transform transform);

    public abstract void UpdateState(CameraMovement cameraMovement, Transform transform);

}
