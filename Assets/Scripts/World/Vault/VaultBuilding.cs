using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class takes care of building rooms within the Vault
/// It takes care of:
/// <list>
///     <term>Building empty rooms</term>
///     <term>Building specific rooms</term>
///     <term>Assinging prices to room and spending currency</term>
///     <term>Selecting and Deselecting Slots and Rooms</term>
/// </list>
/// </summary>
public class VaultBuilding : MonoBehaviour
{
    public static event Action OpenConfirmPopup;
    public static event Action OpenRoomPicker;
    public static event Action<MoneyOperation, int> ConfirmTransaction;

    GameObject[] _roomSlots;
    GameObject _parentToTransform;
    GameObject _roomsWrapper;
    GameObject _emptyRoom;

    private void OnEnable()
    {
        EmptyRoomPopup.ConfirmEmptyRoomCreation += CreateNewEmptyRoom;
        VaultUI.ResetRoomRoomColor += ResetRoomSlotsColors;
        RoomPickerPopup.ConfirmRoomCreation += CreateNewRoom;
    }

    private void OnDisable()
    {
        EmptyRoomPopup.ConfirmEmptyRoomCreation -= CreateNewEmptyRoom;
        VaultUI.ResetRoomRoomColor -= ResetRoomSlotsColors;
        RoomPickerPopup.ConfirmRoomCreation -= CreateNewRoom;
    }

    private void Awake()
    {
        _roomSlots = GameObject.FindGameObjectsWithTag(Constants.ROOM_SLOT);
    }
    void Update()
    {
        /// <summary>
        /// In order for this to work proper, only alowing this to
        /// work in building mode
        /// </summary>
        if (CameraSwitcher.IsInBuildingMode)
            CreateEmptyRoom();
    }

    /// <summary>
    /// Using Raycast on mouse to choose a specific ground object
    /// Once chosen, ground object will turn blue
    /// Then it opens up a respective UI popup depending on whether user clicks on ground object or empty room
    /// For empty room creation: <see cref="VaultUI.OpenEmptyRoomConfirmPopup"/>
    /// For specific room creation: <see cref="VaultUI.OpenRoomPicker"/>
    /// </summary>
    void CreateEmptyRoom()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag(Constants.ROOM_SLOT) && !VaultUI.WaitingForClick)
                {
                    ResetRoomSlotsColors();

                    Material material = hit.transform.gameObject.GetComponent<MeshRenderer>().material;
                    material.SetColor("_Color", Color.blue);

                    // Sets the parent to a clicked object instance
                    // to prevent all the objects being manipulated
                    _parentToTransform = hit.transform.parent.gameObject;

                    OpenConfirmPopup?.Invoke();
                }

                if (hit.transform.CompareTag(Constants.EMPTY_ROOM) && !VaultUI.WaitingForClick)
                {
                    ResetRoomSlotsColors();

                    // Sets the parent to a clicked object instance
                    // to prevent all the objects being manipulated
                    _emptyRoom = hit.transform.parent.gameObject;
                    GameObject slot = _emptyRoom.transform.parent.gameObject;
                    _roomsWrapper = slot.transform.Find(Constants.ROOMS).gameObject;

                    OpenRoomPicker?.Invoke();
                }
            }
        }
    }

    /// <summary>
    /// Function makes sure the user deselect the room
    /// </summary>
    void ResetRoomSlotsColors()
    {
        foreach (GameObject roomSlot in _roomSlots)
        {
            Material roomSlotMaterial = roomSlot.GetComponent<MeshRenderer>().material;
            roomSlotMaterial.SetColor("_Color", Color.white);
        }
    }

    /// <summary>
    /// Function takes the now assigned object <see cref="CreateEmptyRoom"/>
    /// and transforms it to a new room.
    /// Also makes sure its not for free due event invocation
    /// Function takes the now assigned object <see cref="Money.SetAmount"/>
    /// </summary>
    void CreateNewEmptyRoom()
    {
        if(_parentToTransform != null)
        {
            GameObject ground = _parentToTransform.transform.GetChild(0).gameObject;
            GameObject emptyRoom = _parentToTransform.transform.GetChild(1).gameObject;

            ground.SetActive(false);
            emptyRoom.SetActive(true);

            ConfirmTransaction?.Invoke(MoneyOperation.SPEND, 100);
        }
    }

    /// <summary>
    /// Function takes the now assigned object <see cref="CreateEmptyRoom"/>
    /// and transforms it to a new room.
    /// Also makes sure its not for free due event invocation
    /// Function takes the now assigned object <see cref="Money.SetAmount"/>
    /// </summary>
    void CreateNewRoom(string roomName)
    {
        if(_roomsWrapper != null && _emptyRoom != null)
        {
            GameObject room = _roomsWrapper.transform.Find(roomName).gameObject;
            room.SetActive(true);
            _emptyRoom.SetActive(false);

            ConfirmTransaction?.Invoke(MoneyOperation.SPEND, GetRoomPrice(roomName));
        }
    }

    /// <summary>
    /// Taking care of proper amount of money being spend on different room
    /// </summary>
    /// <returns>int -> representing the price of the room</returns>
    int GetRoomPrice(string roomName)
    {
        int price = 0;

        switch (roomName)
        {
            case "Water_Factory":
                price = 250;
                break;

            case "Power_Plant":
                price = 250;
                break;

            case "Restaurant":
                price = 150;
                break;

            case "Bed_Room":
                price = 150;
                break;

            case "Storage":
                price = 200;
                break;

            case "Amunition_Room":
                price = 200;
                break;

            case "MedBay":
                price = 250;
                break;

            case "Game_Room":
                price = 200;
                break;

            default:
                price = 0;
                break;
        }

        return price;
    }
}
