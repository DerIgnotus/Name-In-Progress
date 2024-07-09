using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform moneyText;
    [SerializeField] private Transform questPrefab;
    [SerializeField] private Transform whereInstantiateQuests;
    [SerializeField] private Transform questTransform;
    [SerializeField] private Transform questTexts;
    [SerializeField] private Transform robotsDisplay;

    [SerializeField] private Inventory inventory;

    public int money { get; private set; }
    public NPC currentNPC { get; set; }
    public List<Quest> quests { get; set; } = new List<Quest>();
    public List<NPC> npcs { get; set; } = new List<NPC>();
    public List<Robot> robots { get; set; } = new List<Robot>();
    public Transform npcsParent { get; set; }
    public Transform currentOre { get; set; }

    private bool canMove = true;

    public UnityEvent updateQuests;
    public UnityEvent updateInventory;
    public UnityEvent updateOres;

    void Start()
    {
        npcsParent = GameObject.Find("NPCs").transform;

        canMove = true;

        updateQuests.AddListener(UpdateQuests);
        updateInventory.AddListener(inventory.UpdateItems);
        updateOres.AddListener(inventory.UpdateOres);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!currentNPC) return;
    }

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

    public Transform GetInventory()
    {
        return inventory.GetInventoryTransform();
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

    public void DisplayQuests()
    {
        int i = 0;

        questTransform.gameObject.SetActive(true);

        foreach (Quest quest in quests)
        {
            GameObject questObject = Instantiate(questPrefab.gameObject, whereInstantiateQuests);

            var questPlayerMined = quest.GetObjective().mine - npcs[i].mineMinusThis;
            var questRobotMined = quest.GetObjective().robotsMine - npcs[i].robotsMineMinusThis;

            var questText = questObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
            questText.text = quest.GetQuestName();

            if (quest.GetObjective().mine > 0)
            {
                var questText1 = Instantiate(questTexts.gameObject, questObject.transform);
                questText1.GetComponent<TMPro.TextMeshProUGUI>().text = "Mine " + quest.GetObjective().mine + " " + quest.GetObjective().whatToMine;
                questText1.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = questPlayerMined.ToString() + "/" + quest.GetObjective().mine;
            }
            if (quest.GetObjective().robotsMine > 0)
            {
                var questText2 = Instantiate(questTexts.gameObject, questObject.transform);
                questText2.GetComponent<TMPro.TextMeshProUGUI>().text = "Let Robots mine " + quest.GetObjective().robotsMine + " " + quest.GetObjective().whatRobotsMine;
                questText2.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = questRobotMined.ToString() + "/" + quest.GetObjective().robotsMine;
            }

            i++;
        }
    }

    public void RemoveQuests()
    {
        questTransform.gameObject.SetActive(false);

        foreach (Transform quest in whereInstantiateQuests)
        {
            Destroy(quest.gameObject);
        }
    }

    public void UpdateQuests()
    {
        if (questTransform.gameObject.activeSelf)
        {
            RemoveQuests();
            DisplayQuests();
        }
        else
        {
            RemoveQuests();
        }
    }

    public void UpdateRobotDisplay()
    {
        if (robotsDisplay.gameObject.activeSelf)
        {
            inventory.RemoveRobots();
            DisplayRobots();
        }
        else
        {
            inventory.RemoveRobots();
        }
    }

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

    public void AddOre(OreScriptableObject ore, int count)
    {
        inventory.AddOre(ore, count);
    }

    public bool HasOre(string oreName, int count)
    {
        return inventory.HasOre(oreName, count);
    }

    public void RemoveOre(string oreName, int count)
    {
        inventory.RemoveOre(oreName, count);
    }

    public void DisplayOres()
    {
        inventory.DisplayOres();
    }

    public void RemoveOres()
    {
        inventory.RemoveOres();
    }

    public void SetInventory(bool active)
    {
        inventory.GetInventoryTransform().gameObject.SetActive(active);
    }

    public void DisplayRobots()
    {
        inventory.DisplayRobots(robots);
    }

    public void SetRobotsDisplay(bool active)
    {
        robotsDisplay.gameObject.SetActive(active);
    }

    public GameObject GetRobotsDisplay()
    {
        return robotsDisplay.gameObject;
    }
}

