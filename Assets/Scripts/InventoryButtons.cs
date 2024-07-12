using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtons : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public int whichRobot { get; set; }

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void OpenItems()
    {
        gameManager.RemoveOres();
        gameManager.RemoveInventory();
        gameManager.DisplayInventory();
    }

    public void OpenOres()
    {
        gameManager.RemoveInventory();
        gameManager.RemoveOres();
        gameManager.DisplayOres();
    }

    public void SetOreAsTarget()
    {
        Robot robot = gameManager.robots[whichRobot];

        robot.targetedOre = gameManager.currentOre;

        gameManager.UpdateRobotDisplay();
    }

    public void SmeltOre()
    {
        if (gameManager.currentOven == null) return;

        Oven oven = gameManager.currentOven.GetComponent<Oven>();
        OreScriptableObject ore = gameManager.GetOres()[transform.GetChild(1).name].OreScriptableObject;
        OreScript oreScript = gameManager.GetOres()[transform.GetChild(1).name];

        if (gameManager.HasOre(transform.GetChild(1).name, 1, oven.ores))
        {
            oven.AddOre(ore, oreScript.Count);
            gameManager.RemoveOre(transform.GetChild(1).name, oreScript.Count, gameManager.GetOres());
        }
        else if (oven.ores.Count < oven.GetMaxOres())
        {
            oven.AddOre(ore, oreScript.Count);
            gameManager.RemoveOre(transform.GetChild(1).name, oreScript.Count, gameManager.GetOres());
        }
    }
}
