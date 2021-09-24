using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Raw_System : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string rawName;

    Friends_RespwanManger friends_RespwanManger;
  
    void Start()
    {
        friends_RespwanManger = GameObject.Find("Friends_RespwanManger").GetComponent<Friends_RespwanManger>();
    }

    // Update is called once per frame
    void Update()
    {
        CallArmy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.gameObject.name)
        {
            case  ("Lane_UP"):
                Debug.Log(" iam in raw  " + collision.gameObject.name);
                rawName = "Lane_UP";
                break;
                

            case "Lane_Mid":
                Debug.Log(" iam in raw  " + collision.gameObject.name);
                rawName = "Lane_Mid";
                break;

            case "Lane_Down":
                Debug.Log(" iam in raw  " + collision.gameObject.name);
                rawName = "Lane_Down";
                break;

            default:
             
                break;

        }
        

       
    }

    void CallArmy()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&rawName== "Lane_UP")
        {
            Debug.Log(" iam calling Army in " + rawName);

            friends_RespwanManger.Respwan();
        }
        if (Input.GetKeyDown(KeyCode.Space) && rawName == "Lane_Mid")
        {
            Debug.Log(" iam calling Army in " + rawName);
            friends_RespwanManger.Respwan();
        }
        if (Input.GetKeyDown(KeyCode.Space) && rawName == "Lane_Down")
        {
            Debug.Log(" iam calling Army in " + rawName);
            friends_RespwanManger.Respwan();
        }
    }
   
}
