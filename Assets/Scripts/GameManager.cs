using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform moneyText;
    [SerializeField] private Inventory inventory;

    public int money { get; private set; }
    public NPC currentNPC { get; set; }
    public List<Quest> quests { get; set; } = new List<Quest>();
    public List<NPC> npcs { get; set; } = new List<NPC>();
    public List<Robot> robots { get; set; } = new List<Robot>();
    public Transform npcsParent { get; set; }
    public Transform currentOre { get; set; }
    public Transform currentOven { get; set; }

    private bool canMove = true;

    public UnityEvent updateQuests;
    public UnityEvent updateInventory;
    public UnityEvent updateOres;

    void Start()
    {
        npcsParent = GameObject.Find("NPCs").transform;

        canMove = true;

        updateQuests.AddListener(inventory.UpdateQuests);
        updateInventory.AddListener(inventory.UpdateItems);
        updateOres.AddListener(inventory.UpdateOres);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!currentNPC) return;
    }



    // PLAYER //

    public void SetCanMove(bool canIMove)
    {
        if (canIMove)
        {
            canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            canMove = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void AddMoney(int money)
    {
        this.money += money;

        moneyText.GetComponent<TMPro.TextMeshProUGUI>().text = "Money: " + this.money;
    }



    // INVENTORY //

    public Transform GetInventory()
    {
        return inventory.GetInventoryTransform();
    }

    public void AddItem(Item item, int count)
    {
        inventory.AddItem(item, count);
    }

    public bool HasItem(string itemName, int count)
    {
        return inventory.HasItem(itemName, count);
    }

    public void RemoveItem(string itemName, int count)
    {
        inventory.RemoveItem(itemName, count);
    }

    public void DisplayInventory()
    {
        inventory.DisplayItems();
    }

    public void RemoveInventory()
    {
        inventory.RemoveItems();
    }

    public void SetInventory(bool active)
    {
        inventory.GetInventoryTransform().gameObject.SetActive(active);
    }




    // ORES //

    public void AddOre(OreScriptableObject ore, int count, Dictionary<string, OreScript> oresDict)
    {
        inventory.AddOre(ore, count, oresDict);
    }

    public bool HasOre(string oreName, int count, Dictionary<string, OreScript> oresDict)
    {
        return inventory.HasOre(oreName, count, oresDict);
    }

    public void RemoveOre(string oreName, int count, Dictionary<string, OreScript> oresDict)
    {
        inventory.RemoveOre(oreName, count, oresDict);
    }

    public void DisplayOres()
    {
        inventory.DisplayOres();
    }

    public void RemoveOres()
    {
        inventory.RemoveOres();
    }

    public Dictionary<string, OreScript> GetOres()
    {
        return inventory.GetOres();
    }



    // ROBOTS //

    public void DisplayRobots()
    {
        inventory.DisplayRobots(robots);
    }

    public void SetRobotsDisplay(bool active)
    {
        inventory.SetRobotsDisplay(active);
    }

    public GameObject GetRobotsDisplay()
    {
        return inventory.GetRobotsDisplay();
    }

    public void UpdateRobotDisplay()
    {
        inventory.UpdateRobotDisplay(robots);
    }



    // QUESTS //

    public void ResetButtons(Transform bg)
    {
        if (currentNPC.questInProgress)
        {
            bg.GetChild(1).gameObject.SetActive(true);
            bg.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            bg.GetChild(1).gameObject.SetActive(false);
            bg.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void RemoveQuests()
    {
        inventory.RemoveQuests();
    }

    public void DisplayQuests()
    {
        inventory.DisplayQuests();
    }
}

