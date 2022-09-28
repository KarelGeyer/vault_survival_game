using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This Class serves as a way to controll NPC and shows its stats
/// It takes care of:
/// <list>
///     <term>Displaying NPC's name</term>
///     <term>Displaying NPC's stats</term>
///     <term>Assining work to and NPC</term>
///     <term>Sending NPC to rest when needed</term>
///     <term>Removing NPC from the vault</term>
/// </list>
/// </summary>
public class CharacterCard : MonoBehaviour
{
    private static string _npcName;
    private static int _npcSkill;
    private static int _npcHappiness;
    private static GameObject[] _targetRooms;

    public static event Action<Transform, string> OnRoomAssignment;
    public static event Action OnRoomChoosing;

    [SerializeField] GameObject _characterName;
    [SerializeField] GameObject _characterLevel;
    [SerializeField] GameObject _characterHappiness;
    [SerializeField] GameObject[] _skills;

    // Getters and Setters
    public static string NpcName { get { return _npcName; } }
    public static int NpcSkill { get { return _npcSkill; } }
    public static int NpcHappiness { get { return _npcHappiness; } }
    public static GameObject[] TargetRooms { get { return _targetRooms; } }

    private void Update()
    {
        if (CameraSwitcher.IsInBuildingMode)
            GetCardStats();
    }

    /// <summary>
    /// Get the NPC's stats for a chosen NPC and displays them in the card.
    /// When all the information about the NPC is aquired, it triggeres a
    /// function to open the NPC's card
    /// <see cref="VaultUI.ShowCharacterCard"/>
    /// </summary>
    void GetCardStats()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag(Constants.NPC) && !VaultUI.WaitingForClick)
                {

                    Npc npc = hit.transform.GetComponent<Npc>();
                    _npcName = npc.name;
                    _npcSkill = npc._level;
                    _npcHappiness = npc._happiness;

                    List<int> skills = new()
                    {
                        npc._enduranceLevel,
                        npc._strengthLevel,
                        npc._intelligenceLevel,
                        npc._dexterityLevel
                    };

                    SetNpcName(_npcName);
                    SetNpcLevel(_npcSkill);
                    SetNpcSkills(skills);
                    SetCharacterHappines(_npcHappiness);

                    VaultUI.Instance.ShowCharacterCard();
                }
            }
        }
    }

    /// <summary>
    /// Following methods:
    /// <list>
    /// <see cref="SetNpcName"/>
    /// <see cref="SetNpcLevel"/>
    /// <see cref="SetCharacterHappines"/>
    /// <see cref="SetNpcSkills"/>
    /// </list>
    /// Serves as a way to set the Card's content with
    /// correct stats for chosen NPC
    /// </summary>

    void SetNpcName(string name)
    {
        TextMeshProUGUI text = _characterName.GetComponent<TextMeshProUGUI>();
        text.SetText(name);
    }

    void SetNpcLevel(int level)
    {
        TextMeshProUGUI text = _characterLevel.GetComponent<TextMeshProUGUI>();
        text.SetText($"level {level}");
    }

    void SetCharacterHappines(int happiness)
    {
        TextMeshProUGUI text = _characterHappiness.GetComponent<TextMeshProUGUI>();
        text.SetText($"{happiness}%");
    }

    /// <summary>
    /// This loops through a NPC's skill values and for each point Image
    /// in each skill Tab, it will collor the image depending on the skill
    /// value.
    /// Emaple:
    /// Strenght skill has value of 3 -> 3 images will have a blue color
    /// </summary>
    void SetNpcSkills(List<int> skills)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            int skill = skills[i];
            GameObject skillTextObject = _skills[i].transform.GetChild(1).gameObject;

            for (int j = 0; j < skill; j++)
            {
                Image skillImage = skillTextObject.transform.GetChild(j).GetComponent<Image>();
                skillImage.color = Color.blue;
            }
        }
    }

    /// <summary>
    /// Method bound to a button accepts a string which represent a name of a room.
    /// Its a part of assinging NPC to a specific room for it to work or rest there.
    /// It finds all the rooms give then name, currently active, loops through it
    /// and set it active as well as it sets the room Add button to highlight rooms
    /// for player to pick. Then it triggers an event opening the RoomAssignment Card.
    /// <see cref="VaultUI.ShowRoomAssignmentCard"/>
    /// <see cref="RoomAssignmentCard"/>
    /// <see cref="Npc.HasAssignedWork"/>
    /// <see cref="Npc.NavigateNpc"/>
    /// </summary>
    public void SendNpcToLocation(string roomName)
    {
        _targetRooms = GameObject.FindGameObjectsWithTag(roomName);

        if( _targetRooms.Length > 0)
        {
            foreach (GameObject room in _targetRooms)
            {
                Transform addButton = room.transform.Find("Add");
                addButton.gameObject.SetActive(true);
            }

            OnRoomChoosing?.Invoke();
        }
    }
}
