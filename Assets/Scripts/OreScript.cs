using UnityEngine;


public class OreScript
{
    public OreScriptableObject OreScriptableObject { get; private set; }
    public int Count { get; set; }

    public OreScript(OreScriptableObject oreScriptableObject, int count)
    {
        OreScriptableObject = oreScriptableObject;
        Count = count;
    }
}
