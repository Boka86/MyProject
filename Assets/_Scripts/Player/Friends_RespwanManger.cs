using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Transform laneUp;
    public Transform laneMid;
    public Transform laneDown;
    public Transform currentRespwanPoint;
    void Start()
    {
        freindNumber0_panel.color = Color.green;

    }

    // Update is called once per frame
    void Update()
    {
        
        ChooseFriendNumber();
    }


    public void Respwan()
    {
        Debug.Log(" i called respwan funaction  ");
        Instantiate(solidersGameObjects[CurrentFriendNumber], currentRespwanPoint.position, Quaternion.identity);
    }

    void ChooseFriendNumber()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentFriendNumber = freindNumber0;
            freindNumber0_panel.color = Color.green;
            freindNumber1_panel.color = Color.red;
            freindNumber2_panel.color = Color.red;

        }
        
       
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentFriendNumber = freindNumber1;
            freindNumber1_panel.color = Color.green;
            freindNumber0_panel.color = Color.red;
            freindNumber2_panel.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentFriendNumber = freindNumber2;
            freindNumber2_panel.color = Color.green;
            freindNumber0_panel.color = Color.red;
            freindNumber1_panel.color = Color.red;
        }

    }
}
