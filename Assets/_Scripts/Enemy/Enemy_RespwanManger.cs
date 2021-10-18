using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RespwanManger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Transform  [] respawnPoints;
    [SerializeField] GameObject [] enemyGo;
   
    int enemyGoRandomizer;
    int enemyPosRandomizer;
    private void Awake()
    {
        
    }
    void Start()
    {
       
        InvokeRepeating("RespwanEnemy", Game_Diffcluty.startTime, Game_Diffcluty.repeatTime);
    }

    // Update is called once per frame
    void Update()
    {
    
    }


    void RespwanEnemy ()
    {

        enemyGoRandomizer = Random.Range(0, 3);
        enemyPosRandomizer = Random.Range(0, 3);
        Instantiate(enemyGo[enemyGoRandomizer], respawnPoints[enemyPosRandomizer].transform.position, Quaternion.identity);

    }

   




}
