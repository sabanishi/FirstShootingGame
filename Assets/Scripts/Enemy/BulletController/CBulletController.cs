using System;
using UnityEngine;

public abstract class CBulletController
{
    protected int Cnt,Interval;
    protected ShotColor Shotcolor;
    protected float Speed, Angle;
    protected MGameManager GameManager;
    protected AbstractEnemy parentEnemy;
    public CBulletController(ShotColor shotColor,float speed, float angle,int interval,AbstractEnemy parent)
    {
        Cnt = 0;
        Interval = interval;
        Shotcolor = shotColor;
        Speed = speed;
        Angle = angle;
        this.parentEnemy = parent;
        GameManager = GameObject.Find("GameManager").GetComponent<MGameManager>();
    }
    public virtual void Move(Vector3 pos)
    {

    }
}
