
using UnityEngine;
using UnityEngine.Rendering;

public abstract class AbstractBoss:AbstractEnemy
{
    protected bool isFinalBoss;

    public int MAX_HP;
    public int RemindCount;
    
    protected int firstHp;
    protected int secondHp;
    protected int thirdHp;
    protected int forthHp;
    protected MGameManager gameManager;
    protected Player player;
    protected int coolTime;
    protected int startCount;
    protected int shotCount;

    public override void Start()
    {
        base.Start();
        gameObject.AddComponent<SortingGroup>().sortingOrder = -2;
     }

    public override void Die(Collider2D collision)
    {
        this.RemindCount--;
        if (this.RemindCount == 0)
        {
            base.Die(collision);
            Vector3 position;
            for (int i = 0; i <= 7; i++)
            {
                position = new Vector3
                {
                    x = this.transform.position.x + (Random.value * 2f - 1f),
                    y = this.transform.position.y + (Random.value * 2f - 1f),
                    z = this.transform.position.z
                };
                Instantiate(
                               m_explosionPrefab,
                               position,
                               Quaternion.identity
                            );
            }
            this.gameManager.CommandManager.isStop = false;
            this.gameManager.deleteAllEnemyShot();
            if (this.isFinalBoss)
            {
                //ゲーム終わらせる処理
            }
           
        }
        else
        {
            Vector3 position;
            for(int i = 0; i <= 7; i++)
            {
                position = new Vector3
                {
                    x = this.transform.position.x + (Random.value * 2f - 1f),
                    y = this.transform.position.y + (Random.value * 2f - 1f),
                    z = this.transform.position.z
                };
                Instantiate(
                               m_explosionPrefab,
                               position,
                               Quaternion.identity
                            );
            }  
            this.MaxHpChange();
        }
    }

    protected virtual void MaxHpChange()
    {
        switch (this.RemindCount)
        {
            case 1:
                this.MAX_HP = this.firstHp;
                break;
            case 2:
                this.MAX_HP = this.secondHp;
                break;
            case 3:
                this.MAX_HP = this.thirdHp;
                break;
            case 4:
                this.MAX_HP = this.forthHp;
                break;
        }
        this.Hp = this.MAX_HP;
        this.coolTime = 100;
        this.gameManager.deleteAllEnemyShot();
        this.shotCount = 0;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.coolTime == 0)
        {
            base.OnTriggerEnter2D(collision);
        }   
    }
}
