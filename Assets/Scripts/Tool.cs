using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] private float harvestSpeed;
    [SerializeField] private float harvestAmount;
    [SerializeField] private float harvestDamage;
    [SerializeField] private float miningLevel;

    public float GetHarvestSpeed()
    {
        return harvestSpeed;
    }

    public float GetHarvestAmount()
    {
        return harvestAmount;
    }

    public float GetHarvestDamage()
    {
        return harvestDamage;
    }

    public float GetMiningLevel()
    {
        return miningLevel;
    }
}
