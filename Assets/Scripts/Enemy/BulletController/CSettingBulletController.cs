using System;
using UnityEngine;

public class CSettingBulletController : CBulletController
{
    private float[] angles;
    private float[] speeds;
    private int[] coolTimes;
    private int N_way;
    private float Range360;
    public CSettingBulletController(ShotColor shotColor, float bl_speed, int interval,AbstractEnemy parent,
        float[] angles,float[] speeds,int[] coolTimes,int n_way,float range360) : base(shotColor, bl_speed, 0f, interval,parent)
    {
        this.angles = angles;
        this.speeds = speeds;
        this.coolTimes = coolTimes;
        N_way = n_way;
        if (N_way == 0) N_way = 2;
        Range360 = range360;
    }

    public override void Move(Vector3 pos)
    {
        if (Cnt % Interval == 0)
        {
            for (int i = 0; i < N_way; i++)
            {
                EnemyShotVelocityChange shot = (EnemyShotVelocityChange)GameManager.VelocityChangeBulletFactories[(int)Shotcolor].CreateBullet();
                shot.transform.localPosition = pos;
                if (angles == null && speeds == null)
                {
                    float angle = Mathf.Atan(this.parentEnemy.VY / this.parentEnemy.VX) * Mathf.Rad2Deg + i * 180 - 90;
                    shot.Init(angle * Mathf.Deg2Rad, 0);
                    float[] angles = { angle * Mathf.Deg2Rad };
                    float[] speeds = { Speed };
                    shot.SetFloatAndSpeed(angles, speeds, coolTimes);
                }
                else
                { 
                    float[] newAngles = new float[angles.Length];
                    Array.Copy(angles, newAngles, angles.Length);
                
                    for (int j = 0; j < angles.Length; j++)
                    {
                        if (Range360 == 360)
                        {
                            if (N_way % 2 == 0)
                            {
                                newAngles[j] += (int)(Range360 / (N_way * 2) * (i * 2 + 1))*Mathf.Deg2Rad;
                            }
                            else
                            {
                                newAngles[j] += (int)(Range360 / N_way * i) * Mathf.Deg2Rad;
                            }
                        }
    
                        else
                        {
                            if (N_way % 2 == 0)
                            {
                                //偶数弾
                                newAngles[j] += (int)(Range360 / (N_way * 2 - 2) * (i * 2 - N_way + 1)) * Mathf.Deg2Rad;
                            }
                            else
                            {
                                //奇数弾
                                if (!(N_way == 1))
                                {
                                    newAngles[j] += (int)(Range360 / (N_way - 1) * (i - (N_way - 1) / 2)) * Mathf.Deg2Rad;
                                }
                            }
                        }
                    }
                    
                    shot.SetFloatAndSpeed(newAngles, speeds, coolTimes);
                }  
            }
        }

        Cnt++;
    }
}