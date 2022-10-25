using System.Collections.Generic;
using UnityEngine;

public class MGameManager : MonoBehaviour
{
    public static CResourcesLoader<GameObject> ResourcesLoader = new CResourcesLoader<GameObject>();
    public CCommandManager CommandManager = new CCommandManager();

    public EnemyShot EnemyShotPrefab;
    public EnemyShotBound EnemyShotBoundPrefab;
    public EnemyShotVelocityChange EnemyShotVelocityPrefab;
    public EnemyShot EnemyShotPrefab2;
    public EnemyShotBound EnemyShotBoundPrefab2;
    public EnemyShotVelocityChange EnemyShotVelocityPrefab2;

    public bool isAliveBoss;
    public AbstractBoss nowBoss;
    [SerializeField] GameObject PauseSpriteMask;
    [SerializeField]private PauseInfo pauseInfo;

    public class BulletFactory
    {
        [SerializeField]
        public string SpriteName;
        public Sprite BulletSprite;
        public bool ColliderType;
        public List<EnemyShot> EnemyShots;
        public List<EnemyShotBound> EnemyShotBounds;
        public List<EnemyShotVelocityChange> EnemyShotVelocityChanges;
        public EnemyShot shotPrefab;
        public BulletType BulletType;

        public BulletFactory(string sprite_name, BulletType type)
        {
            SpriteName = sprite_name;
            BulletType = type;
        }

        public void Init()
        {
            BulletSprite = (Sprite)Resources.Load(SpriteName, typeof(Sprite));
            switch (BulletType)
            {
                case BulletType.Normal:
                    EnemyShots = new List<EnemyShot>();
                    break;
                case BulletType.Bound:
                    EnemyShotBounds = new List<EnemyShotBound>();
                    break;
                case BulletType.VelocityChange:
                    EnemyShotVelocityChanges = new List<EnemyShotVelocityChange>();
                    break;
            }
        }

        public EnemyShot CreateBullet()
        {

            switch (BulletType)
            {
                case BulletType.Normal:
                    foreach (var obj in EnemyShots)
                    {
                        if (!obj.gameObject.activeSelf)
                        {
                            obj.gameObject.SetActive(true);
                            return obj;
                        }
                    }
                    break;
                case BulletType.Bound:
                    foreach (var obj in EnemyShotBounds)
                    {
                        if (!obj.gameObject.activeSelf)
                        {
                            obj.gameObject.SetActive(true);
                            return obj;
                        }
                    }
                    break;
                case BulletType.VelocityChange:
                    foreach(var obj in EnemyShotVelocityChanges)
                    {
                        if (!obj.gameObject.activeSelf)
                        {
                            obj.gameObject.SetActive(true);
                            return obj;
                        }
                    }
                    break;
            }
            var newObj = CreateNewBurret(BulletType);
            switch (BulletType)
            {
                case BulletType.Normal:
                    EnemyShots.Add(newObj);
                    break;
                case BulletType.Bound:
                    EnemyShotBounds.Add((EnemyShotBound)newObj);
                    break;
                case BulletType.VelocityChange:
                    EnemyShotVelocityChanges.Add((EnemyShotVelocityChange)newObj);
                    break;
            }
            newObj.gameObject.SetActive(true);
            return newObj;
        }

        // 新しく弾を作成する処理
        private EnemyShot CreateNewBurret(BulletType bulletType)
        {
            Vector2 pos;
            EnemyShot newObj;
            switch (bulletType)
            {
                case BulletType.Normal:
                    pos = new Vector2(1000, 1000); // 画面外であればどこでもOK
                    newObj = Instantiate(shotPrefab, pos, Quaternion.identity); // 弾を生成しておいて
                    newObj.name = this.shotPrefab.name + (EnemyShots.Count + 1);
                    newObj.GetComponent<SpriteRenderer>().sprite = this.BulletSprite;
                    return newObj.GetComponent<EnemyShot>(); // 返す
                case BulletType.Bound:
                    pos = new Vector2(1000, 1000); // 画面外であればどこでもOK
                    newObj = Instantiate(shotPrefab, pos, Quaternion.identity); // 弾を生成しておいて
                    newObj.name = this.shotPrefab.name + (EnemyShotBounds.Count + 1);
                    newObj.GetComponent<SpriteRenderer>().sprite = this.BulletSprite;
                    return newObj.GetComponent<EnemyShotBound>(); // 返す
                case BulletType.VelocityChange:
                    pos = new Vector2(1000, 1000); // 画面外であればどこでもOK
                    newObj = Instantiate(shotPrefab, pos, Quaternion.identity); // 弾を生成しておいて
                    newObj.name = this.shotPrefab.name + (EnemyShotVelocityChanges.Count + 1);
                    newObj.GetComponent<SpriteRenderer>().sprite = this.BulletSprite;
                    return newObj.GetComponent<EnemyShotVelocityChange>(); // 返す
            }
            return null;
        }
    }

    public BulletFactory[] BulletFactories = new BulletFactory[]
        {
            new BulletFactory("image/enemyShot/enemyShot0",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot1",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot2",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot3",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot4",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot5",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot6",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot7",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot8",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot9",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot10",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot11",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot12",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot13",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot14",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot15",BulletType.Normal),
            new BulletFactory("image/enemyShot/enemyShot16",BulletType.Normal),
        };

    public BulletFactory[] BoundBulletFactories = new BulletFactory[]
    {
            new BulletFactory("image/enemyShot/enemyShot0",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot1",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot2",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot3",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot4",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot5",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot6",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot7",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot8",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot9",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot10",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot11",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot12",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot13",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot14",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot15",BulletType.Bound),
            new BulletFactory("image/enemyShot/enemyShot16",BulletType.Bound),
    };

    public BulletFactory[] VelocityChangeBulletFactories = new BulletFactory[]
    {
            new BulletFactory("image/enemyShot/enemyShot0",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot1",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot2",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot3",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot4",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot5",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot6",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot7",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot8",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot9",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot10",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot11",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot12",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot13",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot14",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot15",BulletType.VelocityChange),
            new BulletFactory("image/enemyShot/enemyShot16",BulletType.VelocityChange),
    };



    public void Start()
    {
        for (int i = 0; i < BulletFactories.Length; i++)
        {
            BulletFactories[i].Init();
            if (i <= 8)
            {
                BulletFactories[i].shotPrefab = this.EnemyShotPrefab;
            }
            else
            {
                BulletFactories[i].shotPrefab = this.EnemyShotPrefab2;
            }
        }

        for (int i = 0; i < BoundBulletFactories.Length; i++)
        {
            BoundBulletFactories[i].Init();
            if (i <= 8)
            {
                BoundBulletFactories[i].shotPrefab = this.EnemyShotBoundPrefab;
            }
            else
            {
                BoundBulletFactories[i].shotPrefab = this.EnemyShotBoundPrefab2;
            }
        }

        for (int i = 0; i < VelocityChangeBulletFactories.Length; i++)
        {
            VelocityChangeBulletFactories[i].Init();
            if (i <= 8)
            {
                VelocityChangeBulletFactories[i].shotPrefab = this.EnemyShotVelocityPrefab;
            }
            else
            {
                VelocityChangeBulletFactories[i].shotPrefab = this.EnemyShotVelocityPrefab2;
            }
        }

        // Prefabフォルダから、すべてのGameObjectを読み込む
        if (!ResourcesLoader.LoadAllObjects("Prefab"))
        {
            print("Prefabファイル読み込みに失敗しました");
        }
        // Soundフォルダからすべての音声ファイルを読み込む
        /* if (!CSoundPlayer.LoadAllSounds("se"))
         {
             print("seファイル読み込みに失敗しました");
         }
         if (!CSoundPlayer.LoadAllSounds("music"))
         {
             print("seファイル読み込みに失敗しました");
         }*/
        string file_name = Application.dataPath + @"/Scenes/Stage/stage1.txt";
        CommandManager.Initalize(this);
        CommandManager.LoadScript(file_name);

        pauseInfo.Init(this);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.GameStop();
        }
        if (Time.timeScale == 0)
        {
            pauseInfo.Check();
        }
        else
        {
            CommandManager.Run();
            if (this.nowBoss != null)
            {
                this.isAliveBoss = true;
            }
            else
            {
                this.isAliveBoss = false;
            }
        }
    }

    //一時停止
    private void GameStop()
    {
        PauseSpriteMask.SetActive(true);
        Time.timeScale = 0;
    }

    //再開
    public void GameResume()
    {
        PauseSpriteMask.SetActive(false);
        Time.timeScale = 1.0f;
    }

    //最初から
    public void GoStart()
    {
        Debug.Log("最初から");
    }
    //メニューに戻る
    public void GoBack()
    {
        Debug.Log("メニューへ");
    }

    //弾を全て消す処理
    public void deleteAllEnemyShot()
    {
        foreach (var obj in BulletFactories)
        {
            foreach (var shot in obj.EnemyShots)
            {
                if (shot.gameObject.activeSelf)
                {
                    shot.gameObject.SetActive(false);
                }
            }
        }

        foreach (var obj in BoundBulletFactories)
        {
            foreach (var shot in obj.EnemyShotBounds)
            {
                if (shot.gameObject.activeSelf)
                {
                    shot.gameObject.SetActive(false);
                }
            }
        }

        foreach (var obj in VelocityChangeBulletFactories)
        {
            foreach (var shot in obj.EnemyShotVelocityChanges)
            {
                if (shot.gameObject.activeSelf)
                {
                    shot.gameObject.SetActive(false);
                }
            }
        }
    }
}
