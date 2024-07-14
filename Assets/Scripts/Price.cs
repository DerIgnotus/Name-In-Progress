using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Price", menuName = "Price", order = 3)]
public class Price : ScriptableObject
{
    [SerializeField] private PriceData priceData;

    [Serializable]
    public struct PriceData
    {
        public int price;
        public ItemStruct[] items;
    }

    [Serializable]
    public struct ItemStruct
    {
        public Item item;
        public int amount;
    }

    public PriceData GetPriceData()
    {
        return priceData;
    }
}



