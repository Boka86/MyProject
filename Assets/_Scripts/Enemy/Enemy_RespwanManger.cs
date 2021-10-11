using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RespwanManger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Transform  [] respawnPoints;
    [SerializeField] GameObject [] enemyGo;
    [SerializeField] float waitTime;
    [SerializeField] float startTime;
    [SerializeField] float repeatTime;
  
    
    int enemyGoRandomizer;
    int enemyPosRandomizer;
    void Start()
    {
        InvokeRepeating("RespwanEnemy", startTime, repeatTime);
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
