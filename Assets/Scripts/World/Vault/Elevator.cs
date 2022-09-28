using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class controlls the Elevator actions based on the caller
/// <list>
///     <item>Takes care of transporting the player to chosen floors</item>
///     <item>Takes care of transporting the NPC to chosen floors</item>
/// </list>
/// <summary>
public class Elevator : MonoBehaviour
{
    private static bool canNpcComeInside;
    public static bool CanNpcComeInside { get { return canNpcComeInside; } set { canNpcComeInside = value; } }

    private static bool canNpcExit;
    public static bool CanNpcExit { get { return canNpcExit; } set { canNpcExit = value; } }

    [SerializeField] private Transform triggerPointPosition;
    [SerializeField] private GameObject elevatorController;

    private GameObject player;

    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private Vector3 velocity;

    private float speed = 7f;
    private float smoothMovement = 0.2f;
    private float triggerValue = 3f;
    private float distanceFromPlayer;

    private float positionX;
    private float positionY;
    private float positionZ;

    private bool shouldMove;
    private bool isMoving;
    private bool isMovingToNpc;
    private bool didNpcChooseFloor;

    private string _floor;

    private float _firstFloor = 3.2f;
    private float _secondFloor = -18.7f;
    private float _thirdFloor = -27.9f;

    private void Awake() // THIS WILL NEED TO USE COMMAND PATTERN TO QUEUE CALLS;
    {
        player = GameObject.FindGameObjectWithTag(Constants.PLAYER);

        positionX = transform.position.x;
        positionY = transform.position.y;
        positionZ = transform.position.z;

        initialPosition = new Vector3(positionX, positionY, positionZ);
        targetPosition = initialPosition;

        isMoving = false;
        isMovingToNpc = false;
        canNpcComeInside = false;
        canNpcExit = false;
        StartCoroutine(IsMoving());
    }

    void FixedUpdate()
    {
        distanceFromPlayer = Vector3.Distance(triggerPointPosition.position, player.transform.position);

        /// <summary>
        /// Triggers the Elevator UI for player to choose a floor
        /// Player Mode Only
        /// <summary>
        if (distanceFromPlayer < triggerValue)
        {
            elevatorController.SetActive(true);
        }
        else
        {
            elevatorController.SetActive(false);
            player.transform.SetParent(null, true);
        }

        /// <summary>
        /// If the elevator is moving, it hides the UI
        /// Player Mode Only
        /// <summary>
        if (isMoving)
        {
            elevatorController.SetActive(false);
        }

        /// <summary>
        /// Sets the player as a child to solve physics issue
        /// Player Mode Only
        /// <summary>
        if (shouldMove)
        {
            player.transform.SetParent(transform, true);
            MoveElevator();
        }
    }

    /// <summary>
    /// Sets the position the elevator is supposed to move
    /// Player Mode Only
    /// <summary>
    public void SetTargetPosition(float floorPosition)
    {
        targetPosition = new Vector3(positionX, floorPosition, positionZ);
        shouldMove = true;
    }


    /// <summary>
    /// Moves Elevator in a provided position
    /// Player Mode Only
    /// <summary>
    private void MoveElevator()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothMovement, speed);
    }

    /// <summary>
    /// Checks if the elevator is moving
    /// Player Mode Only
    /// <summary>
    private IEnumerator IsMoving()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(1f);

        Vector3 finalPos = transform.position;

        if (startPos.y != finalPos.y)
        {
            isMoving = true;
        } else
        {
            isMoving = false;
        }
    }
}
