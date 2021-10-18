using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Diffcluty : MonoBehaviour

{
    // Start is called before the first frame update
    [SerializeField] public bool easy;
    [SerializeField] public bool normal;
    [SerializeField] public bool hard;
    Diccult_text diccult;
    [SerializeField] public static float startTime;
    [SerializeField] public static float repeatTime;


    private void Start()
    {
        diccult = GameObject.Find("GameObject").GetComponent<Diccult_text>();
      if(hard==true)
        {
            startTime = 1f;
            repeatTime = 2f;
            diccult.currentGameDfcculity.text = diccult.hard;
        }
    }
    private void Update()
    {
        Debug.Log(("Start time  =") + startTime + ("Repate Time = ") + repeatTime);
    }
    public void Easy()
    {
        easy = true;
        normal = false;
        hard = false;
        diccult.currentGameDfcculity.text = diccult.easy;
        
        startTime  =4f;
        repeatTime = 5f;
     
    }
    public void Normal()
    {
        startTime = 2f;
        repeatTime = 3f;
        easy = false;
        normal = true;
        hard = false;

        diccult.currentGameDfcculity.text = diccult.normal;
      
    }
    public void Hard()
    {
        startTime = 1f;
        repeatTime = 2f;
        easy = false;
        normal = false;
        hard = true;
        diccult.currentGameDfcculity.text = diccult.hard;
        

    }
}
