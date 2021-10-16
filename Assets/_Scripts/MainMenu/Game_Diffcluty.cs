using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Game_Diffcluty : MonoBehaviour
    
{
    // Start is called before the first frame update
    [SerializeField] public bool easy;
    [SerializeField] public bool normal;
    [SerializeField] public bool hard;
    [SerializeField] TextMeshProUGUI currentGameDfcculity;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }
    void Start()
    {
               DontDestroyOnLoad(this.gameObject);
        currentGameDfcculity.text = "(Game Play) Normal";
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Easy()
    {
        easy = true;
        normal = false;
        hard = false;
        currentGameDfcculity.text = "(Game Play) EASY";
    }
    public void Normal()
    {
        
        easy = false;
        normal = true;
        hard = false;
        currentGameDfcculity.text = "(Game Play) Normal";
    }
    public void Hard()
    {
        currentGameDfcculity.text = "(Game Play) Hard";
        easy = false;
        normal = false;
        hard = true;
    }
}
