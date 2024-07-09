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
}
