using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Quest[] quest;

    public bool questInProgress { get; private set; } = false;

    private int currentQuest = 0;
    private GameManager gameManager;

    public int mineMinusThis { get; private set; }
    public int robotsMineMinusThis { get; private set; }

    //private int thisQuestIsOn;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void AcceptQuest()
    {
        questInProgress = true;

        //thisQuestIsOn = gameManager.quests.Count;

        mineMinusThis = quest[currentQuest].GetObjective().mine;
        robotsMineMinusThis = quest[currentQuest].GetObjective().robotsMine;

        gameManager.quests.Add(quest[currentQuest]);
        gameManager.npcs.Add(this);
        gameManager.updateQuests.Invoke();
    }

    public void CompleteQuest()
    {
        if (questIsComplete(quest[currentQuest]))
        {
            questInProgress = false;
            gameManager.quests.Remove(quest[currentQuest]);
            gameManager.npcs.Remove(this);
            GiveReward(quest[currentQuest]);
            gameManager.updateQuests.Invoke();
            currentQuest++;
        }
    }

    public void OreHarvested(string whatWasMined)
    {
        if (!questInProgress) return;

        if (mineMinusThis <= 0) return;

        if (quest[currentQuest].GetObjective().whatToMine == whatWasMined)
        {
            mineMinusThis--;
        }
    }

    public void OreHarvestedByRobot(string whatWasMined)
    {
        if (!questInProgress) return;

        if (robotsMineMinusThis <= 0) return;

        if (quest[currentQuest].GetObjective().whatRobotsMine == whatWasMined)
        {
            robotsMineMinusThis--;
        }
    }

    private bool questIsComplete(Quest quest)
    {
        Quest.questObjective objective = quest.GetObjective();

        if (mineMinusThis > 0 || robotsMineMinusThis > 0)
        {
            return false;
        }
        else return true;
    }

    private void GiveReward(Quest quest)
    {
        Quest.Reward reward = quest.GetReward();

        gameManager.AddMoney(reward.money);
    }
}
