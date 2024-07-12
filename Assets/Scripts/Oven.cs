using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Oven : MonoBehaviour
{
    [SerializeField] private float smeltSpeed;
    [SerializeField] private int maxOres;

    public Dictionary<string, OreScript> ores { get; set; } = new Dictionary<string, OreScript>();

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        Smelt();
    }

    public int GetMaxOres()
    {
        return maxOres;
    }

    private void Smelt()
    {
        if (ores.Count == 0) return;

        var firstOreEntry = ores.FirstOrDefault();
        if (firstOreEntry.Equals(default(KeyValuePair<string, OreScript>))) return;

        OreScript firstOre = firstOreEntry.Value;
        OreScriptableObject oreScriptable = firstOre.OreScriptableObject;

        if (oreScriptable.GetSmeltTime() > 0)
        {
            oreScriptable.SetSmeltTime(oreScriptable.GetSmeltTime() - Time.deltaTime * smeltSpeed);
        }
        else
        {
            gameManager.AddMoney(oreScriptable.GetOreValue());
            gameManager.RemoveOre(oreScriptable.GetOreName(), 1, ores);

            // Reset smelt time if there are more ores to smelt
            if (ores.Count > 0)
            {
                firstOreEntry = ores.FirstOrDefault();
                firstOre = firstOreEntry.Value;
                oreScriptable = firstOre.OreScriptableObject;
            }

            oreScriptable.SetSmeltTime(oreScriptable.GetInitialSmeltTime());
        }
    }

    public void AddOre(OreScriptableObject ore, int count)
    {
        if (gameManager.HasOre(ore.name, count, ores)) gameManager.AddOre(ore, count, ores);
        else if (ores.Count < maxOres) gameManager.AddOre(ore, count, ores);
    }
}
