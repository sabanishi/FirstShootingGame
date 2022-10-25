using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float m_interval;

    private float m_timer;

    // Update is called once per frame
    void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer < m_interval) return;

        /*m_timer = 0;
        var enemyPrefab = m_enemyPrefabs[0];
        var enemy = Instantiate(enemyPrefab);
        enemy.Init();*/
    }
}
