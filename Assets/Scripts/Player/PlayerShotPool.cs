using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotPool : MonoBehaviour
{
    [SerializeField]
    private PlayerShot _poolObj; // プールするオブジェクト。ここでは弾
    private List<PlayerShot> _list0; //高速ショット
    private List<PlayerShot> _list1;//低速ショット
    private const int MAXCOUNT = 30; // 最初に生成する弾の数

    private Sprite[] images;

    void Awake()
    {
        images = new Sprite[2];
        images[0]= (Sprite)Resources.Load("image/playerShot", typeof(Sprite));
        images[1] = (Sprite)Resources.Load("image/playerShot2", typeof(Sprite));

        CreatePool();
    }

    // 最初にある程度の数、オブジェクトを作成してプールしておく処理
    private void CreatePool()
    {
        _list0= new List<PlayerShot>();
        for (int i = 0; i < MAXCOUNT; i++)
        {
            var newObj = CreateNewBurret(0); // 弾を生成して
            newObj.gameObject.SetActive(false);
            _list0.Add(newObj); // リストに保存しておく
        }
        _list1 = new List<PlayerShot>();
        for (int i = 0; i < MAXCOUNT; i++)
        {
            var newObj = CreateNewBurret(1); // 弾を生成して
            newObj.gameObject.SetActive(false);
            _list1.Add(newObj); // リストに保存しておく
        }

    }

    // 未使用の弾を探して返す処理
    // 未使用のものがなければ新しく作って返す
    public PlayerShot GetBurret(int shotNumber)
    {
        if (shotNumber == 0)
        {
            foreach (var obj in _list0)
            {
                if (!obj.gameObject.activeSelf)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }
        }
        else if (shotNumber == 1)
        {
            foreach (var obj in _list1)
            {
                if (!obj.gameObject.activeSelf)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }
        }
        else
        {
            Debug.Log("PlayerShotPoolでエラー");
        }


        // 全て使用中だったら新しく作り、リストに追加してから返す
        var newObj = CreateNewBurret(shotNumber);
        if (shotNumber == 0)
        {
            _list0.Add(newObj);
        }
        else if (shotNumber == 1)
        {
            _list1.Add(newObj);
        }

        newObj.gameObject.SetActive(true);
        return newObj;
    }

    // 新しく弾を作成する処理
    private PlayerShot CreateNewBurret(int shotNumber)
    {
        var pos = new Vector2(1000, 1000); // 画面外であればどこでもOK
        var newObj = Instantiate(_poolObj, pos, Quaternion.identity); // 弾を生成しておいて
        if (shotNumber == 0)
        {
            newObj.name = _poolObj.name + shotNumber + "の" + (_list0.Count + 1); // 名前を連番でつけてから
        }
        else
        {
            newObj.name = _poolObj.name + shotNumber + "の" + (_list1.Count + 1); // 名前を連番でつけてから
        }
        newObj.GetComponent<SpriteRenderer>().sprite = this.images[shotNumber];

        return newObj; // 返す
    }
}
