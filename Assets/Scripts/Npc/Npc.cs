using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class takes care of all the NPC's actions, movements and activities
/// <list>
///     <item>managing NPC's work</item>
///     <item>managing NPC's movement and interactions</item>
///     <item>managing NPC's stats</item>
///     <item>managing NPC's gets some rest</item>
///     <item>removing the NPC from the vault</item>
///     <item>Managing how and whether NPC should die</item>
/// </list>
/// </summary>
public class Npc : MonoBehaviour
{
    Vector3 _workLocation;
    GameObject _currentNpc;


    // NPC STATS
    public string _name;
    public string _sex;
    public string _position;
    public int _level;
    public int _happiness;
    public int _enduranceLevel;
    public int _strengthLevel;
    public int _intelligenceLevel;
    public int _dexterityLevel;

    public float _accumulatedHappiness;
    public float _accumulatedSkill;
    public float _accumulatedLevel;

    private void OnEnable()
    {
        RoomAssignmentCard.OnRoomAssignment += AssignRoom;
    }

    private void OnDisable()
    {
        RoomAssignmentCard.OnRoomAssignment -= AssignRoom;
    }

    /// <summary>
    /// Following functions basically just take care of reusing
    /// the <see cref="Name"/> <see cref="Skill"/> methods to
    /// assign correct values for the NPC when instantiated in
    /// <see cref="SpawnNpc"/>
    /// Methods:
    /// <list>
    ///     <see cref="AssignLevel"/>
    ///     <see cref="AssignName"/>
    ///     <see cref="AssignHappiness"/>
    ///     <see cref="AssignSkillValues"/>
    /// </list>
    /// </summary>
    public void AssignLevel()
    {
        _level = UnityEngine.Random.Range(Constants.MIN_LEVEL, Constants.MAX_START_LEVEL);
    }

    public void AssignName()
    {
        _sex = transform.name.Split('_')[0];
        _name = Name.Instance.GetRandomName(_sex);
    }

    public void AssignHappiness(int value)
    {
        _happiness = value;
    }

    public void AssignSkillValues()
    {
        _enduranceLevel = Skill.Instance.GetRandomSkillLevel(_enduranceLevel);
        _strengthLevel = Skill.Instance.GetRandomSkillLevel(_strengthLevel);
        _intelligenceLevel = Skill.Instance.GetRandomSkillLevel(_intelligenceLevel);
        _dexterityLevel = Skill.Instance.GetRandomSkillLevel(_dexterityLevel);
    }

    public void Update()
    {
        Transform room = transform.parent.transform.parent;
        if (room.name == "MedBay" || room.name == "Water_Factory")
        {
            DecreaseHappines();
        }
        else if (room.name == "Game_Room" || room.name == "Bed_Room")
        {
            IncreaseHappines();
        }
    }

    /// <summary>
    /// Method triggered by even in <see cref="RoomAssignmentCard"/>
    /// that Navigates the NPC to a provided room picked previously in
    /// <see cref="RoomAssignmentCard"/>. Then it sets the room's
    /// NPC container (Npc's) as parent of the NPC to keep track
    /// of NPC's currently assigned to work there. Room also has a
    /// limit of npc's currently working there to 3, more cannot be
    /// assigned.
    /// <see cref="RoomAssignmentCard.ConfirmRoomAssignment"/>
    /// <see cref="CharacterCard.SendNpcToLocation"/>
    /// </summary>
    void AssignRoom(Transform targetLocation, string npcName)
    {
        if (targetLocation != null)
        {
            _currentNpc = GameObject.Find(npcName);

            if (_currentNpc != null)
            {
                Transform npcContainer = targetLocation;
                Transform npcPositionContainer = targetLocation.parent.transform.Find("Npc_position");
                string roomName = npcContainer.parent.name;

                print(roomName);

                if (npcContainer.childCount < 4)
                {
                    _currentNpc.transform.SetParent(targetLocation);
                    _currentNpc.GetComponent<Npc>()._position = roomName + "specialist";

                    if(roomName == "MedBay")
                    {
                        NpcManager.Instance.SetMedbaySpecialists(_currentNpc);
                        print(roomName);
                    }

                    if (roomName == "Water_Factory")
                    {
                        NpcManager.Instance.SetWaterFactorySpecialists(_currentNpc);
                        print(roomName);
                    }
                }


                if (npcContainer.childCount > 0 && npcContainer.childCount < 4)
                {
                    Vector3 npcPosition = npcPositionContainer.GetChild(npcContainer.childCount - 1).transform.position;
                    Vector3 newNpcPosition = new Vector3(npcPosition.x, npcPosition.y + 0.5f, npcPosition.z);
                    _currentNpc.transform.position = newNpcPosition;
                }
            }
        }
    }

    void DecreaseHappines()
    {
        _accumulatedHappiness += Time.deltaTime;

        if(_accumulatedHappiness > 10 && _happiness > 0)
        {
            AssignHappiness(_happiness - 1);
            _accumulatedHappiness = 0f;
        }
    }

    void IncreaseHappines()
    {
        _accumulatedHappiness += Time.deltaTime;

        if (_accumulatedHappiness > 4 && _happiness < 100)
        {
            AssignHappiness(_happiness + 1);
            _accumulatedHappiness = 0f;
        }
    }

    public void SetDexterity(Npc npc)
    {
        npc._dexterityLevel += 1;
    }

    public void SetStrength(Npc npc)
    {
        npc._strengthLevel += 1;
    }

    public void SetIntelligence(Npc npc)
    {
        npc._intelligenceLevel += 1;
    }

    public void SetEndurance(Npc npc)
    {
        npc._enduranceLevel += 1;
    }
}
