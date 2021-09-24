using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friends_RespwanManger : MonoBehaviour
{
    [SerializeField] GameObject[] solidersGameObjects;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Respwan()
    {
        Debug.Log(" i called respwan funaction  ");
        Instantiate(solidersGameObjects[0], transform.position, Quaternion.identity);
    }
}
