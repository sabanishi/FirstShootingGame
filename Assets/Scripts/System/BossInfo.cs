using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInfo : MonoBehaviour
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
            Vector3 localScale = this.gameObject.transform.localScale;
            localScale.x = 667;
            this.gameObject.transform.localScale = localScale;
        }
        else
        {
            Vector3 localScale = this.gameObject.transform.localScale;
            localScale.x = 0;
            this.gameObject.transform.localScale = localScale;
        }   
        Vector3 position = this.gameObject.transform.position;
        position.x = 3.8f;
        position.y = -2.4f;
        this.gameObject.transform.position = position;
    }
}
