using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest", order = 0)]
public class Quest : ScriptableObject
{
    [SerializeField] private string questName;
    [SerializeField] private string questDescription;
    [SerializeField] private Reward reward;
    [SerializeField] private questObjective objective;

    [System.Serializable]
    public struct questObjective
    {
        public int mine;
        public string whatToMine;
        public int robotsMine;
        public string whatRobotsMine;
    }

    [System.Serializable]
    public struct Reward
    {
        public int money;
    }

    public questObjective GetObjective()
    {
        return objective;
    }

    public Reward GetReward()
    {
        return reward;
    }

    public string GetQuestName()
    {
        return questName;
    }
}



