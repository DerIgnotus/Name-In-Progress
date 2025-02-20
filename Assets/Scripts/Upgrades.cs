using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private GameObject equipmentBuy;
    [SerializeField] private Price equipmentPrice;

    public int whichRobot { get; set; }
    public int whichAutoMiner { get; set; }

    private bool isBought = false;

    private GameManager gameManager;
    private Prices prices;
    private GameObject upgradePanel;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        prices = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Prices>();
        upgradePanel = GameObject.Find("Panel");
        upgradePanel = gameManager.GetUpgradePanel();
    }

    public void SetCurrentRobot()
    {
        gameManager.whichRobot = whichRobot;

        UpgradeRobot();
    }

    public void SetCurrentAutoMiner()
    {
        gameManager.whichAutoMiner = whichAutoMiner;

        UpgradeAutoMiner();
    }

    public void UpgradeRobot()
    {
        foreach (Transform transform in upgradePanel.transform)
        {
            Destroy(transform.gameObject);
        }

        upgradePanel.SetActive(true);

        Robot robot = gameManager.robots[gameManager.whichRobot];
        GameObject robotUpgradePrefab = gameManager.GetRobotUpgradeButtons();
        GameObject robotObject = Instantiate(robotUpgradePrefab.gameObject, upgradePanel.transform);
        Transform robotTransform = robotObject.transform;

        robotTransform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = robot.GetRarity() + "      |      " + robot.GetLevel();
        robotTransform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "Mining Speed: " + robot.GetHarvestSpeed();
        robotTransform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = "Mining Amount: " + robot.GetHarvestAmount();
        robotTransform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Mining Level: " + robot.GetMiningLevel();
        robotTransform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>().text = "Movement Speed: " + robot.GetMoveSpeed();
        robotTransform.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = "Max Energy: " + robot.GetMaxEnergy();
        robotTransform.GetChild(6).GetComponent<TMPro.TextMeshProUGUI>().text = "Charging Speed: " + robot.GetChargingSpeed();

        robotTransform.GetChild(7).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Cost: " + prices.GetRobotMiningSpeedPrice(robot.harvestSpeedUpgradeLevel);
        robotTransform.GetChild(8).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Cost: " + prices.GetRobotMiningAmountPrice(robot.harvestAmountUpgradeLevel);
        robotTransform.GetChild(9).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Cost: " + prices.GetRobotMiningLevelPrice(robot.miningLevelUpgradeLevel);
        robotTransform.GetChild(10).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Cost: " + prices.GetRobotMaxEnergyPrice(robot.energyMaxUpgradeLevel);
        robotTransform.GetChild(11).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Cost: " + prices.GetRobotChargingSpeedPrice(robot.chargingSpeedUpgradeLevel);
        robotTransform.GetChild(12).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Cost: " + prices.GetRobotMoveSpeedPrice(robot.moveSpeedUpgradeLevel);
    }

    public void UpgradeAutoMiner()
    {
        foreach (Transform transform in upgradePanel.transform)
        {
            Destroy(transform.gameObject);
        }

        upgradePanel.SetActive(true);

        AutoMiner autoMiner = gameManager.autoMiners[gameManager.whichAutoMiner];
        GameObject autoMinerUpgradePrefab = gameManager.GetAutoMinerUpgradeButtons();
        GameObject autoMinerObject = Instantiate(autoMinerUpgradePrefab.gameObject, upgradePanel.transform);
        Transform autoMinerTransform = autoMinerObject.transform;

        autoMinerTransform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Level: " + autoMiner.GetAutoMinerLevel().ToString();
        autoMinerTransform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "Production Speed: " + autoMiner.GetAutoMinerProductionSpeed().ToString();
        autoMinerTransform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = "Prouction Amount: " + autoMiner.GetAutoMinerProductionRate().ToString();

        autoMinerTransform.GetChild(3).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Cost: " + prices.GetAutoMinerProductionSpeedPrice(gameManager.autoMiners[whichAutoMiner].whichProductionSpeedLevel);
        autoMinerTransform.GetChild(4).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Cost: " + prices.GetAutoMinerProductionRatePrice(gameManager.autoMiners[whichAutoMiner].whichProductionRateLevel);
    }

    public void UpgradeRobotMiningSpeed()
    {
        Robot currentRobot = gameManager.robots[gameManager.whichRobot];

        if (!gameManager.OnlyMoneyCanBuy(prices.GetRobotMiningSpeedPrice(currentRobot.harvestSpeedUpgradeLevel))) return;

        gameManager.OnlyMoneyBuy(prices.GetRobotMiningSpeedPrice(currentRobot.harvestSpeedUpgradeLevel));

        currentRobot.SetHarvestSpeed(prices.GetRobotMiningSpeedOutcome(currentRobot.harvestSpeedUpgradeLevel));

        currentRobot.harvestSpeedUpgradeLevel++;

        gameManager.OpenRobotUpgrades();
        UpgradeRobot();
    }

    public void UpgradeRobotMiningAmount()
    {
        Robot currentRobot = gameManager.robots[gameManager.whichRobot];

        if (!gameManager.OnlyMoneyCanBuy(prices.GetRobotMiningAmountPrice(currentRobot.harvestAmountUpgradeLevel))) return;

        gameManager.OnlyMoneyBuy(prices.GetRobotMiningAmountPrice(currentRobot.harvestAmountUpgradeLevel));

        currentRobot.SetHarvestAmount(prices.GetRobotMiningAmountOutcome(currentRobot.harvestAmountUpgradeLevel));

        currentRobot.harvestAmountUpgradeLevel++;

        gameManager.OpenRobotUpgrades();
        UpgradeRobot();
    }

    public void UpgradeRobotMiningLevel()
    {
        Robot currentRobot = gameManager.robots[gameManager.whichRobot];

        if (!gameManager.OnlyMoneyCanBuy(prices.GetRobotMiningLevelPrice(currentRobot.miningLevelUpgradeLevel))) return;

        gameManager.OnlyMoneyBuy(prices.GetRobotMiningLevelPrice(currentRobot.miningLevelUpgradeLevel));

        currentRobot.SetMiningLevel(prices.GetRobotMiningLevelOutcome(currentRobot.miningLevelUpgradeLevel));

        currentRobot.miningLevelUpgradeLevel++;

        gameManager.OpenRobotUpgrades();
        UpgradeRobot();
    }

    public void UpgradeRobotMaxEnergy()
    {
        Robot currentRobot = gameManager.robots[gameManager.whichRobot];

        if (!gameManager.OnlyMoneyCanBuy(prices.GetRobotMaxEnergyPrice(currentRobot.energyMaxUpgradeLevel))) return;

        gameManager.OnlyMoneyBuy(prices.GetRobotMaxEnergyPrice(currentRobot.energyMaxUpgradeLevel));

        currentRobot.SetMaxEnergy(prices.GetRobotMaxEnergyOutcome(currentRobot.energyMaxUpgradeLevel));

        currentRobot.energyMaxUpgradeLevel++;

        gameManager.OpenRobotUpgrades();
        UpgradeRobot();
    }

    public void UpgradeRobotChargingSpeed()
    {
        Robot currentRobot = gameManager.robots[gameManager.whichRobot];

        if (!gameManager.OnlyMoneyCanBuy(prices.GetRobotChargingSpeedPrice(currentRobot.chargingSpeedUpgradeLevel))) return;

        gameManager.OnlyMoneyBuy(prices.GetRobotChargingSpeedPrice(currentRobot.chargingSpeedUpgradeLevel));

        currentRobot.SetChargingSpeed(prices.GetRobotChargingSpeedOutcome(currentRobot.chargingSpeedUpgradeLevel));

        currentRobot.chargingSpeedUpgradeLevel++;

        gameManager.OpenRobotUpgrades();
        UpgradeRobot();
    }

    public void UpgradeRobotMoveSpeed()
    {
        Robot currentRobot = gameManager.robots[gameManager.whichRobot];

        if (!gameManager.OnlyMoneyCanBuy(prices.GetRobotMoveSpeedPrice(currentRobot.moveSpeedUpgradeLevel))) return;

        gameManager.OnlyMoneyBuy(prices.GetRobotMoveSpeedPrice(currentRobot.moveSpeedUpgradeLevel));

        currentRobot.SetMoveSpeed(prices.GetRobotMoveSpeedOutcome(currentRobot.moveSpeedUpgradeLevel));

        currentRobot.moveSpeedUpgradeLevel++;

        gameManager.OpenRobotUpgrades();
        UpgradeRobot();
    }

    public void UpgradeAutoMinerProductionSpeed()
    {
        AutoMiner currentAutoMiner = gameManager.autoMiners[gameManager.whichAutoMiner];

        if (!gameManager.OnlyMoneyCanBuy(prices.GetAutoMinerProductionSpeedPrice(currentAutoMiner.whichProductionSpeedLevel))) return;

        gameManager.OnlyMoneyBuy(prices.GetAutoMinerProductionSpeedPrice(currentAutoMiner.whichProductionSpeedLevel));

        currentAutoMiner.SetProductionSpeed(prices.GetAutoMinerProductionSpeedOutcome(currentAutoMiner.whichProductionSpeedLevel));

        currentAutoMiner.whichProductionSpeedLevel++;

        gameManager.OpenAutoMinerUpgrades();
        UpgradeAutoMiner();
    }

    public void UpgradeAutoMinerProductionRate()
    {
        AutoMiner currentAutoMiner = gameManager.autoMiners[gameManager.whichAutoMiner];

        if (!gameManager.OnlyMoneyCanBuy(prices.GetAutoMinerProductionRatePrice(currentAutoMiner.whichProductionRateLevel))) return;

        gameManager.OnlyMoneyBuy(prices.GetAutoMinerProductionRatePrice(currentAutoMiner.whichProductionRateLevel));

        currentAutoMiner.SetProductionRate(prices.GetAutoMinerProductionRateOutcome(currentAutoMiner.whichProductionRateLevel));

        currentAutoMiner.whichProductionRateLevel++;

        gameManager.OpenAutoMinerUpgrades();
        UpgradeAutoMiner();
    }

    public void BuyEquipment()
    {
        if (isBought)
        {
            gameManager.Equip(equipmentBuy);
            ChangeToGreen();
            return;
        }

        if (!gameManager.CanBuy(equipmentPrice)) return;

        gameManager.Buy(equipmentPrice);

        isBought = true;

        gameManager.Equip(equipmentBuy);

        ChangeToGreen();
    }

    private void ChangeToGreen()
    {
        Button button = GetComponent<Button>(); // Get the Button component
        ColorBlock cb = button.colors; // Get the current ColorBlock

        cb.highlightedColor = Color.green; // Change the highlighted color to green

        button.colors = cb;
    }
}
