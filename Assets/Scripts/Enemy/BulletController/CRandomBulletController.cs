using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CRandomBulletController : CBulletController
{
    private int N_way;
    private float range360;
    public CRandomBulletController(
        ShotColor shotColor, float bl_speed, int interval, int n_way,float range360,AbstractEnemy parent) : base(shotColor, bl_speed, 0f, interval,parent)
    {
        N_way = n_way;
        this.range360 = range360;
    }
    public override void Move(Vector3 pos)
    {
        if (Cnt % Interval == 0)
        {
            for(int i = 0; i < N_way; i++)
            {
                var angle = this.Angle+Random.Range(0,this.range360)-this.range360/2;
                EnemyShot shot = GameManager.BulletFactories[(int)Shotcolor].CreateBullet();
                shot.transform.localPosition = pos;
                shot.Init(angle * Mathf.Deg2Rad, Speed);
            }
        }
        Cnt++;
    }
}