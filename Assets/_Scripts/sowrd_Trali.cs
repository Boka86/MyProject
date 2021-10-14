using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sowrd_Trali : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject sowrdTrail;
    void Start()
    {
        sowrdTrail = GameObject.Find("Trail");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SowrdTrailEnable()
    {
        sowrdTrail.SetActive(true);
    }
    public void SowrdTrailDisable()
    {
        sowrdTrail.SetActive(false);
    }
}
