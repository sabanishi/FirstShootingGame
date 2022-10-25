using System;
using UnityEngine;

public class Enemy4:AbstractEnemy
{
    private float angle;
    public void setAngle(float angle)
    {
        this.angle = angle;
    }
    public float speed;
    private bool isInit;
    protected override void Move()
    {
        BulletDischarge = true;
        if (!isInit)
        {
            this.Init(this.angle);
            isInit = true;
        }
        if (Utils.IsOutWindow(transform.position))
        { 
            float newAngle;
            if (!(transform.position.x < Utils.LimitTopLeft.x || Utils.LimitButtomRight.x < transform.position.x))
            {
                newAngle = (360 - (angle * Mathf.Rad2Deg)) * Mathf.Deg2Rad;
                this.Init(newAngle);
            }
        }
        transform.position += new Vector3(VX, VY, 0.0f);
    }

    public void Init(float newAngle)
    {
        this.VX = speed *Mathf.Cos(newAngle);
        this.VY = speed *Mathf.Sin(newAngle);
        this.angle = newAngle;
    }

    public static GameObject New(TagEnemyStatus enemy_status)
    {
        GameObject enemy_obj = Instantiate(enemy_status.EnemyObj, new Vector3(enemy_status.X, enemy_status.Y, 0), Quaternion.identity);
        enemy_obj.AddComponent<Enemy4>().EnemyStatus = enemy_status;
        return enemy_obj;
    }
}
