using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Utils
{
	public static Vector2 m_moveLimit = new Vector2(6.5f,3.4f);

    // 移動可能な範囲

   public static Vector2 LimitTopLeft = new Vector2(-7.0f, 3.8f);
   public  static Vector2 LimitButtomRight = new Vector2(1.3f, -3.8f);

	//オブジェクトが消える範囲かどうか
    public static bool IsOut(Vector3 position)
    {
        return (position.x < LimitTopLeft.x-0.2f ||
               LimitTopLeft.y+0.2f < position.y ||
                LimitButtomRight.x+0.2f < position.x ||
                position.y < LimitButtomRight.y-0.2f);
    }

	//画面内かどうか
	public static bool IsOutWindow(Vector3 position)
    {
		return (position.x < LimitTopLeft.x||
			   LimitTopLeft.y< position.y ||
				LimitButtomRight.x< position.x ||
				position.y < LimitButtomRight.y);
	}

    public static Vector3 ClampPosition(Vector3 position){
		return new Vector3(
			Mathf.Clamp(position.x,-m_moveLimit.x,-m_moveLimit.x+m_moveLimit.y*2.1f),
			Mathf.Clamp(position.y,-m_moveLimit.y,m_moveLimit.y),
			0
		);
	}

	//ラジアンの角度を与えたら方向ベクトルを返す
	public static Vector3 GetDirection(float angle_Rad){
		return new Vector3(
			Mathf.Cos(angle_Rad),
			Mathf.Sin(angle_Rad),
			0
		);
	}

    // 引数に角度（ 0 ～ 360 ）を渡すとベクトルに変換して返す
    public static Vector3 GetDirection360(float angle360)
    {
        float angle = angle360 * Mathf.Deg2Rad;
        return new Vector3
        (
            Mathf.Cos(angle),
            Mathf.Sin(angle),
            0
        );
    }
    // 引数に角度（ 0 ～ 1.0f ）を渡すとベクトルに変換して返す
    public static Vector3 GetDirection1(float angle1)
    {
        float angle = angle1 / (3.14f * 2);
        return new Vector3
        (
            Mathf.Cos(angle),
            Mathf.Sin(angle),
            0
        );
    }
    // 引数に角度（ 0 ～ PI2 ）を渡すベクトルに変換して返す
    public static Vector3 GetDirectionPI2(float angle_pi2)
    {
        return new Vector3
        (
            Mathf.Cos(angle_pi2),
            Mathf.Sin(angle_pi2),
            0
        );
    }

    public static Sprite getSprite(string url)
    {
        return (Sprite)Resources.Load(url, typeof(Sprite));
    }

    // 初期値から最終値の倍率を変更する
    // num	現在の数値(sa～eaの値を入れる)
    // sa	変化前の初期値
    // ea	変化前の最終値
    // sb	変化後の初期値
    // eb	変化後の最終値
    // 使用例 10～100までの変化を0～1までの変化に倍率を変更する
    // cout << ChangeRate( 10, 10, 100, 0, 1 ) << endl;
   static public float ChangeRate(float num, float sa, float ea, float sb, float eb)
    {
        num = Mathf.Clamp(num, sa, ea);
        float a = (sb - eb) / (sa - ea);
        float b = sb - sa * a;
        return a * num + b;
    }

	public static float ChangeRateX(float num)
    {
		return Utils.ChangeRate(num,0.0f,100.0f, -7.1f,1.3f);
    }

	public static float ChangeRateY(float num)
    {
		return Utils.ChangeRate(num, 0.0f, 100.0f, 3.8f, -3.8f);
    }
}




public class CResourcesLoader<T> where T : UnityEngine.Object
{
	private Dictionary<string, T> ResourcesHandles = new Dictionary<string, T>();

	public bool LoadObject(string file_name)
	{
		string fn = Path.GetFileName(file_name);

		if (ResourcesHandles.ContainsKey(fn))
		{
			Debug.Log("LoadObject()で同じ名前のキーがありました");
			Debug.Log(file_name + "にある" + fn + "の名前を変更してください");
			return false;
		}
		else
		{
			T ob = Resources.Load<T>(file_name);
			if (ob)
			{
				ResourcesHandles.Add(fn, ob);
				return true;
			}
			else
			{
				Debug.Log("LoadObject()" + file_name + "の読み込みが失敗しました");
				return false;
			}
		}
	}

	public bool LoadAllObjects(string file_name)
	{
		T[] obs = Resources.LoadAll<T>(file_name);

		if (obs.Length <= 0)
		{
			return false;
		}

		foreach (T ob in obs)
		{
			if (ResourcesHandles.ContainsKey(ob.name))
			{
				Debug.Log("LoadAllObject()で同じ名前のキーがありました");
				Debug.Log(file_name + "にある" + ob.name + "の名前を変更してください");
			}
			else
			{
				ResourcesHandles.Add(ob.name, ob);
			}
		}
		return true;
	}

	public T GetObjectHandle(string name)
	{
		if (ResourcesHandles.ContainsKey(name))
		{
			return ResourcesHandles[name];
		}
		else
		{
			return null;
		}
	}

	public bool ContainsKey(string key_name)
	{
		return ResourcesHandles.ContainsKey(key_name);
	}
}
public class CSoundPlayer
{
	static GameObject SoundPlayerObj, BGMPlayerObj;
	static CResourcesLoader<AudioClip> ResourcesLoader = new CResourcesLoader<AudioClip>();
	static AudioSource SoundAudioSource, BGMAudioSource;
	static CFadeTimer FadeTimer;

	public static IEnumerator SetFadeTimer(float start_val, float end_val, float end_time)
	{
		FadeTimer = new CFadeTimer(start_val, end_val, end_time);

		while (true)
		{
			float t = FadeTimer.CalcTime();
			BGMAudioSource.volume = t;

			if (t <= 0.0f)
			{
				BGMAudioSource.Stop();
				yield break;
			}
			else
			{
				yield return new WaitForSeconds(Time.deltaTime);
			}
		}
	}

	public static bool LoadAudioClip(string audio_path)
	{
		return ResourcesLoader.LoadObject(audio_path);
	}

	public static bool LoadAllSounds(string audio_path)
	{
		return ResourcesLoader.LoadAllObjects(audio_path);
	}

	public static bool StopSound(string se_name)
	{
		GameObject sound_obj = GameObject.Find(se_name);
		if (sound_obj)
		{
			AudioSource aus = sound_obj.GetComponent<AudioSource>();
			if (aus)
			{
				aus.Stop();
				return true;
			}
		}
		return false;
	}

	public static AudioClip GetAudioClip(string se_name)
	{
		if (ResourcesLoader.ContainsKey(se_name) == false)
		{
			return null;
		}
		else
		{
			return ResourcesLoader.GetObjectHandle(se_name);
		}
	}

	public static bool PlaySound(string se_name, bool se_flag = true)
	{
		if (ResourcesLoader.ContainsKey(se_name) == false)
		{
			return false;
		}

		if (se_flag)
		{
			if (SoundPlayerObj == null)
			{
				SoundPlayerObj = new GameObject("SoundPlayer");
				SoundAudioSource = SoundPlayerObj.AddComponent<AudioSource>();
			}
			SoundAudioSource.PlayOneShot(ResourcesLoader.GetObjectHandle(se_name));
		}
		else
		{
			if (BGMPlayerObj == null)
			{
				BGMPlayerObj = new GameObject("BGMPlayer");
				BGMAudioSource = BGMPlayerObj.AddComponent<AudioSource>();
				BGMAudioSource.clip = ResourcesLoader.GetObjectHandle(se_name);
				BGMAudioSource.volume = 1.0f;
				BGMAudioSource.loop = true;
				BGMAudioSource.Play();
			}
			else
			{
				if (BGMAudioSource)
				{
					if (BGMAudioSource.isPlaying)
					{
						BGMAudioSource.Stop();
					}
					else
					{
						BGMAudioSource.Play();
					}
				}
				else
				{
					BGMAudioSource = BGMPlayerObj.AddComponent<AudioSource>();
					if (BGMAudioSource)
					{
						BGMAudioSource.clip = ResourcesLoader.GetObjectHandle(se_name);
						BGMAudioSource.volume = 1.0f;
						BGMAudioSource.loop = true;
						BGMAudioSource.Play();
					}
				}
			}
		}

		return true;
	}
}

// 指定した時間である値からある値まで数を変化させる
// start_val 変化させたい数の初期値
// end_val	 変化させたい数の終了値
// end_time  終了時間(end_timeかけて終了させる)
// 使用手順
// 1 宣言する
// CFadeTimer FadeTimer;
// 2 初期化する
// FadeTimer = new CFadeTimer( 100, 1000, 3 );
// 3 毎ループ行う処理を追加する
// print( FadeTimer.CalcTime () );
public class CFadeTimer
{
	private bool Flag = true;
	private float StartVal, EndVal, StartTime, EndTime, Delta, Result = 0.0f;

	public CFadeTimer(float start_val, float end_val, float end_time)
	{
		StartVal = start_val;
		EndVal = end_val;
		EndTime = end_time;
		StartTime = Time.realtimeSinceStartup;
		Delta = (EndVal - StartVal) / EndTime;
	}

	public float CalcTime()
	{
		if (Flag)
		{
			float t = Time.realtimeSinceStartup - StartTime;
			if (EndTime <= t)
			{
				Flag = false;
				Result = EndVal;
				return Result;
			}
			Result = Delta * t + StartVal;
			return Result;
		}
		else
		{
			return -1.0f;
		}
	}
}
