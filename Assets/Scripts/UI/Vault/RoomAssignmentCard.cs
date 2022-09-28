using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

/// <summary>
/// Classe's purpose is to get the currently picked NPC,
/// pick a room and assign this NPC to that room to work
/// there.
/// </summary>
public class RoomAssignmentCard : MonoBehaviour
{
    public static event Action<Transform, string> OnRoomAssignment;
    public static event Action CloseRoomAssignmentCard;

    [SerializeField] private TextMeshProUGUI _characterName;
    [SerializeField] private TextMeshProUGUI _characterLevel;
    [SerializeField] private TextMeshProUGUI _roomName;
    [SerializeField] private TextMeshProUGUI _roomLevel;
    [SerializeField] private TextMeshProUGUI _roomPosition;
    [SerializeField] private TextMeshProUGUI _roomFreeSlots;

    Transform _chosenRoomPosition;

    private string _npcName;
    private int _npcLevel;
    private GameObject[] _rooms;

    private void OnEnable()
    {
        _npcName = CharacterCard.NpcName;
        _npcLevel = CharacterCard.NpcSkill;
        _rooms = CharacterCard.TargetRooms;

        _characterName.SetText(_npcName);
        _characterLevel.SetText($"level {_npcLevel}");
        _roomName.SetText("");
        _roomLevel.SetText("");
        _roomFreeSlots.SetText("");

        _chosenRoomPosition = null;
    }

    private void OnDisable()
    {
        DeselectRooms();
    }

    private void Update()
    {
        GetRoomData();
    }

    /// <summary>
    /// Method that gets the data about the highlighted room that
    /// player has picked to assign NPC for work. These data are
    /// then displayed on the RoomAssignmentCard.
    /// </summary>
    private void GetRoomData()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag(Constants.CHOOSE_ROOM))
                {
                    Transform room = hit.transform.parent.transform;
                    Transform level = room.transform.parent.transform.parent.transform.parent;

                    _chosenRoomPosition = room.Find("Npc's").transform;
                    int freeSlots = 3 - _chosenRoomPosition.childCount;

                    int levelNumber;

                    if(level.name == "Floor")
                    {
                        levelNumber = 1;
                    }
                    else
                    {
                        string floor = level.name.Split('(')[1];
                        levelNumber = int.Parse(Regex.Match(floor, @"\d+").Value) + 1;
                    }

                    _roomName.SetText(room.name);
                    _roomLevel.SetText($"floor {levelNumber}");
                    _roomFreeSlots.SetText($"{freeSlots}");
                }
            }
        }
    }

    /// <summary>
    /// Method triggers events that hides the card and send the npc
    /// to work in a chosen room.
    /// <see cref="VaultUI.CloseRoomAssignmentCard"/>
    /// <see cref="CharacterCard.SendNpcToLocation"/>
    /// <see cref="Npc.NavigateNpc"/>
    /// <see cref="Npc.HasAssignedWork"/>
    /// </summary>
    public void ConfirmRoomAssignment()
    {
        if (_chosenRoomPosition != null)
        {
            OnRoomAssignment.Invoke(_chosenRoomPosition, _npcName);
            CloseRoomAssignmentCard.Invoke();
        }
    }

    /// <summary>
    /// Method that hides the add (+) buttons that were previously
    /// enabled in <see cref="CharacterCard"/>.
    /// </summary>
    public void DeselectRooms()
    {
        foreach (GameObject room in _rooms)
        {
            Transform addButton = room.transform.Find("Add");
            addButton.gameObject.SetActive(false);
        }
    }
}
