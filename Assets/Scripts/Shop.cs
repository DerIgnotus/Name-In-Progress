using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Transform shop;

    private bool shopIsOpen = false;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.updateShop)
        {
            gameManager.updateShop = false;
            OpenShop();
        }
    }

    public void ShopThings()
    {
        if (shopIsOpen)
        {
            shopIsOpen = false;
            CloseShop();
        }
        else
        {
            shopIsOpen = true;
            OpenShop();
        }
    }

    private void OpenShop()
    {
        shop.gameObject.SetActive(true);

        var button1 = shop.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        var button2 = shop.GetChild(0).GetChild(0).GetChild(0).GetChild(1);

        string text1 = "Buy Robot:\nMoney: " + gameManager.GetRobotPrice().GetPriceData().price;
        string text2 = "Buy Oven:\nMoney: " + gameManager.GetOvenPrice().GetPriceData().price;

        foreach (Price.ItemStruct itemStruct in gameManager.GetRobotPrice().GetPriceData().items)
        {
            text1 += $"\n{itemStruct.item.GetItemName()}: {itemStruct.amount}";
        }

        foreach (Price.ItemStruct itemStruct in gameManager.GetOvenPrice().GetPriceData().items)
        {
            text2 += $"\n{itemStruct.item.GetItemName()}: {itemStruct.amount}";
        }

        button1.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = text1;
        button2.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = text2;
    }

    private void CloseShop()
    {
        shop.gameObject.SetActive(false);
    }
}
