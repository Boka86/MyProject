using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Bar_Script_F : MonoBehaviour
{
    Vector3 localScale;
    Player player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = player.health;
        transform.localScale = localScale;

        if (player.health < 0.1)
        {
            // prevent negtive x on Scale
            player.health = 0f;
        }
    }
}
