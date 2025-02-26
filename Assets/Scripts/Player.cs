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
    private bool ovenOpen = false;

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
            else if (LookingAt().tag == "Oven")
            {
                LookingAtOven(LookingAt());
            }
            else if (LookingAt().tag == "Shop")
            {
                LookingAtShop(LookingAt());
            }
            else if (LookingAt().tag == "Npc")
            {
                LookingAtNPC(LookingAt());
            }
            else if (LookingAt().tag == "UpgradeStation")
            {
                LookingAtUpgradeStation();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressedEscape();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gameManager.AddMoney(50);
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
            gameManager.SetOreDisplay(false);

            gameManager.UpdateAutoMinerDisplay();
            gameManager.UpdateRobotDisplay();

            gameManager.SetCanMove(true);
        }
        else
        {
            gameManager.SetOreDisplay(true);

            gameManager.UpdateAutoMinerDisplay();
            gameManager.UpdateRobotDisplay();

            gameManager.SetCanMove(false);
        }
    }

    private void PressedEscape()
    {
        //if (gameManager.GetCanMove()) gameManager.SetCanMove(false);
        //else gameManager.SetCanMove(true);

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameManager.lookAroundRightClick = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameManager.lookAroundRightClick = false;
        }
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
                    gameManager.currentOven = null;

                    LookingAtOre();

                    return hit.transform;
                case "Npc":
                    mineText.gameObject.SetActive(false);
                    ovenText.gameObject.SetActive(false);
                    npcText.gameObject.SetActive(true);

                    gameManager.currentOven = null;

                    //LookingAtNPC(hit.transform);

                    return hit.transform;
                case "Oven":
                    mineText.gameObject.SetActive(false);
                    npcText.gameObject.SetActive(false);
                    ovenText.gameObject.SetActive(true);

                    //LookingAtOven(hit.transform);

                    return hit.transform;
                case "Shop":
                    mineText.gameObject.SetActive(false);
                    npcText.gameObject.SetActive(false);
                    ovenText.gameObject.SetActive(true);

                    gameManager.currentOven = null;

                    //LookingAtShop(hit.transform);

                    return hit.transform;
                case "UpgradeStation":
                    mineText.gameObject.SetActive(false);
                    npcText.gameObject.SetActive(false);
                    ovenText.gameObject.SetActive(true);

                    gameManager.currentOven = null;

                    return hit.transform;
                default:
                    ore = null;
                    mineText.gameObject.SetActive(false);
                    npcText.gameObject.SetActive(false);
                    ovenText.gameObject.SetActive(false);

                    gameManager.currentOven = null;

                    return null;
            }
        }
        else
        {
            gameManager.currentOven = null;
            AllUIOff();
            return null;
        }
    }

    private void LookingAtOven(Transform oven)
    {
        gameManager.currentOven = oven;

        if (gameManager.GetInventory().gameObject.activeSelf)
        {
            if (ovenOpen)
            {
                gameManager.SetInventory(false);
                ovenOpen = false;
                return;
            }

            if (gameManager.GetInventory().transform.GetChild(0).gameObject.activeSelf)
            {
                gameManager.RemoveInventory();

                gameManager.RemoveOres();
                gameManager.DisplayOres();
                ovenOpen = true;
            }
            else if (gameManager.GetInventory().transform.GetChild(1).gameObject.activeSelf)
            {
                gameManager.RemoveOres();
                gameManager.DisplayOres();
                ovenOpen = true;
            }
        }
        else
        {
            gameManager.SetInventory(true);

            gameManager.RemoveOres();
            gameManager.DisplayOres();
            ovenOpen = true;
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
        if (!gameManager.GetCanMove()) return;


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

    private void LookingAtNPC(Transform hit)
    {
        bgTransform.gameObject.SetActive(true);
        gameManager.currentNPC = hit.GetComponent<NPC>();
        gameManager.ResetButtons(bgTransform);
        gameManager.SetCanMove(false);

    }

    private void LookingAtShop(Transform hit)
    {
        gameManager.SetCanMove(!gameManager.GetCanMove());

        hit.GetComponent<Shop>().ShopThings();
    }

    private void Mine(Transform ore)
    {
        Ore oreScript = ore.GetComponent<Ore>();
        if (oreScript.GetMiningLevel() <= tool.GetMiningLevel())
        {
            oreScript.Harvest(1 * tool.GetHarvestDamage());
        }
    }

    private void LookingAtUpgradeStation()
    {
        gameManager.OpenCloseUpgradeStation();
    }
}
