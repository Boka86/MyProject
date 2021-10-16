using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Raw_System : MonoBehaviour
{
    // it dected the lane player in and communcate with teh Friend respwanManger 
    // Start is called before the first frame update
    [SerializeField] string rawName;
    [SerializeField] public float currentMana;
    [SerializeField] public float maxMana;
    [SerializeField] float manaIncreaseRate;
    [SerializeField] float waitTimerMana;
    [SerializeField] GameObject manaBar;
    [SerializeField] TextMeshProUGUI manaText;
    // [SerializeField] GameObject ArrowRaw;
    GameObject game_Diffcluty;
    Game_Diffcluty game_DiffclutyStatus;
    bool canCallSummonAgain;
    [SerializeField] Image mana_Image;
    AudioSource source;
    [SerializeField] AudioClip ManaSound;
    Vector3 localScale;

    Friends_RespwanManger friends_RespwanManger;

    private void Awake()
    {
        if (game_Diffcluty == null)
        {
            game_Diffcluty = GameObject.Find("game_Diffcluty");
            game_DiffclutyStatus = game_Diffcluty.GetComponent<Game_Diffcluty>();
        }
    }

    void Start()
    {
        canCallSummonAgain = true;
        source = GetComponent<AudioSource>();
        friends_RespwanManger = GameObject.Find("Friends_RespwanManger").GetComponent<Friends_RespwanManger>();
        currentMana = maxMana;
        localScale = manaBar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        CallArmy();
        ManaIncreaseRate();
        GameDiffculty();
        localScale.x = currentMana;
        manaBar.transform.localScale = localScale;
        manaText.text = " Mana " + currentMana.ToString("F1");
     
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        switch ( collision.gameObject.name)
        {
            case  ("Lane_UP"):
               //ArrowRaw.SetActive(true);
                rawName = "Lane_UP";
               // ArrowRaw.SetActive(true);
                friends_RespwanManger.currentRespwanPoint.position = friends_RespwanManger.laneUp.position;
                break;
                

            case "Lane_Mid":
                
                rawName = "Lane_Mid";
               // ArrowRaw.SetActive(true);
                friends_RespwanManger.currentRespwanPoint.position = friends_RespwanManger.laneMid.position;
                break;

            case "Lane_Down":
               // ArrowRaw.SetActive(true);
                friends_RespwanManger.currentRespwanPoint.position = friends_RespwanManger.laneDown.position;
                rawName = "Lane_Down";
                break;

            default:
                rawName = "FakeLane";
                //ArrowRaw.SetActive(false);
                break;
                
        }
        

       
    }

    void CallArmy()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rawName == "Lane_UP" && currentMana >= friends_RespwanManger.manaCost && canCallSummonAgain)
        {
            canCallSummonAgain = false;
            friends_RespwanManger.Respwan();
            currentMana = currentMana - friends_RespwanManger.manaCost;

            mana_Image.GetComponent<Image>().color = Color.red;
            StartCoroutine("RevertColor");
            StartCoroutine("DelaySummon");
        }
        if (Input.GetKeyDown(KeyCode.Space) && rawName == "Lane_Mid" && currentMana >= friends_RespwanManger.manaCost && canCallSummonAgain)
        {
            canCallSummonAgain = false;
         
            friends_RespwanManger.Respwan();
            currentMana = currentMana - friends_RespwanManger.manaCost;
            mana_Image.GetComponent<Image>().color = Color.red;
            StartCoroutine("RevertColor");
            StartCoroutine("DelaySummon");

        }
        if (Input.GetKeyDown(KeyCode.Space) && rawName == "Lane_Down" && currentMana >= friends_RespwanManger.manaCost && canCallSummonAgain)
        {
            canCallSummonAgain = false;
            friends_RespwanManger.Respwan();
            currentMana = currentMana - friends_RespwanManger.manaCost;
            mana_Image.GetComponent<Image>().color = Color.red;
            StartCoroutine("RevertColor");
            StartCoroutine("DelaySummon");

        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            source.PlayOneShot(ManaSound, 10);
        

        }
    }
    void ManaIncreaseRate()
    {

        if (currentMana < maxMana)
        {
            StartCoroutine("DelayRate");
           
        }

        if (currentMana > maxMana)
        {
            // prevent to reach bigger than Max mana when collecting Mana Potion
           currentMana = maxMana;
        }
    }
   

    IEnumerator DelayRate ()
    {
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        yield return new WaitForSeconds(waitTimerMana);
        
        currentMana += manaIncreaseRate*Time.deltaTime;
        
    }
    IEnumerator RevertColor()
    {
        yield return new WaitForSeconds(0.2f);
        mana_Image.GetComponent<Image>().color = Color.white;
    }
    IEnumerator DelaySummon()
    {
        yield return new WaitForSeconds(0.5f);
        canCallSummonAgain = true;
    }


    void GameDiffculty()
    {
        if (game_DiffclutyStatus.easy)
        {
            manaIncreaseRate = 0.5f;
           
        }
        if (game_DiffclutyStatus.normal)
        {
            manaIncreaseRate = 0.4f;
        }
        if (game_DiffclutyStatus.hard)
        {
            manaIncreaseRate = 0.2f;
        }
    }
}
