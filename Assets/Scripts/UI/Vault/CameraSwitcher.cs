using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class takes care of toggling between Player and Building Mode
/// while in the Vault Scene. This is done through switching <see cref="Player"/> and
/// <see cref="Camera"/> states.
/// </summary>
public class CameraSwitcher : MonoBehaviour
{
    static bool isInBuildingMode;
    public static bool IsInBuildingMode { get { return isInBuildingMode; } }

    GameObject playerCamera;
    CameraMovement cameraMovement;
    GameObject player;
    Movement playerMovement;

    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag(Constants.CAMERA);
        cameraMovement = playerCamera.GetComponent<CameraMovement>();

        player = GameObject.FindGameObjectWithTag(Constants.PLAYER);
        playerMovement = player.GetComponent<Movement>();
    }


    public void SwitchCameraView()
    {
        cameraMovement.SwitchState();
        playerMovement.SwitchState();

        if (isInBuildingMode)
        {
            isInBuildingMode = false;
        }
        else isInBuildingMode = true;
    }
}
