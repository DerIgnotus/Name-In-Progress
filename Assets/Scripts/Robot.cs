using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    // Stats
    [SerializeField] private float harvestSpeed;
    [SerializeField] private float harvestAmount;
    [SerializeField] private float harvestDamage;
    [SerializeField] private float miningLevel;
    [SerializeField] private float energyMax;
    [SerializeField] private float chargingSpeed;
    [SerializeField] private float moveSpeed;

    [SerializeField] private Sprite icon;
    [SerializeField] private string rarity;
    [SerializeField] private int level;

    //[SerializeField] private Transform[] ores;
    [SerializeField] private float circleRadiusMin = 1.25f;
    [SerializeField] private float circleRadiusMax = 2.5f;
    [SerializeField] private float circleSpeedMax = 2.25f;
    [SerializeField] private float circleSpeedMin = 1f;
    [SerializeField] private float verticalSpeedMax = 2.25f;
    [SerializeField] private float verticalSpeedMin = 1f;
    [SerializeField] private float verticalAmplitude = 0.3f;

    public Transform targetedOre { get; private set; }

    public int harvestSpeedUpgradeLevel { get; set; }
    public int harvestAmountUpgradeLevel { get; set; }
    public int miningLevelUpgradeLevel { get; set; }
    public int energyMaxUpgradeLevel { get; set; }
    public int chargingSpeedUpgradeLevel { get; set; }
    public int moveSpeedUpgradeLevel { get; set; }

    private float circleSpeed;
    private float verticalSpeed;
    private float energy;
    private float currentHarvestTime;
    private float circleRadius;
    private bool charging = false;
    private bool reachedOre = false;

    void Start()
    {
        /*
        var oresTransform = GameObject.Find("Ores").transform;
        ores = new Transform[oresTransform.childCount];

        for (int i = 0; i < oresTransform.childCount; i++)
        {
            ores[i] = oresTransform.GetChild(i);
        }

        targetedOre = ores[Random.Range(0, ores.Length)];
        */

        circleRadius = Random.Range(circleRadiusMin, circleRadiusMax);
        circleSpeed = Random.Range(circleSpeedMin, circleSpeedMax);
        verticalSpeed = Random.Range(verticalSpeedMin, verticalSpeedMax);

        energy = energyMax;
    }

    void Update()
    {
        if (charging)
        {
            Charge();
            return;
        }

        if (targetedOre == null) return;

        if (!reachedOre)
        {
            if (Vector3.Distance(transform.position, targetedOre.position) > 1.5f)
            {
                Vector3 directionToOre = (targetedOre.position - transform.position).normalized;

                transform.position += directionToOre * moveSpeed * Time.deltaTime;
                return;
            }
        }

        reachedOre = true;

        CircleAroundOre();

        if (currentHarvestTime > 0)
        {
            currentHarvestTime -= Time.deltaTime;
        }
        else
        {
            TryToMineOre();
            currentHarvestTime = harvestSpeed;
        }

        energy -= Time.deltaTime;

        if (energy <= 0)
        {
            energy = 0;
            charging = true;
        }
    }

    private void CircleAroundOre()
    {
        float x = Mathf.Cos(Time.time * circleSpeed) * circleRadius;
        float z = Mathf.Sin(Time.time * circleSpeed) * circleRadius;
        float y = Mathf.Sin(Time.time * verticalSpeed) * verticalAmplitude - verticalAmplitude;
        Vector3 nextPosition = new Vector3(x, y, z) + targetedOre.position;
        nextPosition.y += targetedOre.position.y + verticalAmplitude;
        transform.position = nextPosition;
    }

    private void TryToMineOre()
    {
        Ore oreScript = targetedOre.GetComponent<Ore>();

        if (!oreScript) return;

        if (oreScript.GetMiningLevel() <= miningLevel && oreScript.CanMine())
        {
            MineOre(oreScript);
        }
    }

    private void Charge()
    {
        charging = true;

        if (Vector3.Distance(transform.position, transform.parent.position) > 0.1f)
        {
            Vector3 directionToCharger = (transform.parent.position - transform.position).normalized;

            transform.position += directionToCharger * moveSpeed * Time.deltaTime;
            return;
        }

        if (energy < energyMax)
        {
            energy += chargingSpeed * Time.deltaTime;
        }
        else
        {
            energy = energyMax;
            charging = false;
            reachedOre = false;
        }
    }

    private void MineOre(Ore oreScript)
    {
        oreScript.HarvestedByRobot(1 * harvestDamage);
    }

    public Sprite GetSprite()
    {
        return icon;
    }

    public string GetRarity()
    {
        return rarity;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetTargetedOre(Transform ore)
    {
        reachedOre = false;
        targetedOre = ore;
    }

    public float GetHarvestSpeed()
    {
        return harvestSpeed;
    }

    public float GetHarvestAmount()
    {
        return harvestAmount;
    }
    public float GetMiningLevel()
    {
        return miningLevel;
    }

    public float GetMaxEnergy()
    {
        return energyMax;
    }

    public float GetChargingSpeed()
    {
        return chargingSpeed;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetHarvestSpeed(float value)
    {
        harvestSpeed = value;
    }

    public void SetHarvestAmount(float value)
    {
        harvestAmount = value;
    }

    public void SetMiningLevel(float value)
    {
        miningLevel = value;
    }

    public void SetMaxEnergy(float value)
    {
        energyMax = value;
    }

    public void SetChargingSpeed(float value)
    {
        chargingSpeed = value;
    }

    public void SetMoveSpeed(float value)
    {
        moveSpeed = value;
    }
}