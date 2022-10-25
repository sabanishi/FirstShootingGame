using System;
using UnityEngine;

public abstract class AbstractEnemy :MonoBehaviour
{
    public TagEnemyStatus EnemyStatus;
    protected CBulletController BulletController;

    protected bool BulletDischarge = false; // 弾幕発射フラグ
    protected int Cnt = 0;
    public int Hp;
    public float VX = 0.0f, VY = 0.0f;
    private int damageCount;
    private bool isGetDamage;

    public GameObject m_explosionPrefab;
    public GameObject m_hitPrefab;

    public virtual void Start()
    {
        switch (EnemyStatus.Bulletpattern)
        {
            case BulletPattern.Aim:
                BulletController = new CAimBulletController(
                    EnemyStatus.Shotcolor, EnemyStatus.BulletSpeed, EnemyStatus.BulletN_Way, EnemyStatus.BulletRange360, EnemyStatus.BulletInterval, this);
                break;
            case BulletPattern.Winder:
                BulletController = new CWinderBulletController(
                    EnemyStatus.Shotcolor, EnemyStatus.BulletSpeed, EnemyStatus.BulletInterval, EnemyStatus.BulletRotateSpeed, EnemyStatus.BulletN_Way, EnemyStatus.Bullet_Angles, EnemyStatus.BulletRange360, this);
                break;
            case BulletPattern.Random:
                BulletController = new CRandomBulletController(
                                    EnemyStatus.Shotcolor, EnemyStatus.BulletSpeed, EnemyStatus.BulletInterval, EnemyStatus.BulletN_Way, EnemyStatus.BulletRange360, this);
                break;
            case BulletPattern.Setting:
                BulletController = new CSettingBulletController(
                     EnemyStatus.Shotcolor, EnemyStatus.BulletSpeed, EnemyStatus.BulletInterval, this, EnemyStatus.Bullet_Angles, EnemyStatus.Bullet_Speeds, EnemyStatus.CoolTimes, EnemyStatus.BulletN_Way, EnemyStatus.BulletRange360);
                break;
        }
        this.VX = EnemyStatus.VX;
        this.VY = EnemyStatus.VY;
        m_explosionPrefab = (GameObject)Resources.Load("Prefab/Explosion");
        m_hitPrefab = (GameObject)Resources.Load("Prefab/hit");
        this.Hp = EnemyStatus.Hp;

    }

    protected virtual void Move()
    {

    }

    public void FixedUpdate()
    {
        Move();

        if (BulletController != null && BulletDischarge)
        {
            BulletController.Move(transform.position);
        }

        if (Utils.IsOut(transform.position))
        {
            Destroy(gameObject);
        }

        Cnt++;
        if (this.damageCount > 0)
        {
            this.damageCount--;
        }
        else
        {
            this.damageCount = 0;
        }
        if (this.isGetDamage)
        {
            this.damageCount = 10;
            this.isGetDamage = false;
        }
        
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.damageCount == 0&&this.Hp>0)
        {
            if (this.gameObject.activeSelf)
            {
                if (collision.name.Contains("playerShot"))
                {
                    collision.gameObject.SetActive(false);
                    Hp--;
                    if (Hp <= 0)
                    {
                        this.Die(collision);
                    }
                    else
                    {
                        Instantiate(
                        m_hitPrefab,
                        collision.transform.localPosition,
                        Quaternion.identity
                        );
                    }
                    this.isGetDamage = true;
                }
            }
        }
    }

    public virtual void Die(Collider2D collision)
    {
        Destroy(gameObject);
        if (collision != null)
        {
            Instantiate(
                     m_explosionPrefab,
                     collision.transform.localPosition,
                     Quaternion.identity
                     );
        }
        else
        {
            Instantiate(
                     m_explosionPrefab,
                     this.transform.localPosition,
                     Quaternion.identity
                     );
        }
        
    }
}

public struct TagEnemyStatus
{
    public GameObject EnemyObj;
    public float X, Y, BulletSpeed, BulletRange360, VX, VY, BulletRotateSpeed;
    public int Hp, BulletN_Way, BulletInterval;
    public ShotColor Shotcolor;
    public BulletPattern Bulletpattern;
    public float[] Bullet_Angles;
    public float[] Bullet_Speeds;
    public int[] CoolTimes;

    public TagEnemyStatus(GameObject enemy_obj, float x , float y,
        int hp, BulletPattern bullet_pattern,float bullet_speed,ShotColor shot_color,
        int bullet_n_way,float bullet_range360,int bullet_interval,float vx,float vy,float bullet_RotateSpeed
        )
    {
        EnemyObj = enemy_obj;
        X = x;
        Y = y;
        Hp = hp;
        Bulletpattern = bullet_pattern;
        Shotcolor = shot_color;
        BulletSpeed = bullet_speed;
        BulletN_Way = bullet_n_way;
        BulletRange360 = bullet_range360;
        BulletInterval = bullet_interval;
        VX = vx;
        VY = vy;
        BulletRotateSpeed = bullet_RotateSpeed;
        Bullet_Angles = null;
        Bullet_Speeds=null;
        CoolTimes=null;
}
}
