using System;
using UnityEngine;

public class CAimBulletController : CBulletController
{
    private int N_way;
    private float Range360;//度数方
    public CAimBulletController(ShotColor shotColor,float bl_speed,int n_way,float range360,int interval,AbstractEnemy parent) : base(shotColor,bl_speed,0f,interval,parent)
    {
        N_way = n_way;
        Range360 = range360;
    }

    public override void Move(Vector3 pos)
    {
        if (Cnt %Interval == 0)
        {
            GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player");
            if (PlayerObj != null)
            {
                Vector3 player_pos = PlayerObj.transform.position;
                Angle = Mathf.Atan2(player_pos.y - pos.y, player_pos.x - pos.x);
            }
            else // プレイヤーが見つからなければ下に発射
            {
                Angle = Mathf.PI + (Mathf.PI / 2);
            }
            if (Range360 == 360)
            {
                if (N_way % 2 == 0)
                {
                    //偶数弾
                    for (int i = 0; i < N_way; i++)
                    {
                        var angle = Angle * Mathf.Rad2Deg + Range360 / (N_way * 2) * (i * 2+1);
                        EnemyShot shot = GameManager.BulletFactories[(int)Shotcolor].CreateBullet();
                        shot.transform.localPosition = pos;
                        shot.Init(angle * Mathf.Deg2Rad, Speed);
                    }
                }
                else
                {
                    //奇数弾
                    for (int i = 0; i < N_way; i++)
                    {
                        var angle = Angle * Mathf.Rad2Deg + Range360 / N_way * i;
                        EnemyShot shot = GameManager.BulletFactories[(int)Shotcolor].CreateBullet();
                        shot.transform.localPosition = pos;
                        shot.Init(angle * Mathf.Deg2Rad, Speed);
                    }
                }
            }
            else
            {
                if (N_way % 2 == 0)
                {
                    //偶数弾
                    for (int i = 0; i < N_way; i++)
                    {
                        var angle = Angle * Mathf.Rad2Deg + Range360 / (N_way * 2 - 2) * (i * 2 - N_way + 1);
                        EnemyShot shot = GameManager.BulletFactories[(int)Shotcolor].CreateBullet();
                        shot.transform.localPosition = pos;
                        shot.Init(angle * Mathf.Deg2Rad, Speed);
                    }
                }
                else
                {
                    //奇数弾
                    if (N_way == 1)
                    {
                        EnemyShot shot = GameManager.BulletFactories[(int)Shotcolor].CreateBullet();
                        shot.transform.localPosition = pos;
                        shot.Init(Angle, Speed);
                    }
                    else
                    {
                        for (int i = 0; i < N_way; i++)
                        {
                            var angle = Angle * Mathf.Rad2Deg + Range360 / (N_way - 1) * (i - (N_way - 1) / 2);
                            EnemyShot shot = GameManager.BulletFactories[(int)Shotcolor].CreateBullet();
                            shot.transform.localPosition = pos;
                            shot.Init(angle * Mathf.Deg2Rad, Speed);
                        }
                    }
                }
            }
        }
        Cnt++;
    }
}
