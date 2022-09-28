using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class controlls the Vault Doors, It's functionality is to
/// open the door when player or NPC is near to it.
/// <summary>
public class Door : MonoBehaviour
{
    private GameObject _player;
    private GameObject[] _npcs;
    private Animator _doorAnimator;

    private float _distanceFromPlayer;
    private float _distanceFromNpc;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag(Constants.PLAYER);
        _npcs = GameObject.FindGameObjectsWithTag(Constants.NPC);
        _doorAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        _distanceFromPlayer = Vector3.Distance(transform.position, _player.transform.position);
        HandleDoor();
    }

    /// <summary>
    /// Opens the door once player is near the door
    /// </summary>
    void HandleDoor()
    {
        if (_distanceFromPlayer < 4 || _distanceFromNpc < 4)
        {
            _doorAnimator.SetBool(Constants.IS_DOOR_OPENED, true);
        }
        else _doorAnimator.SetBool(Constants.IS_DOOR_OPENED, false);
    }
}
