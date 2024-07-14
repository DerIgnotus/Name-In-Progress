using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ore : MonoBehaviour
{
    [SerializeField] private int miningLevel;
    [SerializeField] private float harvestTime;
    [SerializeField] private int harvestAmount;
    [SerializeField] private float untilNextMine;
    [SerializeField] private string oreName;

    [SerializeField] private Item item_1;
    [SerializeField] private OreScriptableObject ore_1;

    public UnityEvent<string> oreHarvested;
    public UnityEvent<string> oreHarvestedByRobot;

    private float currentUntilNextMine;
    private float currentHarvestTime;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (gameManager.npcsParent == null) gameManager.npcsParent = GameObject.Find("NPCs").transform;

        for (int i = 0; i < gameManager.npcsParent.childCount; i++)
        {
            int index = i;
            oreHarvested.AddListener((oreName) =>
            {
                if (index < gameManager.npcsParent.childCount)
                {
                    gameManager.npcsParent.GetChild(index).GetComponent<NPC>().OreHarvested(oreName);
                }
            });
            oreHarvestedByRobot.AddListener((oreName) =>
            {
                if (index < gameManager.npcsParent.childCount)
                {
                    gameManager.npcsParent.GetChild(index).GetComponent<NPC>().OreHarvestedByRobot(oreName);
                }
            });
        }


        currentUntilNextMine = 0;
        currentHarvestTime = harvestTime;
    }

    private void Update()
    {
        if (currentUntilNextMine > 0)
        {
            currentUntilNextMine -= Time.deltaTime;
        }
    }

    public float GetHarvestTime()
    {
        return harvestTime;
    }

    public int GetHarvestAmount()
    {
        return harvestAmount;
    }

    public int GetMiningLevel()
    {
        return miningLevel;
    }

    public string GetOreName()
    {
        return oreName;
    }

    public void Harvest(float harvestDamage)
    {
        currentHarvestTime -= harvestDamage;
        if (currentHarvestTime <= 0)
        {
            oreHarvested.Invoke(oreName);
            HarvestOre();
        }
    }

    public void HarvestedByRobot(float harvestDamage)
    {
        currentHarvestTime -= harvestDamage;
        if (currentHarvestTime <= 0)
        {
            oreHarvestedByRobot.Invoke(oreName);
            oreHarvested.Invoke(oreName);
            HarvestOre();
        }
    }

    private void HarvestOre()
    {
        gameManager.AddItem(item_1, 1);
        gameManager.AddOre(ore_1, 1, gameManager.GetOres());

        gameManager.updateInventory.Invoke();
        gameManager.updateOres.Invoke();
        gameManager.updateQuests.Invoke();

        currentUntilNextMine = untilNextMine;
        currentHarvestTime = harvestTime;
    }

    public void HarvestOreOwnHarvestTimer(int harvestAmount)
    {
        oreHarvested.Invoke(oreName);

        gameManager.AddItem(item_1, 1 * harvestAmount);

        gameManager.updateInventory.Invoke();
        gameManager.updateQuests.Invoke();

        if (gameManager.ovens.Count == 0) return;

        foreach (Oven oven in gameManager.ovens)
        {
            if (oven.AddOre(ore_1, 1 * harvestAmount))
            {
                break;
            }
        }
    }

    public bool CanMine()
    {
        return currentUntilNextMine <= 0;
    }
}
