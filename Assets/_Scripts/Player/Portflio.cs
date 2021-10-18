using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Portflio : MonoBehaviour
{
    
    [SerializeField] bool tvIsOn;
    [SerializeField] bool canOpenTv;
    bool canChangeTV;
    [SerializeField] TextMeshProUGUI  PressE  ;
    [SerializeField] GameObject tvSprite_mainChannel;
    [SerializeField] GameObject tvSprite_Project;
    [SerializeField] GameObject TV_Loding_Screen;
    AudioSource source;
    [SerializeField]   AudioClip tvOn;
   [SerializeField] Animator anim;
    [SerializeField] string youTube_URL;
    void Start()

    {
        TV_Loding_Screen.SetActive(false);
       // anim = GameObject.Find("TV_Loding_Screen").GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        tvSprite_Project.SetActive(false);
        tvIsOn = false;
        canOpenTv = false;
        canChangeTV = false;
        PressE.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        ControlTv();
        LittelWarProject();
        ExitGame();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TV"))
        {
            PressE.text = " Press E to Open TV ";
            canOpenTv = true;
            
        }
         if(other.gameObject.tag!=("TV"))
        {
            PressE.text = " Go to TV ";
            tvIsOn = false;
            canChangeTV = false;
            tvSprite_Project.gameObject.SetActive(false);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TV"))
        {
            canOpenTv = false;

        }
    }

    void ControlTv()
    {
        if (tvIsOn&&canChangeTV==false)
        {
            
            tvSprite_mainChannel.gameObject.SetActive(true);
            PressE.text = "";


        }
        else if (tvIsOn != true)
        {
            tvSprite_mainChannel.gameObject.SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.E) && canOpenTv)
        {
            tvIsOn = !tvIsOn;
            source.PlayOneShot(tvOn, 5);

        }
        if (tvIsOn == true && Input.GetKeyDown(KeyCode.Alpha1))
        {
            canChangeTV = true;
            tvSprite_mainChannel.gameObject.SetActive(false);
            tvSprite_Project.gameObject.SetActive(true);
            source.PlayOneShot(tvOn, 5);


        }
        if (tvIsOn == true && Input.GetKeyDown(KeyCode.Alpha2))
        {
            canChangeTV = true;
            OpenYouTube();
            source.PlayOneShot(tvOn, 5);


        }
    }
    //  Project Load Bulid
    void LittelWarProject()
    {

        if (tvSprite_Project.activeSelf==true&&Input.GetKeyDown(KeyCode.E))
        {
            // littel War Project
            TV_Loding_Screen.SetActive(true);
            anim.SetTrigger("TV_Loding_Screen");

            StartCoroutine("ChannelSwap");
        }

        
    }

    void ExitGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    IEnumerator ChannelSwap()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level_00");

    }

    void OpenYouTube()
    {
        Application.OpenURL(youTube_URL);
    }
}
