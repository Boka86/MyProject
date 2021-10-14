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
    
    bool canCallSummonAgain;
    [SerializeField] Image mana_Image;
    AudioSource source;
    [SerializeField] AudioClip ManaSound;
    Vector3 localScale;

    Friends_RespwanManger friends_RespwanManger;
  
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
        localScale.x = currentMana;
        manaBar.transform.localScale = localScale;
        manaText.text = " Mana " + currentMana.ToString("F1");
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch ( collision.gameObject.name)
        {
            case  ("Lane_UP"):
                Debug.Log(" iam in raw  " + collision.gameObject.name);
                rawName = "Lane_UP";
                friends_RespwanManger.currentRespwanPoint.position = friends_RespwanManger.laneUp.position;
                break;
                

            case "Lane_Mid":
                Debug.Log(" iam in raw  " + collision.gameObject.name);
                rawName = "Lane_Mid";
                friends_RespwanManger.currentRespwanPoint.position = friends_RespwanManger.laneMid.position;
                break;

            case "Lane_Down":
                Debug.Log(" iam in raw  " + collision.gameObject.name);
                friends_RespwanManger.currentRespwanPoint.position = friends_RespwanManger.laneDown.position;
                rawName = "Lane_Down";
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
}
