using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform itemPrefab;
    [SerializeField] private Transform whereInstantiateItems;
    [SerializeField] private Transform orePrefab;
    [SerializeField] private Transform whereInstantiateOres;

    [SerializeField] private Transform robotPrefab;
    [SerializeField] private Transform whereInstantiateRobots;

    [SerializeField] private Transform inventory;

    private Dictionary<string, ItemScript> items = new Dictionary<string, ItemScript>();
    private Dictionary<string, OreScript> ores = new Dictionary<string, OreScript>();

    public void AddItem(Item newItem, int count)
    {
        string itemName = newItem.GetItemName();
        if (items.ContainsKey(itemName))
        {
            ItemScript existingItem = items[itemName];
            int currentCount = existingItem.Count;

            if (newItem.GetMaxItemCount() == 0)
            {
                existingItem.Count = currentCount + count;
            }
            else if (currentCount < newItem.GetMaxItemCount())
            {
                if (currentCount + count > newItem.GetMaxItemCount())
                {
                    existingItem.Count = newItem.GetMaxItemCount();
                }
                else
                {
                    existingItem.Count = currentCount + count;
                }
            }
        }
        else
        {
            items[itemName] = new ItemScript(newItem, count);
        }
        UpdateItems();
    }

    public Transform GetInventoryTransform()
    {
        return inventory;
    }

    public bool HasItem(string itemName, int count)
    {
        if (items.ContainsKey(itemName))
        {
            return items[itemName].Count >= count;
        }
        return false;
    }

    public void RemoveItem(string itemName, int count)
    {
        if (items.ContainsKey(itemName))
        {
            ItemScript item = items[itemName];
            item.Count -= count;
            if (item.Count <= 0)
            {
                items.Remove(itemName);
            }
            UpdateItems();
        }
    }

    public void DisplayItems()
    {
        inventory.GetChild(0).gameObject.SetActive(true);

        foreach (ItemScript item in items.Values)
        {
            GameObject itemObject = Instantiate(itemPrefab.gameObject, whereInstantiateItems);
            Transform itemTransform = itemObject.transform;

            itemTransform.GetChild(0).GetComponent<Image>().sprite = item.Item.GetItemSprite();
            itemTransform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = item.Item.GetItemName() + " x" + item.Count;
            itemTransform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = item.Item.GetItemDescription();
        }
    }

    public void RemoveItems()
    {
        inventory.GetChild(0).gameObject.SetActive(false);

        foreach (Transform item in whereInstantiateItems)
        {
            Destroy(item.gameObject);
        }
    }

    public void UpdateItems()
    {
        if (inventory.GetChild(0).gameObject.activeSelf)
        {
            RemoveItems();
            DisplayItems();
        }
        else
        {
            RemoveItems();
        }
    }

    public void AddOre(OreScriptableObject newOre, int count)
    {
        string oreName = newOre.GetOreName();
        if (ores.ContainsKey(oreName))
        {
            OreScript existingOre = ores[oreName];
            int currentCount = existingOre.Count;

            if (newOre.GetMaxOreCount() == 0)
            {
                existingOre.Count = currentCount + count;
            }
            else if (currentCount < newOre.GetMaxOreCount())
            {
                if (currentCount + count > newOre.GetMaxOreCount())
                {
                    existingOre.Count = newOre.GetMaxOreCount();
                }
                else
                {
                    existingOre.Count = currentCount + count;
                }
            }
        }
        else
        {
            ores[oreName] = new OreScript(newOre, count);
        }
        UpdateOres();
    }

    public bool HasOre(string oreName, int count)
    {
        if (ores.ContainsKey(oreName))
        {
            return ores[oreName].Count >= count;
        }
        return false;
    }

    public void RemoveOre(string oreName, int count)
    {
        if (ores.ContainsKey(oreName))
        {
            OreScript ore = ores[oreName];
            ore.Count -= count;
            if (ore.Count <= 0)
            {
                ores.Remove(oreName);
            }
            UpdateOres();
        }
    }

    public void DisplayOres()
    {
        inventory.GetChild(1).gameObject.SetActive(true);

        foreach (OreScript ore in ores.Values)
        {
            GameObject oreObject = Instantiate(orePrefab.gameObject, whereInstantiateOres);
            Transform oreTransform = oreObject.transform;

            oreTransform.GetChild(0).GetComponent<Image>().sprite = ore.OreScriptableObject.GetOreSprite();
            oreTransform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = ore.OreScriptableObject.GetOreName() + " x" + ore.Count;
            oreTransform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = ore.OreScriptableObject.GetOreDescription();
        }
    }

    public void RemoveOres()
    {
        inventory.GetChild(1).gameObject.SetActive(false);

        foreach (Transform ore in whereInstantiateOres)
        {
            Destroy(ore.gameObject);
        }
    }

    public void UpdateOres()
    {
        if (inventory.GetChild(1).gameObject.activeSelf)
        {
            RemoveOres();
            DisplayOres();
        }
        else
        {
            RemoveOres();
        }
    }

    public void DisplayRobots(List<Robot> robots)
    {
        int i = 0;

        foreach (Transform child in whereInstantiateRobots)
        {
            Destroy(child.gameObject);
        }

        foreach (Robot robot in robots)
        {

            GameObject robotObject = Instantiate(robotPrefab.gameObject, whereInstantiateRobots);
            Transform robotTransform = robotObject.transform;

            robotTransform.GetChild(0).GetComponent<Image>().sprite = robot.GetSprite();
            robotTransform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = robot.GetRarity() + "      |      " + robot.GetLevel();

            if (robot.targetedOre != null)
            {
                robotTransform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = "Robot mining: " + robot.targetedOre.name;
            }
            else
            {
                robotTransform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = "Robot not mining";
            }

            robotObject.GetComponent<InventoryButtons>().whichRobot = i;
            i++;
        }
    }

    public void RemoveRobots()
    {
        foreach (Transform robot in whereInstantiateRobots)
        {
            Destroy(robot.gameObject);
        }
    }
}
