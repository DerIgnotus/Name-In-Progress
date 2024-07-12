using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform mineText;
    [SerializeField] private Transform npcText;
    [SerializeField] private Transform ovenText;
    [SerializeField] private Transform bgTransform;

    [SerializeField] private GameObject robotPrefab;

    private GameManager gameManager;
    private Tool tool;
    private float currentMiningTime;
    private Transform ore;
    private bool dispalyingQuests = false;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        LookingAt();
        Controlls();
    }

    private void Controlls()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OpenQuests();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            InstantiateRobot();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (LookingAt() == null) OpenInventory();
            else if (LookingAt().tag == "Ore")
            {
                OpenOreManagement();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressedEscape();
        }
    }

    private void InstantiateRobot()
    {
        Transform whereToInstantiate = GameObject.Find("Robots").transform;

        Robot robot = Instantiate(robotPrefab, whereToInstantiate).GetComponent<Robot>();
        gameManager.robots.Add(robot);
    }

    private void OpenOreManagement()
    {
        gameManager.GetInventory().gameObject.SetActive(false);

        if (gameManager.GetRobotsDisplay().activeSelf)
        {
            gameManager.GetRobotsDisplay().SetActive(false);
            gameManager.SetCanMove(true);
        }
        else
        {
            gameManager.GetRobotsDisplay().SetActive(true);
            gameManager.DisplayRobots();
            gameManager.SetCanMove(false);
        }
    }

    private void PressedEscape()
    {
        if (gameManager.GetCanMove()) gameManager.SetCanMove(false);
        else gameManager.SetCanMove(true);
    }

    private void OpenInventory()
    {
        if (gameManager.GetInventory().gameObject.activeSelf)
        {
            gameManager.SetInventory(false);
        }
        else
        {
            gameManager.SetInventory(true);
        }
    }

    private void OpenQuests()
    {
        if (dispalyingQuests)
        {
            dispalyingQuests = false;
            gameManager.RemoveQuests();
        }
        else
        {
            dispalyingQuests = true;
            gameManager.DisplayQuests();
        }
    }

    private Transform LookingAt()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.GetChild(2).position, transform.GetChild(2).forward, out hit, 2f))
        {
            switch (hit.collider.tag)
            {
                case "Ore":
                    npcText.gameObject.SetActive(false);
                    ovenText.gameObject.SetActive(false);
                    mineText.gameObject.SetActive(true);

                    ore = hit.collider.transform;
                    gameManager.currentOre = ore;

                    LookingAtOre();

                    return hit.transform;
                case "Npc":
                    mineText.gameObject.SetActive(false);
                    ovenText.gameObject.SetActive(false);
                    npcText.gameObject.SetActive(true);

                    LookingAtNPC(hit);

                    return hit.transform;
                case "Oven":
                    mineText.gameObject.SetActive(false);
                    npcText.gameObject.SetActive(false);
                    ovenText.gameObject.SetActive(true);

                    LookingAtOven(hit.transform);

                    return hit.transform;
                default:
                    ore = null;
                    mineText.gameObject.SetActive(false);
                    npcText.gameObject.SetActive(false);
                    ovenText.gameObject.SetActive(false);

                    return null;
            }
        }
        else
        {
            AllUIOff();
            return null;
        }
    }

    private void LookingAtOven(Transform oven)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameManager.currentOven = oven;

            if (gameManager.GetInventory().gameObject.activeSelf)
            {
                gameManager.SetInventory(false);

                gameManager.DisplayInventory();
                gameManager.RemoveOres();

                gameManager.SetCanMove(true);
            }
            else
            {
                gameManager.SetInventory(true);

                gameManager.RemoveInventory();
                gameManager.DisplayOres();

                gameManager.SetCanMove(false);
            }
        }
    }

    private void AllUIOff()
    {
        mineText.gameObject.SetActive(false);
        npcText.gameObject.SetActive(false);
        ovenText.gameObject.SetActive(false);
    }

    private void LookingAtOre()
    {
        tool = GameObject.FindGameObjectWithTag("Tool").GetComponent<Tool>();
        if (!tool) return;

        if (currentMiningTime <= 0)
        {
            if (Input.GetMouseButtonDown(0) && ore.GetComponent<Ore>().CanMine())
            {
                Mine(ore);
                currentMiningTime = tool.GetHarvestSpeed();
            }
        }
        else currentMiningTime -= Time.deltaTime;
    }

    private void LookingAtNPC(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bgTransform.gameObject.SetActive(true);
            gameManager.currentNPC = hit.collider.GetComponent<NPC>();
            gameManager.ResetButtons(bgTransform);
            gameManager.SetCanMove(false);
        }
    }

    private void Mine(Transform ore)
    {
        Ore oreScript = ore.GetComponent<Ore>();
        if (oreScript.GetMiningLevel() <= tool.GetMiningLevel())
        {
            oreScript.Harvest(1 * tool.GetHarvestDamage());
        }
    }
}
