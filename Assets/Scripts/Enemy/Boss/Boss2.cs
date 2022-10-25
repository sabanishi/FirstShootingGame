using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss2 : AbstractBoss
{
    private List<AbstractEnemy> subUnits = new List<AbstractEnemy>();
    public Boss2()
    {
        this.isFinalBoss = true;
        this.RemindCount = 4;
        this.forthHp = 1000;
        this.thirdHp = 1300;
        this.secondHp = 700;
        this.firstHp = 800;
        this.MAX_HP = this.forthHp;
        this.Hp = this.MAX_HP;
        this.coolTime = 60;
        this.startCount = 0;
    }

    protected override void Move()
    {
        if (startCount <= 55)
        {
            startCount++;
            if (startCount == 55)
            {
                this.VY = 0;
            }
        }
        if (coolTime > 0)
        {
            coolTime--;
        }
        else
        {
            switch (this.RemindCount)
            {
                case 4:
                    this.firstAction();
                    break;
                case 3:
                    this.SecondAction();
                    break;
                case 2:
                    this.ThirdAction();
                    break;
                case 1:
                    this.ForthAction();
                    break;
            }
            coolTime = 0;
            shotCount++;
        }
        transform.position += new Vector3(VX, VY, 0.0f);
    }

    private void firstAction()
    {
        
        if (shotCount % 200 == 0)
        {
            CreateEnemy1(true);
        }
        if (shotCount % 200 == 120)
        {
            CreateEnemy1(false);
        }
    }

    private void CreateEnemy1(bool isRight)
    {
        TagEnemyStatus status = new TagEnemyStatus();
        float x;
        float speed1;
        float speed2;
        if (isRight)
        {
            x = 100;
            status.VX = -0.02f;
            status.Shotcolor = ShotColor.EnemyShot9;
            speed1 = 0.07f;
            speed2 = 0.04f;
        }
        else
        {
            x= 0;
            status.VX = 0.02f;
            status.Shotcolor = ShotColor.EnemyShot13;
            speed1 = 0.04f;
            speed2 = 0.05f;
        }

        status.X = Utils.ChangeRateX(x);
        status.Y = Utils.ChangeRateY(30);

        status.Bulletpattern = BulletPattern.Setting;
        status.BulletInterval = 100;
        status.Hp = 50;
        status.BulletN_Way = 17;
        float aimAngle = Random.Range(0, 361) * Mathf.Deg2Rad;
        status.EnemyObj = MGameManager.ResourcesLoader.GetObjectHandle("Boss2Sub1");
        int[] coolTimes = {0,8,40};
        float[] Angles = { aimAngle, aimAngle,aimAngle};
        float[] Speeds = { speed1,0,speed2};
        status.CoolTimes = coolTimes;
        status.Bullet_Angles = Angles;
        status.Bullet_Speeds = Speeds;
        status.BulletRange360 = 360;
        this.subUnits.Add(Enemy2.New(status).GetComponent<Enemy2>());
    }

    private void SecondAction()
    {
        if (shotCount % 450 == 0)
        {
            CreateEnemy2();
        }
        if (shotCount % 5 == 0)
        {
            EnemyShot shot = gameManager.BulletFactories[14].CreateBullet();
            shot.transform.localPosition = new Vector3(Utils.ChangeRateX(Random.Range(0,100)),Utils.ChangeRateY(0),0);
            shot.Init(270*Mathf.Deg2Rad,0.04f);
        }
    }

    private void CreateEnemy2()
    {
        for(int i = 0; i <= 3; i++)//縦列
        {
            for(int j = 0; j <= 1; j++)//横列
            {
                TagEnemyStatus status = new TagEnemyStatus();
                status.Hp = 1000000;
                status.X = Utils.ChangeRateX(10 + j * 80);
                status.Y = Utils.ChangeRateY(0);
                status.Bulletpattern = BulletPattern.Winder;
                status.BulletN_Way = 3;
                status.BulletRotateSpeed = 0.01f*(float)Math.Pow(-1,j);
                status.BulletSpeed = 0.07f;
                status.BulletInterval = 6;
                if (i == 0)
                {
                    status.Bullet_Angles = new float[] {270*Mathf.Deg2Rad};
                }
                else if (i == 1)
                {
                    status.Bullet_Angles = new float[] {(-80+j*340)*Mathf.Deg2Rad};
                }
                else if (i == 2)
                {
                    status.Bullet_Angles = new float[] {(-60+j*300)*Mathf.Deg2Rad};
                }else if (i == 3)
                {
                    status.Bullet_Angles = new float[] { (-30 + j * 240) * Mathf.Deg2Rad };
                }
                status.EnemyObj = MGameManager.ResourcesLoader.GetObjectHandle("Boss2Sub1");
                status.Shotcolor = ShotColor.EnemyShot1;
                status.VY = -0.03f * (i + 1);
                this.subUnits.Add(Enemy5.New(status).GetComponent<Enemy5>());
             }
        }
    }

    private void ThirdAction()
    {
        if (shotCount % 400 <= 280)
        {
            if (shotCount % 6 == 0)
            {
                Vector3 player_pos = player.transform.position;
                float Angle = Mathf.Atan2(player_pos.y -transform.position.y, player_pos.x - transform.position.x)*Mathf.Rad2Deg;
                for(int i = 0; i <= 12; i++)
                {
                    EnemyShot shot = gameManager.BulletFactories[9].CreateBullet();
                    shot.transform.localPosition = this.transform.position;
                    shot.Init((Angle+360/12*i)*Mathf.Deg2Rad, 0.1f);
                }
            }
            if (shotCount % 6 == 3)
            {
                Vector3 player_pos = player.transform.position;
                float Angle = Mathf.Atan2(player_pos.y - transform.position.y, player_pos.x - transform.position.x) * Mathf.Rad2Deg;
                for (int i = 0; i <= 13; i++)
                {
                    EnemyShot shot = gameManager.BulletFactories[13].CreateBullet();
                    shot.transform.localPosition = this.transform.position;
                    shot.Init((Angle + 360 / 12 * (i+0.5f)) * Mathf.Deg2Rad, 0.1f);
                }
            }
        }
        if (shotCount % 400 == 0)
        {
            this.CreateEnemy3();
        }
    }

    private void CreateEnemy3()
    {
        for (int i = 0; i <= 1; i++)//縦列
        {
            for (int j = 0; j <= 1; j++)//横列
            {
                TagEnemyStatus status = new TagEnemyStatus();
                status.Hp = 1000000;
                status.X = Utils.ChangeRateX(10 + j * 80);
                status.Y = Utils.ChangeRateY(0);
                status.Bulletpattern = BulletPattern.Random;
                status.BulletN_Way = 1;
                status.BulletSpeed = 0.03f;
                status.BulletInterval = 5;
                status.BulletRange360 = 360;
                status.EnemyObj = MGameManager.ResourcesLoader.GetObjectHandle("Boss2Sub1");
                status.Shotcolor = ShotColor.EnemyShot16;
                status.VY = -0.03f * (i + 1);
                this.subUnits.Add(Enemy5.New(status).GetComponent<Enemy5>());
            }
        }
    }

    private void ForthAction()
    {
        if (shotCount % 5 == 0)
        {
            ModifySpeed();
        }
        if (shotCount % 50 == 0)
        {
            int angle = Random.Range(0, 361);
            for(int i = 0; i < 16; i++)
            {
                float newAngle=(angle+360/16*i)*Mathf.Deg2Rad;
                EnemyShotVelocityChange shot = (EnemyShotVelocityChange)gameManager.VelocityChangeBulletFactories[10].CreateBullet();
                shot.transform.localPosition = this.transform.position;
                int[] coolTimes = { 0, 12, 100 };
                float[] Angles = {newAngle,newAngle,newAngle };
                float[] Speeds = { 0.07f, 0, 0.04f };
                shot.SetFloatAndSpeed(Angles,Speeds, coolTimes);
            }
        }
    }

    private void ModifySpeed()
    {
        Vector3 player_pos = player.transform.position;
        float Angle = Mathf.Atan2(player_pos.y - transform.position.y, player_pos.x - transform.position.x);
        this.VX = (float)0.015 * Mathf.Cos(Angle);
        this.VY = (float)0.015 * Mathf.Sin(Angle);
    }

    protected override void MaxHpChange()
    {
        base.MaxHpChange();
        foreach(var obj in subUnits)
        {
            if (obj != null)
            {
                obj.Die(null);
            }
        }
        subUnits.Clear();
    }

    public static GameObject New(TagEnemyStatus enemy_status)
    {
        GameObject enemy_obj = Instantiate(enemy_status.EnemyObj, new Vector3(enemy_status.X, enemy_status.Y, 0), Quaternion.identity);
        enemy_obj.AddComponent<Boss2>().EnemyStatus = enemy_status;
        enemy_obj.GetComponent<Boss2>().gameManager = GameObject.Find("GameManager").GetComponent<MGameManager>();
        enemy_obj.GetComponent<Boss2>().gameManager.nowBoss = enemy_obj.GetComponent<Boss2>();
        enemy_obj.GetComponent<Boss2>().player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        return enemy_obj;
    }
}
