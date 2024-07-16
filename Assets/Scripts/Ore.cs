using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Ore : MonoBehaviour
{
    [SerializeField] private int miningLevel;
    [SerializeField] private float harvestTime;
    [SerializeField] private int harvestAmount;
    [SerializeField] private float untilNextMine;
    [SerializeField] private string oreName;
    [SerializeField] private GameObject flyingText;

    [SerializeField] private Item item_1;
    [SerializeField] private OreScriptableObject ore_1;

    public UnityEvent<string> oreHarvested;
    public UnityEvent<string> oreHarvestedByRobot;

    private float currentUntilNextMine;
    private float currentHarvestTime;
    private GameManager gameManager;
    private Renderer oreRenderer;
    private Color originalColor;

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

        oreRenderer = GetComponent<Renderer>();
        if (oreRenderer != null)
        {
            originalColor = oreRenderer.material.color;
        }
    }

    private void Update()
    {
        if (currentUntilNextMine > 0)
        {
            currentUntilNextMine -= Time.deltaTime;
        }

        UpdateOreColor();
    }

    private void UpdateOreColor()
    {
        if (oreRenderer != null)
        {
            if (currentUntilNextMine > 0)
            {
                // Blend between the original color and red
                oreRenderer.material.color = Color.Lerp(originalColor, Color.red, 0.75f);
            }
            else
            {
                // Restore the original color
                oreRenderer.material.color = originalColor;
            }
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

        Mined(1 * harvestAmount);

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
        Mined(1 * harvestAmount);

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

    private void Mined(int amount)
    {
        GameObject textObject = Instantiate(flyingText, transform.position, Quaternion.identity);
        string text;
        text = oreName + ": " + amount.ToString() + "\n" + item_1.GetItemName() + ": 1";

        TextMeshPro textMeshPro = textObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        textMeshPro.text = text;
    }

    public bool CanMine()
    {
        return currentUntilNextMine <= 0;
    }
}
