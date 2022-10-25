using System;
using UnityEngine;

public class Enemy5:AbstractEnemy
{
   protected override void Move()
    {
        
        if (Cnt == 20)
        {
            this.VY = 0;
            this.BulletDischarge = true;
        }
        if (Cnt == 370)
        {
            this.VY = -0.1f;
            this.BulletDischarge = false;
        }

        transform.position += new Vector3(VX, VY, 0.0f);
    }

    public static GameObject New(TagEnemyStatus enemy_status)
    {
        GameObject enemy_obj = Instantiate(enemy_status.EnemyObj, new Vector3(enemy_status.X, enemy_status.Y, 0), Quaternion.identity);
        enemy_obj.AddComponent<Enemy5>().EnemyStatus = enemy_status;
        return enemy_obj;
    }
}
