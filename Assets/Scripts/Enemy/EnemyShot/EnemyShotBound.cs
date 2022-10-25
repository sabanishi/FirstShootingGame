using System;
using UnityEngine;

public class EnemyShotBound:EnemyShot
{
    private int boundCount;
    private int coolTime;

    protected override void Move()
    {
        if (coolTime > 0)
        {
            coolTime--;
        }
        else
        {
            if (Utils.IsOutWindow(transform.position))
            {
                if (boundCount == 0)
                {
                    float newAngle;
                    if(transform.position.x < Utils.LimitTopLeft.x || Utils.LimitButtomRight.x < transform.position.x)
                    {
                        newAngle = (180-Angle * Mathf.Rad2Deg) * Mathf.Deg2Rad;
                    }
                    else
                    {
                        newAngle = (360-Angle * Mathf.Rad2Deg) * Mathf.Deg2Rad;
                    }
                    boundCount++;
                    coolTime = 10;
                    var angles = transform.localEulerAngles;
                    this.Init(newAngle, Speed) ;
                    
                }
                else if (boundCount == 1)
                {
                    boundCount = 0;
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
