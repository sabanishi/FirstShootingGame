using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLifeCount : MonoBehaviour
{
    private MGameManager gameManager;

    void Update()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<MGameManager>();
        }
        if (gameManager.isAliveBoss)
        {
            int count = gameManager.nowBoss.RemindCount;
            this.gameObject.GetComponent<Text>().text = count+"";
        }
    }
}
