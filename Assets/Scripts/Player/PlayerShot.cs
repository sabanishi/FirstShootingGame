using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
	
	public Vector3 m_velocity;

	private float _screenTop;
	private float _screenBottom;
	private float _screenLeft;
	private float _screenRight;
	private Transform _tf;
    // Start is called before the first frame update
    void Awake() {
		_tf = this.transform;

		_screenTop = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;
		_screenBottom = -Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;
		_screenRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;
		_screenLeft = -Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;
	}

  	 // Update is called once per frame
  	void FixedUpdate() { 
        if ((_tf.position.y > _screenTop + 1)||(_tf.position.y<_screenBottom-1)
			||(_tf.position.x > _screenRight + 1) || (_tf.position.x < _screenLeft - 1)
			) {
			this.gameObject.SetActive(false);
		}
		transform.localPosition += m_velocity;
	}

	public void Init(float angle, float speed)
	{
		var direction = Utils.GetDirection(angle);
		m_velocity = direction * speed;

		var angles = transform.localEulerAngles;
		angles.z = angle;
		transform.localEulerAngles = angles;
	}
}
