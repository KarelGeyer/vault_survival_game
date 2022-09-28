using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A base class taking care of managing the Camera states
/// through <see cref="CameraBaseState"/> and <see cref="CameraSwitcher"/>
/// </summary>
public class CameraMovement : MonoBehaviour
{
    CameraBaseState state;
    public ShelterState ShelterState;
    public OnPlayerState OnPlayerState;

    private Transform cameraTransform;

    private void Awake()
    {
        ShelterState = new ShelterState();
        OnPlayerState = new OnPlayerState();

        cameraTransform = transform;

        state = OnPlayerState;
        state.EnterState(this, cameraTransform);
    }

    void Update()
    {
        state.UpdateState(this, cameraTransform);
    }

    public void SwitchState()
    {
        if (state == ShelterState)
        {
            state = OnPlayerState;
        } else
        {
            state = ShelterState;
        }

        state.EnterState(this, transform);
    }
}
