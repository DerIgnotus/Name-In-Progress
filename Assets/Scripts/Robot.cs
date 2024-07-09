using System.Collections;
using System.Collections.Generic;
using Invector;
using UnityEngine;

public class Robot : MonoBehaviour
{
    // Stats
    [SerializeField] private float harvestSpeed;
    [SerializeField] private float harvestAmount;
    [SerializeField] private float harvestDamage;
    [SerializeField] private float miningLevel;
    [SerializeField] private float energyMax;

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

    public Transform targetedOre { get; set; }

    private float circleSpeed;
    private float verticalSpeed;
    private float energy;
    private float currentHarvestTime;
    private float circleRadius;

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
    }

    void Update()
    {
        if (targetedOre != null)
        {
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
}