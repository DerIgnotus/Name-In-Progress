using UnityEngine;


public class ItemScript
{
    public Item Item { get; private set; }
    public int Count { get; set; }

    public ItemScript(Item item, int count)
    {
        Item = item;
        Count = count;
    }
}
