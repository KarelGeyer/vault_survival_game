using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This Class helps to assign random skill values when the NPC is instantiated.
/// Serves as a helper to ->  <see cref="Npc"/>
/// </summary>
public class Skill : MonoBehaviour
{
    private static Skill instance;
    public static Skill Instance { get { return instance; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// <see cref="Npc.AssignSkillValues"/>
    /// </summary>
    /// <returns>
    /// an Random Integer representing NPC's skill value
    /// </returns>
    public int GetRandomSkillLevel(int skillLevel)
    {
        skillLevel = Random.Range(Constants.MIN_NPC_ATTR_LEVEL, Constants.MAX_NPC_ATTR_START_LEVEL);
        return skillLevel;
    }
}
