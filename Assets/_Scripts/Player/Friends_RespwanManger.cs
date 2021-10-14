using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Friends_RespwanManger : MonoBehaviour
{
    [SerializeField] GameObject[] solidersGameObjects;
    [SerializeField] int freindNumber0;
    [SerializeField] int freindNumber1;
    [SerializeField] int freindNumber2;
    [SerializeField] int CurrentFriendNumber;
    [SerializeField] Image freindNumber0_panel;
    [SerializeField] Image freindNumber1_panel;
    [SerializeField] Image freindNumber2_panel;
    [SerializeField] TextMeshProUGUI freindNumber0_Deatils_Health;
    [SerializeField] TextMeshProUGUI freindNumber1_Deatils_Health;
    [SerializeField] TextMeshProUGUI freindNumber2_Deatils_Health;
    [SerializeField] TextMeshProUGUI freindNumber0_Deatils_AP;
    [SerializeField] TextMeshProUGUI freindNumber1_Deatils_AP;
    [SerializeField] TextMeshProUGUI freindNumber2_Deatils_AP;
    [SerializeField] TextMeshProUGUI freindNumber0_Deatils_MC;
    [SerializeField] TextMeshProUGUI freindNumber1_Deatils_MC;
    [SerializeField] TextMeshProUGUI freindNumber2_Deatils_MC;
    [SerializeField] float freindNumber0_Health;
    [SerializeField] float freindNumber1_Health;
    [SerializeField] float freindNumber2_Health;
    [SerializeField] float freindNumber0_AP;
    [SerializeField] float freindNumber1_AP;
    [SerializeField] float freindNumber2_AP;
    [SerializeField] public float freindNumber0_MC;
    [SerializeField] public float freindNumber1_MC;
    [SerializeField] public float freindNumber2_MC;
    [SerializeField] public float manaCost;
    AudioSource source;
   [SerializeField] AudioClip selectSound;
    bool noIsAutoActive;

    public Transform laneUp;
    public Transform laneMid;
    public Transform laneDown;
    public Transform currentRespwanPoint;
    void Start()
    {
        noIsAutoActive = true;// i created this so it automatic start the game with NO 1 as active choice
        
        freindNumber0_panel.color = Color.green;
        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
        ChooseFriendNumber();
    }


    public void Respwan()
    {
        Instantiate(solidersGameObjects[CurrentFriendNumber], currentRespwanPoint.position, Quaternion.identity);
    }

    void ChooseFriendNumber()
    {
        if(noIsAutoActive)
        {
            manaCost = freindNumber0_MC;
            noIsAutoActive = false;
            CurrentFriendNumber = freindNumber0;
            freindNumber0_Deatils_Health.text = " Health " + freindNumber0_Health;
            freindNumber0_Deatils_MC.text = "MC " + freindNumber0_MC;
            freindNumber0_Deatils_AP.text = "AP " + freindNumber0_AP;
            freindNumber0_Deatils_MC.gameObject.SetActive(true);
            freindNumber1_Deatils_MC.gameObject.SetActive(false);
            freindNumber2_Deatils_MC.gameObject.SetActive(false);
            freindNumber0_Deatils_AP.gameObject.SetActive(true);
            freindNumber1_Deatils_AP.gameObject.SetActive(false);
            freindNumber2_Deatils_AP.gameObject.SetActive(false);
            freindNumber0_Deatils_Health.gameObject.SetActive(true);
            freindNumber1_Deatils_Health.gameObject.SetActive(false);
            freindNumber2_Deatils_Health.gameObject.SetActive(false);

            freindNumber0_panel.color = Color.green;
            freindNumber1_panel.color = Color.red;
            freindNumber2_panel.color = Color.red;
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            source.PlayOneShot(selectSound,3);
            manaCost = freindNumber0_MC;
            CurrentFriendNumber = freindNumber0;
            freindNumber0_Deatils_Health.text = " Health " + freindNumber0_Health;
            freindNumber0_Deatils_MC.text = "MC " + freindNumber0_MC;
            freindNumber0_Deatils_AP.text = "AP " + freindNumber0_AP;
            freindNumber0_Deatils_MC.gameObject.SetActive(true);
            freindNumber1_Deatils_MC.gameObject.SetActive(false);
            freindNumber2_Deatils_MC.gameObject.SetActive(false);
            freindNumber0_Deatils_AP.gameObject.SetActive(true);
            freindNumber1_Deatils_AP.gameObject.SetActive(false);
            freindNumber2_Deatils_AP.gameObject.SetActive(false);
            freindNumber0_Deatils_Health.gameObject.SetActive(true);
            freindNumber1_Deatils_Health.gameObject.SetActive(false);
            freindNumber2_Deatils_Health.gameObject.SetActive(false);
           
            freindNumber0_panel.color = Color.green;
            freindNumber1_panel.color = Color.red;
            freindNumber2_panel.color = Color.red;


        }
        
       
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            source.PlayOneShot(selectSound,3);
            manaCost = freindNumber1_MC;
            freindNumber1_Deatils_Health.text = " Health " + freindNumber1_Health;
            freindNumber1_Deatils_AP.text = "AP " + freindNumber1_AP;
            freindNumber1_Deatils_MC.text = "MC " + freindNumber1_MC;
            freindNumber0_Deatils_MC.gameObject.SetActive(false);
            freindNumber1_Deatils_MC.gameObject.SetActive(true);
            freindNumber2_Deatils_MC.gameObject.SetActive(false);
            freindNumber0_Deatils_AP.gameObject.SetActive(false);
            freindNumber1_Deatils_AP.gameObject.SetActive(true);
            freindNumber2_Deatils_AP.gameObject.SetActive(false);
            freindNumber0_Deatils_Health.gameObject.SetActive(false);
            freindNumber1_Deatils_Health.gameObject.SetActive(true);
            freindNumber2_Deatils_Health.gameObject.SetActive(false);
           
            CurrentFriendNumber = freindNumber1;
            freindNumber1_panel.color = Color.green;
            freindNumber0_panel.color = Color.red;
            freindNumber2_panel.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            source.PlayOneShot(selectSound,3);
            manaCost = freindNumber2_MC;
            freindNumber2_Deatils_Health.text = " Health " + freindNumber2_Health;
            freindNumber2_Deatils_AP.text = "AP " + freindNumber2_AP;
            freindNumber2_Deatils_MC.text = "MC " + freindNumber2_MC;
            freindNumber0_Deatils_MC.gameObject.SetActive(false);
            freindNumber1_Deatils_MC.gameObject.SetActive(false);
            freindNumber2_Deatils_MC.gameObject.SetActive(true);
            freindNumber0_Deatils_AP.gameObject.SetActive(false);
            freindNumber1_Deatils_AP.gameObject.SetActive(false);
            freindNumber2_Deatils_AP.gameObject.SetActive(true);
            freindNumber0_Deatils_Health.gameObject.SetActive(false);
            freindNumber1_Deatils_Health.gameObject.SetActive(false);
            freindNumber2_Deatils_Health.gameObject.SetActive(true);

            CurrentFriendNumber = freindNumber2;
            freindNumber2_panel.color = Color.green;
            freindNumber0_panel.color = Color.red;
            freindNumber1_panel.color = Color.red;
        }

    }
   
}
