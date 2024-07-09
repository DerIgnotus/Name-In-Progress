using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCButtons : MonoBehaviour
{
    [SerializeField] private Transform bg;
    [SerializeField] private Transform getQuestTransform;
    [SerializeField] private Transform completeQuestTransform;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void Exit()
    {
        gameManager.SetCanMove(true);
        bg.gameObject.SetActive(false);
    }

    public void GetQuest()
    {
        gameManager.currentNPC.AcceptQuest();

        //getQuestTransform.gameObject.SetActive(false);
        //completeQuestTransform.gameObject.SetActive(true);

        gameManager.SetCanMove(true);
        bg.gameObject.SetActive(false);
    }

    public void CompleteQuest()
    {
        gameManager.currentNPC.CompleteQuest();

        if (gameManager.currentNPC.questInProgress) return;

        //getQuestTransform.gameObject.SetActive(true);
        //completeQuestTransform.gameObject.SetActive(false);

        gameManager.SetCanMove(true);
        bg.gameObject.SetActive(false);
    }
}
