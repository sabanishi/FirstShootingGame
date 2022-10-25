using System;
using UnityEngine;

public class CWinderBulletController : CBulletController
{
    float RotateSpeed;
    int N_way;
    float AimRotate;//時として変わる撃つ方向
    float Range360;

    public CWinderBulletController(ShotColor shotColor, float bl_speed,int interval,float rotateSpeed,int n_way,float[] angles,float range360 ,AbstractEnemy parent) : base(shotColor, bl_speed, 0f, interval,parent)
    {
        RotateSpeed = rotateSpeed;
        N_way = n_way;
        if (angles != null)
        {
            AimRotate = angles[0];
        }
        else
        {
            AimRotate = 0;
        }
        if (range360 == 0)
        {
            Range360 = 360;
        }
        else
        {
            Range360 = range360;
        }
    }

    public override void Move(Vector3 pos)
    {
        if (Cnt % Interval == 0)
        {
            if (N_way % 2 == 0)
            {
                //偶数弾
                for (int i = 0; i < N_way; i++)
                {
                    var angle = AimRotate+Range360/ (N_way * 2) * (i * 2 + 1);
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
                    var angle = AimRotate* Mathf.Rad2Deg +Range360/ N_way * i;
                    EnemyShot shot = GameManager.BulletFactories[(int)Shotcolor].CreateBullet();
                    shot.transform.localPosition = pos;
                    shot.Init(angle * Mathf.Deg2Rad, Speed);
                }
            }
            AimRotate += RotateSpeed;
        }
        Cnt++;
    }
}
