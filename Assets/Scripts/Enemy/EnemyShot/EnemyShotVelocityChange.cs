using System;
public class EnemyShotVelocityChange : EnemyShot
{
    private int[] coolTimes;
    private float[] Angles;
    private float[] Speeds;
    private int changeCount;
    private int coolTime;
    private int maxChange;

    protected override void Move()
    {
        if (changeCount < maxChange)
        {
            if (coolTime < coolTimes[changeCount])
            {
                coolTime++;
            }
            else
            {
                coolTime = 0;
                this.Init(Angles[changeCount], Speeds[changeCount]);
                changeCount++;
            }
        }

        base.Move();
    }

    public void SetFloatAndSpeed(float[] angles,float[] speeds,int[] coolTimes)
    {
        this.Angles = angles;
        this.Speeds = speeds;
        this.coolTimes = coolTimes;
        this.changeCount = 0;
        this.maxChange = this.Angles.Length;
    }
}