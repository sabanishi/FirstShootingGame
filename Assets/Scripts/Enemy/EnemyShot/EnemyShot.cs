using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    protected float Speed,Angle;
    private Vector3 m_velocity;
    void Start()
    {

    }
    void FixedUpdate()
    {
        Vector3 position = transform.position;
        position.x += m_velocity.x;
        position.y += m_velocity.y;
        position.z += m_velocity.z;
        transform.position = position;

        // 弾が進行方向を向くようにする
        var angles = transform.localEulerAngles;
        angles.z = Angle * Mathf.Rad2Deg-90f;
        transform.localEulerAngles = angles;
        Move();
    }

    public void Init(float angle, float speed)
    {
        var direction = Utils.GetDirection(angle);
        m_velocity = direction * speed;
        Speed = speed;

        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;
        Angle = angle;
    }

    protected virtual void Move()
    {
        if (Utils.IsOut(transform.position))
        {
            this.gameObject.SetActive(false);
        }
    }
}
