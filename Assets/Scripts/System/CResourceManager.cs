using UnityEngine;
using System.Collections;

public class CResourceManager : MonoBehaviour
{
    private static CResourcesLoader<GameObject> ResourcesLoader = new CResourcesLoader<GameObject>();
    private static CResourcesLoader<Texture2D> TexturesLoader = new CResourcesLoader<Texture2D>();

    public static bool LoadTexture(string file_name)
    {
        return TexturesLoader.LoadObject(file_name);
    }

    public static Texture2D GetTextureHandle(string obj_name)
    {
        return TexturesLoader.GetObjectHandle(obj_name);
    }

    public static GameObject GetObjectHandle(string obj_name)
    {
        return ResourcesLoader.GetObjectHandle(obj_name);
    }

    void Awake()
    {
        // Prefabsフォルダから、すべてのGameObjectを読み込む
        if (!ResourcesLoader.LoadAllObjects("Prefab"))
        {
            print("Prefabファイル読み込みに失敗しました");
        }
        // 音声ファイルを一曲だけ読み込む
        if (!CSoundPlayer.LoadAudioClip("Sound/Luste"))
        {
            print("Lusteファイル読み込みに失敗しました");
        }
        // Soundフォルダからすべての音声ファイルを読み込む
        if (!CSoundPlayer.LoadAllSounds("Sound"))
        {
            print("Soundファイル読み込みに失敗しました");
        }
        // se_flagオフで音声ファイルを再生する
        CSoundPlayer.PlaySound("Luste", false);
    }

    void Update()
    {
        // Jキーを押すと5秒かけて再生中のフェードアウトする
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(CSoundPlayer.SetFadeTimer(1.0f, 0.0f, 5.0f));
        }
        // Kキーを押すと5秒かけてフェードイン再生する
        if (Input.GetKeyDown(KeyCode.K))
        {
            CSoundPlayer.PlaySound("dangeon07", false);
            StartCoroutine(CSoundPlayer.SetFadeTimer(0.0f, 1.0f, 5.0f));
        }
    }
}
