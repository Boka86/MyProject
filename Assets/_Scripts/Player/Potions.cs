using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    Player player;
    Player_Raw_System player_Raw_System;
    [SerializeField] int healthIncreaseRate;
    [SerializeField] int manaIncreaseRate;
    bool canCallForSupport;
    void Start()
    {
        Destroy(gameObject, 10f);
        player = GameObject.Find("Player").GetComponent<Player>();
        player_Raw_System = GameObject.Find("Player").GetComponent<Player_Raw_System>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.name==("Player")&&this.gameObject.tag==("Health_Potion"))
        {
            player.health = player.health + healthIncreaseRate;
            gameObject.SetActive(false);
            Destroy(gameObject, 5f);
            if (player.health>5)
            {
                // prevent to reach bigger than Max Health
                player.health = 5;
            }
            
           
        }


        else if(coll.gameObject.name == ("Player") && this.gameObject.tag == ("Mana_Potion"))
        {
          
            player_Raw_System.currentMana = player_Raw_System.currentMana + manaIncreaseRate;
            gameObject.SetActive(false);
            Destroy(gameObject, 5f);

            
        }
    }

    IEnumerator ReactiveCallForSupport()
    {
        yield return new WaitForSeconds(5f);
        canCallForSupport = true;
    }
}
