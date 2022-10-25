using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 :AbstractEnemy
{
    protected override void Move()
    {
        BulletDischarge = true;

        transform.position += new Vector3(VX, VY, 0.0f);
    }

    public static GameObject New(TagEnemyStatus enemy_status)
    {
        GameObject enemy_obj = Instantiate(enemy_status.EnemyObj, new Vector3(enemy_status.X, enemy_status.Y, 0), Quaternion.identity);
        enemy_obj.AddComponent<Enemy2>().EnemyStatus = enemy_status;
        return enemy_obj;
    }
}
