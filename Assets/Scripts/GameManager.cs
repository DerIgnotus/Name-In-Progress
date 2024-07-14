using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform moneyText;
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject autoMinerPrefab;
    [SerializeField] private GameObject ovenPrefab;
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private List<Price> autoMinerPrices;
    [SerializeField] private List<Price> ovenPrices;
    [SerializeField] private List<Price> robotPrices;

    public int money { get; private set; }
    public NPC currentNPC { get; set; }
    public List<Quest> quests { get; set; } = new List<Quest>();
    public List<NPC> npcs { get; set; } = new List<NPC>();
    public List<Robot> robots { get; set; } = new List<Robot>();
    public List<AutoMiner> autoMiners { get; set; } = new List<AutoMiner>();
    public List<Oven> ovens { get; set; } = new List<Oven>();
    public Transform npcsParent { get; set; }
    public Transform currentOre { get; set; }
    public Transform currentOven { get; set; }
    public int maxAutoMiners { get; set; } = 3;
    private int currentAutoMiners = 0;
    private int currentOvens = 0;
    private int currentRobots = 0;
    public bool updateShop { get; set; } = false;
    public bool lookAroundRightClick { get; set; } = false;


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

        AddOven();

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

            if (lookAroundRightClick) return;

            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            canMove = false;

            if (lookAroundRightClick) return;

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



    // ORES //

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

    public void RemoveRobots()
    {
        inventory.RemoveRobots();
    }

    public void DisplayAutoMiners()
    {
        inventory.DisplayAutoMiners(autoMiners);
    }

    public void RemoveAutoMiners()
    {
        inventory.RemoveAutoMiners();
    }

    public void UpdateAutoMinerDisplay()
    {
        inventory.UpdateAutoMinerDisplay(autoMiners);
    }

    public Price GetAutoMinerPrice()
    {
        return autoMinerPrices[currentAutoMiners];
    }

    public Price GetRobotPrice()
    {
        return robotPrices[currentRobots];
    }

    public Price GetOvenPrice()
    {
        return ovenPrices[currentOvens];
    }

    public void AddAutoMiner()
    {
        if (CanBuy(GetAutoMinerPrice()))
        {
            Buy(GetAutoMinerPrice());
            AutoMiner autoMiner = Instantiate(autoMinerPrefab, GameObject.Find("AutoMiners").transform.GetChild(currentAutoMiners)).GetComponent<AutoMiner>();
            autoMiners.Add(autoMiner);

            currentAutoMiners++;
        }
    }

    public void AddRobot()
    {
        if (CanBuy(GetRobotPrice()))
        {
            Buy(GetRobotPrice());
            Robot robot = Instantiate(robotPrefab, GameObject.Find("Robots").transform.GetChild(currentRobots)).GetComponent<Robot>();
            robots.Add(robot);

            currentRobots++;
        }
    }

    public void SetOreDisplay(bool active)
    {
        inventory.SetOreDisplay(active);
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


    // BUY //

    public bool CanBuy(Price price)
    {
        if (money < price.GetPriceData().price)
        {
            Debug.Log("Not enough money");
            return false;
        }

        foreach (Price.ItemStruct item in price.GetPriceData().items)
        {
            if (!inventory.HasItem(item.item.GetItemName(), item.amount))
            {
                Debug.Log("Not enough items");
                return false;
            }
        }

        return true;
    }

    public void Buy(Price price)
    {
        AddMoney(-price.GetPriceData().price);

        foreach (Price.ItemStruct item in price.GetPriceData().items)
        {
            inventory.RemoveItem(item.item.GetItemName(), item.amount);
        }
    }

    public void AddOven()
    {
        if (CanBuy(GetOvenPrice()))
        {
            Buy(GetOvenPrice());
            Oven oven = Instantiate(ovenPrefab, GameObject.Find("Ovens").transform.GetChild(currentOvens)).GetComponent<Oven>();
            ovens.Add(oven);

            currentOvens++;
        }
    }
}

