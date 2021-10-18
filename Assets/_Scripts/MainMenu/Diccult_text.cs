using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Diccult_text : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI currentGameDfcculity;
    public string easy;
    public string normal;
    public string hard;
    void Start()
    {
        easy = "(Game Play) EASY";
        normal = "(Game Play) Normal";
        hard = "(Game Play) Hard";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
