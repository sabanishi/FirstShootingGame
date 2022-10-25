using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour{
	public float speed;//速度
	public PlayerShot shotPrefab;//弾
	public float m_shotSpeed;//弾速
	private float m_shotAngleRange;
	public float firstShotAngleRange;//高速移動時の射幅
	public float secondShotAngleRange;//低速移動時の射幅
	private float m_shotTimer;
	public int m_shotCount;//弾の多さ
	public float m_shotInterval;//弾打つ間隔
	private bool isSlow;
	public int life;//残機
	private int nonDamageTime;
	public int getNonDamageTime()
    {
		return nonDamageTime;
    }

	private PlayerShotPool _pool;

	private GameObject playerHit;

	public Sprite image1;
	public Sprite image2;
	private SpriteRenderer Crenderer;
	private MGameManager gameManager;

    void Start()
    {
		_pool = GameObject.Find("playerShotPool").GetComponent<PlayerShotPool>();
		Crenderer = this.gameObject.GetComponent<SpriteRenderer>();
		image1=(Sprite)Resources.Load("image/player", typeof(Sprite));
		image2=(Sprite)Resources.Load("image/player2", typeof(Sprite));
    }

    void FixedUpdate() {
		if (this.gameManager == null)
        {
			this.gameManager = GameObject.Find("GameManager").GetComponent<MGameManager>();
        }
		float vSpeed=0;//垂直方向
		float hSpeed=0;//水平方向
		int count = 0;
        if (Input.GetKey(KeyCode.LeftShift)){
			isSlow = true;
        }
        else{
			isSlow = false;
        }

 		if(Input.GetKey(KeyCode.LeftArrow)){
			count++;
			hSpeed = -this.speed;
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			count++;
			hSpeed = this.speed;

		}	
		if(Input.GetKey(KeyCode.UpArrow)){
			count++;
			vSpeed = this.speed;

		}
		if (Input.GetKey(KeyCode.DownArrow)){
			count++;
			vSpeed = -this.speed;

		}
		if (count >= 2){
			vSpeed *= (float)Math.Sqrt(2) / 2;
			hSpeed *= (float)Math.Sqrt(2) / 2;
		}
		if (isSlow)
		{
			vSpeed /= 2;
			hSpeed /= 2;
			this.m_shotAngleRange = this.secondShotAngleRange;
        }else{
			this.m_shotAngleRange = this.firstShotAngleRange;
        }
	
		transform.Translate(hSpeed,vSpeed, 0,Space.World);

		transform.localPosition=Utils.ClampPosition(transform.localPosition);	

		m_shotTimer +=Time.deltaTime;
		if(m_shotTimer>=m_shotInterval){
			if(Input.GetKey(KeyCode.Z)){
				m_shotTimer=0;
                if (this.isSlow)
                {
					Shot(this.transform.localEulerAngles.z+90, m_shotAngleRange, m_shotSpeed, m_shotCount,1);
				}
                else
                {
					Shot(this.transform.localEulerAngles.z+90, m_shotAngleRange, m_shotSpeed, m_shotCount,0);
				}		
			}
		}

        if ((!isSlow) && (!Input.GetKey(KeyCode.Z))&&count<=1) {
			var angles = transform.localEulerAngles;
			float oldAngle = angles.z;
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				angles.z =90;
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				angles.z = -90;

			}
			if (Input.GetKey(KeyCode.UpArrow))
			{
				angles.z = 0;

			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				angles.z = 180;

			}
			transform.localEulerAngles = angles;
		}

		if (this.playerHit == null)
		{
			this.playerHit = GameObject.Find("playerHit").gameObject;

		}
		else
		{
			this.playerHit.transform.localPosition = this.transform.localPosition;
		}

		if (this.nonDamageTime > 0)
		{
			this.nonDamageTime--;
			this.Crenderer.sprite= this.image2;
		}
		else
		{
			this.nonDamageTime = 0;
			this.Crenderer.sprite = this.image1;
		}
	}

	private void Shot(float angleBase, float angleRange, float speed, int count,int shotType) {
		var pos = transform.localPosition;
		var rot = transform.localRotation;

		if (1 < count) {
			for (int i = 0; i < count; i++) {
				var angle = angleBase + angleRange * ((float)i / (count - 1) - 0.5f);
				PlayerShot shot = _pool.GetBurret(shotType);
				shot.transform.localPosition = this.transform.position;
				shot.Init(angle*Mathf.Deg2Rad, speed);
			}
		} else if (count == 1) {
			PlayerShot shot = _pool.GetBurret(shotType);
			shot.transform.localPosition = this.transform.position;
			shot.Init(angleBase, speed);

		}
	}

	public void Damage()
	{
		if (this.life > 0)
		{
            if (this.nonDamageTime == 0)
            {
				this.nonDamageTime = 180;
				this.life--;
				this.gameManager.deleteAllEnemyShot();
			}
		}
		else
		{
			gameObject.SetActive(false);
		}
	}
}

