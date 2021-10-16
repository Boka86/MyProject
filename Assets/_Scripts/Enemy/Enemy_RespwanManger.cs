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
    GameObject game_Diffcluty;
    Game_Diffcluty game_DiffclutyStatus;

    int enemyGoRandomizer;
    int enemyPosRandomizer;
    private void Awake()
    {
        if(game_Diffcluty==null)
        {
            game_Diffcluty = GameObject.Find("game_Diffcluty");
            game_DiffclutyStatus = game_Diffcluty.GetComponent<Game_Diffcluty>();
        }
    }
    void Start()
    {
        InvokeRepeating("RespwanEnemy", startTime, repeatTime);
    }

    // Update is called once per frame
    void Update()
    {
        GameDiffculty();
    }


    void RespwanEnemy ()
    {

        enemyGoRandomizer = Random.Range(0, 3);
        enemyPosRandomizer = Random.Range(0, 3);
        Instantiate(enemyGo[enemyGoRandomizer], respawnPoints[enemyPosRandomizer].transform.position, Quaternion.identity);

    }

    void GameDiffculty()
    {
        if(game_DiffclutyStatus.easy)
        {
            startTime = 3f;
            repeatTime = 5f;
        }
        if (game_DiffclutyStatus.normal)
        {
            startTime = 2f;
            repeatTime = 3f;
        }
        if (game_DiffclutyStatus.hard)
        {
            startTime = 2f;
            repeatTime = 0.5f;
        }
    }




}
