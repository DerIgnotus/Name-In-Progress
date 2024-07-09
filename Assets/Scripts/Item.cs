using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private int itemCount;
    [SerializeField] private int itemValue;
    [SerializeField] private int maxItemCount;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private string itemDescription;

    public string GetItemName()
    {
        return itemName;
    }

    public int GetItemCount()
    {
        return itemCount;
    }

    public void SetItemCount(int count)
    {
        itemCount = count;
    }

    public int GetItemValue()
    {
        return itemValue;
    }

    public int GetMaxItemCount()
    {
        return maxItemCount;
    }

    public Sprite GetItemSprite()
    {
        return itemSprite;
    }

    public string GetItemDescription()
    {
        return itemDescription;
    }
}
