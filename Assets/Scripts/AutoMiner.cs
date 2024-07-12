using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMiner : MonoBehaviour
{
    [SerializeField] private Sprite autoMinerSprite;
    [SerializeField] private int autoMinerLevel;
    [SerializeField] private float autoMinerProductionSpeed;
    [SerializeField] private int autoMinerProductionRate;

    public Ore targetedOre { get; set; }

    private float autoMinerProductionTimer;
    //private GameManager gameManager;

    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        autoMinerProductionTimer = autoMinerProductionSpeed;
    }


    void Update()
    {
        autoMinerProductionTimer -= Time.deltaTime / 10 * autoMinerProductionSpeed;
        if (autoMinerProductionTimer <= 0)
        {
            if (!targetedOre) return;
            if (targetedOre.GetMiningLevel() > autoMinerLevel) return;

            targetedOre.HarvestOreOwnHarvestTimer(autoMinerProductionRate);

            autoMinerProductionTimer = autoMinerProductionSpeed;
        }
    }

    public int GetAutoMinerLevel()
    {
        return autoMinerLevel;
    }

    public float GetAutoMinerProductionSpeed()
    {
        return autoMinerProductionSpeed;
    }

    public int GetAutoMinerProductionRate()
    {
        return autoMinerProductionRate;
    }

    public Sprite GetSprite()
    {
        return autoMinerSprite;
    }
}
