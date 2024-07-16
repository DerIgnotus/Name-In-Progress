using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prices : MonoBehaviour
{
    [Header("Robot Upgrade Prices")]
    [SerializeField] private Price[] robotMiningSpeedPrices;
    [SerializeField] private Price[] robotMiningAmountPrices;
    [SerializeField] private Price[] robotMiningLevelPrices;
    [SerializeField] private Price[] robotMaxEnergyPrices;
    [SerializeField] private Price[] robotChargingSpeedPrices;
    [SerializeField] private Price[] robotMoveSpeedPrices;

    [Header("Auto Miner Upgrade Prices")]
    [SerializeField] private Price[] autoMinerProductionSpeedPrices;
    [SerializeField] private Price[] autoMinerProductionRatePrices;

    [Header("Robot Upgrade Outcomes")]
    [SerializeField] private float[] robotMiningSpeedOutcomes;
    [SerializeField] private int[] robotMiningAmountOutcomes;
    [SerializeField] private int[] robotMiningLevelOutcomes;
    [SerializeField] private int[] robotMaxEnergyOutcomes;
    [SerializeField] private float[] robotChargingSpeedOutcomes;
    [SerializeField] private float[] robotMoveSpeedOutcomes;

    [Header("Auto Miner Upgrade Outcomes")]
    [SerializeField] private float[] autoMinerProductionSpeedOutcomes;
    [SerializeField] private int[] autoMinerProductionRateOutcomes;



    public int GetRobotMiningSpeedPrice(int whichRobot)
    {
        return robotMiningSpeedPrices[whichRobot].GetPriceData().price;
    }

    public int GetRobotMiningAmountPrice(int whichRobot)
    {
        return robotMiningAmountPrices[whichRobot].GetPriceData().price;
    }

    public int GetRobotMiningLevelPrice(int whichRobot)
    {
        return robotMiningLevelPrices[whichRobot].GetPriceData().price;
    }

    public int GetRobotMaxEnergyPrice(int whichRobot)
    {
        return robotMaxEnergyPrices[whichRobot].GetPriceData().price;
    }

    public int GetRobotChargingSpeedPrice(int whichRobot)
    {
        return robotChargingSpeedPrices[whichRobot].GetPriceData().price;
    }

    public int GetRobotMoveSpeedPrice(int whichRobot)
    {
        return robotMoveSpeedPrices[whichRobot].GetPriceData().price;
    }



    public int GetAutoMinerProductionSpeedPrice(int whichAutoMiner)
    {
        return autoMinerProductionSpeedPrices[whichAutoMiner].GetPriceData().price;
    }

    public int GetAutoMinerProductionRatePrice(int whichAutoMiner)
    {
        return autoMinerProductionRatePrices[whichAutoMiner].GetPriceData().price;
    }



    public float GetRobotMiningSpeedOutcome(int whichRobot)
    {
        return robotMiningSpeedOutcomes[whichRobot];
    }

    public int GetRobotMiningAmountOutcome(int whichRobot)
    {
        return robotMiningAmountOutcomes[whichRobot];
    }

    public int GetRobotMiningLevelOutcome(int whichRobot)
    {
        return robotMiningLevelOutcomes[whichRobot];
    }

    public int GetRobotMaxEnergyOutcome(int whichRobot)
    {
        return robotMaxEnergyOutcomes[whichRobot];
    }

    public float GetRobotChargingSpeedOutcome(int whichRobot)
    {
        return robotChargingSpeedOutcomes[whichRobot];
    }

    public float GetRobotMoveSpeedOutcome(int whichRobot)
    {
        return robotMoveSpeedOutcomes[whichRobot];
    }


    public float GetAutoMinerProductionSpeedOutcome(int whichAutoMiner)
    {
        return autoMinerProductionSpeedOutcomes[whichAutoMiner];
    }

    public int GetAutoMinerProductionRateOutcome(int whichAutoMiner)
    {
        return autoMinerProductionRateOutcomes[whichAutoMiner];
    }
}
