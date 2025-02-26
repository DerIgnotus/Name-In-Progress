using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ore", menuName = "Ore", order = 2)]
public class OreScriptableObject : ScriptableObject
{
    [SerializeField] private string oreName;
    [SerializeField] private int oreCount;
    [SerializeField] private int oreValue;
    [SerializeField] private int maxOreCount;
    [SerializeField] private Sprite oreSprite;
    [SerializeField] private string oreDescription;
    [SerializeField] private float smeltTime;
    [SerializeField] private float initialSmeltTime;


    public string GetOreName()
    {
        return oreName;
    }

    public int GetOreCount()
    {
        return oreCount;
    }

    public void SetOreCount(int count)
    {
        oreCount = count;
    }

    public int GetOreValue()
    {
        return oreValue;
    }

    public int GetMaxOreCount()
    {
        return maxOreCount;
    }

    public Sprite GetOreSprite()
    {
        return oreSprite;
    }

    public string GetOreDescription()
    {
        return oreDescription;
    }

    public float GetSmeltTime()
    {
        return smeltTime;
    }

    public void SetSmeltTime(float time)
    {
        smeltTime = time;
    }

    public float GetInitialSmeltTime()
    {
        return initialSmeltTime;
    }
}
