using System;
using UnityEngine;

public class Enemy1 :AbstractEnemy
{
   protected override void Move()
    {
        
        if (Cnt == 0)
        {
            VY = -3.0f;
        }
        if (Cnt ==100)
        {
            VY = 0;
            BulletDischarge = true;
        }
        /* if (Cnt == 100 + Wait)
         {
             VY = 3.0f;
             BulletDischarge = false;
         }*/

        transform.position += new Vector3(VX, VY, 0.0f);



    }
    public static GameObject New(TagEnemyStatus enemy_status)
    {
        GameObject enemy_obj = Instantiate(enemy_status.EnemyObj, new Vector3(enemy_status.X, enemy_status.Y, 0), Quaternion.identity);
        enemy_obj.AddComponent<Enemy1>().EnemyStatus = enemy_status;
        return enemy_obj;
    }
}
