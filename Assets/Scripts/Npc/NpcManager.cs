using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    private static NpcManager instance;
    public static NpcManager Instance { get { return instance; } }

    private List<GameObject> MedbaySpecialists = new ();
    private List<GameObject> WaterFactorySpecialists = new();

    public void SetMedbaySpecialists(GameObject npc)
    {
        MedbaySpecialists.Add(npc);
    }

    public void SetWaterFactorySpecialists(GameObject npc)
    {
        WaterFactorySpecialists.Add(npc);
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        IncreaseStats();
        // print(MedbaySpecialists.Count);
    }

    void IncreaseStats()
    {
        if (MedbaySpecialists != null)
        {
            foreach (GameObject npc in WaterFactorySpecialists)
            {
                Npc npcController = npc.GetComponent<Npc>();

                if(npcController._position == "Water_Factoryspecialist")
                {
                    npcController._accumulatedSkill += Time.deltaTime;

                    if (npcController._accumulatedSkill > 30)
                    {
                        if (npcController._intelligenceLevel < Constants.MAX_NPC_ATTR_LEVEL)
                            npcController._intelligenceLevel += 1;

                        npcController._accumulatedSkill = 0;
                    }
                }
            }

            foreach (GameObject npc in MedbaySpecialists)
            {
                Npc npcController = npc.GetComponent<Npc>();

                if (npcController._position == "MedBayspecialist")
                {
                    npcController._accumulatedSkill += Time.deltaTime;

                    if (npcController._accumulatedSkill > 30)
                    {
                        if (npcController._intelligenceLevel < Constants.MAX_NPC_ATTR_LEVEL)
                            npcController._strengthLevel += 1;

                        npcController._accumulatedSkill = 0;
                    }
                }
            }
        }
    }
}
