using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss1:AbstractBoss
{
    public Boss1()
    {
        this.isFinalBoss = false;
        this.RemindCount = 2;
        this.secondHp = 800;
        this.firstHp = 1000;
        this.MAX_HP = this.secondHp;
        this.Hp = this.MAX_HP;
        this.coolTime = 60;
    }

    protected override void Move()
    {   
        if (startCount <= 55)
        {
            startCount++;
            if (startCount == 55)
            {
                this.VX = 0;
            }
        }
        if (coolTime > 0)
        {
            coolTime--;
        }
        else
        {
            if (this.RemindCount == 2)
            {
                //第一攻撃
                if (shotCount % 60 == 0)
                {
                    float Angle = 0;
                    Vector3 player_pos = player.transform.position;
                    Angle = Mathf.Atan2(player_pos.y - transform.position.y, player_pos.x - transform.position.x) * Mathf.Rad2Deg;
                    int random = Random.Range(8, 14);
                    for (int i = 0; i <= random; i++)
                    {
                        EnemyShot shot = gameManager.BoundBulletFactories[1].CreateBullet();
                        shot.transform.localPosition = transform.position;
                        shot.Init((i * 360 / random + Angle) * Mathf.Deg2Rad, 0.025f);
                    }
                }
            }
            else
            {
                //第二攻撃
                if (shotCount % 400 == 0)
                {
                    createSubEnemy(80);
                    createSubEnemy(120);
                    createSubEnemy(290);
                    createSubEnemy(240);
                    Vector3 player_pos = player.transform.position;
                    float angle = Mathf.Atan2(player_pos.y -this.transform.position.y, player_pos.x - this.transform.position.x)*Mathf.Rad2Deg;
                    createSubEnemy(angle);
                }
            }
            coolTime = 0;
            shotCount++;
        }
        transform.position += new Vector3(VX, VY, 0.0f);
    }

    private void createSubEnemy(float angle)
    {
        TagEnemyStatus status = new TagEnemyStatus();
        status.X = this.transform.position.x;
        status.Y = this.transform.position.y;
        status.Bulletpattern = BulletPattern.Setting;
        status.BulletInterval = 20;
        status.Hp = 10000000;
        status.BulletSpeed = 0.02f;
        status.Shotcolor = ShotColor.EnemyShot6;
        status.EnemyObj= MGameManager.ResourcesLoader.GetObjectHandle("Boss1SubEnemy");
        int[] coolTimes = { 100 };
        status.CoolTimes = coolTimes;
        Enemy4 enemy=Enemy4.New(status).GetComponent<Enemy4>();
        enemy.setAngle(angle*Mathf.Deg2Rad);
        enemy.speed = 0.06f;
    }

        public static GameObject New(TagEnemyStatus enemy_status)
    {
       GameObject enemy_obj = Instantiate(enemy_status.EnemyObj, new Vector3(enemy_status.X, enemy_status.Y, 0), Quaternion.identity);
        enemy_obj.AddComponent<Boss1>().EnemyStatus = enemy_status;
        enemy_obj.GetComponent<Boss1>().gameManager = GameObject.Find("GameManager").GetComponent<MGameManager>();
        enemy_obj.GetComponent<Boss1>().gameManager.nowBoss = enemy_obj.GetComponent<Boss1>();
        enemy_obj.GetComponent<Boss1>().player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        return enemy_obj;
    }

}
