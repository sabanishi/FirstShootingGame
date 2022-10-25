using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            player.Damage();
        }
        if (collision.name.Contains("enemyShot"))
        {
            this.player.Damage();
        }
    }
}
