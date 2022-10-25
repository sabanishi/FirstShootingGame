using System;
using UnityEngine;

public class Enemy3 : AbstractEnemy
{
    float vy;
    protected override void Move()
    {
        if (Cnt == 0)
        {
            BulletDischarge = false;
        }
        if (Cnt == 100)
        {
            vy = VY;
            VY = 0;
            BulletDischarge = true;
        }
        if (Cnt == 800)
        {
            VY = vy;
            BulletDischarge = false;
        }
        transform.position += new Vector3(VX, VY, 0.0f);
    }

    public static GameObject New(TagEnemyStatus enemy_status)
    {
        GameObject enemy_obj = Instantiate(enemy_status.EnemyObj, new Vector3(enemy_status.X, enemy_status.Y, 0), Quaternion.identity);
        enemy_obj.AddComponent<Enemy3>().EnemyStatus = enemy_status;
        return enemy_obj;
    }
}

