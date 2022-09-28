using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class takes care of spawning the NPC's and assigning correct values
/// to each one of them randomly.
/// The functionalities are written in <see cref="Npc"/>
/// </summary>
public class SpawnNpc : MonoBehaviour
{
    [SerializeField] GameObject[] _npcPrefabs;
    [SerializeField] GameObject _vaultContainer;

    public void SpawnNewNpc()
    {
        GameObject newNpc = Instantiate(_npcPrefabs[Random.Range(0, _npcPrefabs.Length)]);
        Npc npc = newNpc.GetComponent<Npc>();
        npc.AssignLevel();
        npc.AssignSkillValues();
        npc.AssignName();
        npc.AssignHappiness(80);

        newNpc.name = npc._name;
        newNpc.transform.position = transform.position;
        newNpc.transform.SetParent(_vaultContainer.transform);
    }
}
