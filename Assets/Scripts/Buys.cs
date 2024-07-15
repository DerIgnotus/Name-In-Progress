using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buys : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void BuyOven()
    {
        gameManager.AddOven();
        gameManager.updateShop = true;
    }

    public void BuyRobot()
    {
        gameManager.AddRobot();
        gameManager.updateShop = true;
    }

    public void BuyAutoMiner()
    {
        gameManager.AddAutoMiner();
        gameManager.updateShop = true;
    }
}
