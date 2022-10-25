using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpBarScript : MonoBehaviour
{
    private MGameManager gameManager;

    void Update()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<MGameManager>();
            GameObject barObj = GameObject.Find("BossHpBarFrame");
            Transform bar = barObj.transform;
            Vector3 localFrame = bar.localScale;
            localFrame.x = 1.35f;
            bar.localScale = localFrame;
        }
        if (gameManager.isAliveBoss)
        {
           float hp= gameManager.nowBoss.Hp;
           float maxHp = gameManager.nowBoss.MAX_HP;
            Transform transform = this.transform;
            Vector3 localScale = transform.localScale;
            localScale.x = (float)(1.35*hp / maxHp);
            transform.localScale = localScale;
       }
    }
}
