using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    bool isPaused;
    bool lose;
    bool win;
    public int enemyPassedCount;
    public int enemyMax;
    public int friendPassedCount;
    public int friendMax;
    public float supportRateTime;
    [SerializeField] Transform[] respawnPointsForSupport;
    int supportPosRandomizer;
    [SerializeField] float minSupportTime;
    [SerializeField] float maxSupportTime;
    [SerializeField] float supportCoolDown;
    [SerializeField] float supportCoolDownx2;
    [SerializeField] Image potionImage;
    [SerializeField] Image x2Attack;
    int chooseRandomSupport;
    [SerializeField] TextMeshProUGUI enemyPassedText;
    [SerializeField] TextMeshProUGUI friendPassedText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI WinGameText;
    [SerializeField] string OurCityHasFallen;
    [SerializeField] string WeWonTheWar;
    [SerializeField] string enemyKilled;
    [SerializeField] string friendlyKilled;
                     string SupportInTheWay;
    [SerializeField] GameObject[] support;
    [SerializeField] TextMeshProUGUI SupportInTheWay_Text;
    public int enemyKilledCount;
    public int friendlyKilledCount;
    [SerializeField] TextMeshProUGUI enemyKilled_Text;
    [SerializeField] TextMeshProUGUI friendlyKilled_Text;

    [SerializeField] GameObject barrier1;
    [SerializeField] GameObject barrier2;
    AudioSource source;
    [SerializeField] AudioClip hButtonSound;
    public bool gameOver;
    bool enemycanWin;
    bool canCallForSupport;
    Player player;
    public  bool x2attackPower;
    


    private void Awake()
    {
      
    }


    void Start()
    {
       
   



        isPaused = false;
        source = GetComponent<AudioSource>();
        canCallForSupport = true;
        SupportInTheWay = " City Suppport in the Way";
        SupportInTheWay_Text.text = "";
        supportRateTime = Random.Range(minSupportTime, maxSupportTime);
        player = GameObject.Find("Player").GetComponent<Player>();
        enemycanWin = true;
        OurCityHasFallen = "Our City Has Fallen";
        WeWonTheWar = "We Won The War";
        friendPassedText.text = " Friends Passed Enemy Line " + friendPassedCount + (" from / " + friendMax);
        enemyPassedText.text = " Enemy Passed Our Line " + enemyPassedCount + (" from / " + enemyMax);
        gameOverText.enabled = false;
        WinGameText.enabled = false;
        enemyKilled = " Enemy Killed ";
        friendlyKilled = " Friendly Killed ";
        enemyKilled_Text.text = enemyKilled +enemyKilledCount ;
        friendlyKilled_Text.text = friendlyKilled + friendlyKilledCount;
    }

    
    void Update()


    {
      

        GameOver();
        WinGame();
        ExitGame();
        CountKills();
        SupportInTheWay_Potion();
        SupportInTheWay_x2attack();
        PauseGame();
    }
    public void CountFriend()
    {
        friendPassedCount += 1;
        friendPassedText.text = " Friends Passed Enemy Line " + friendPassedCount +( " from / "+friendMax );
    }
    public void CountEnemy()
    {
        enemyPassedCount += 1;
        enemyPassedText.text = " Enemy Passed Our Line " + enemyPassedCount + (" from / " + enemyMax);
    }

    public void WinGame()
    {
        if (friendPassedCount == friendMax)
        {
            enemycanWin = false;
            WinGameText.enabled = true;
            WinGameText.text = WeWonTheWar;
            player.GetComponent<Player>().enabled = false;
            player.GetComponentInChildren<Player_Raw_System>().enabled = false;
            enemyKilled_Text.enabled = true;
            friendlyKilled_Text.enabled = true;
            StartNextLevel();
            GameObject.Find("Player").GetComponent<Collider2D>().enabled = false;

        }
    }
    public void GameOver()
    {
        if(enemyPassedCount==enemyMax&&enemycanWin )
        {
            gameOverText.enabled = true;
            gameOverText.text = OurCityHasFallen;
            player.GetComponent<Player>().enabled = false;
            player.GetComponentInChildren<Player_Raw_System>().enabled = false;
            Destroy(barrier1);
            Destroy(barrier2);
            gameOver = true;
            player.GetComponent<Collider2D>().enabled = false;
            enemyKilled_Text.enabled = true;
            friendlyKilled_Text.enabled = true;
            StartNextLevel();


        }
        if(player.died!=false&&enemycanWin)
        {
            gameOver = true;
            gameOverText.enabled = true;
            gameOverText.text = OurCityHasFallen;
            player.GetComponent<Player>().enabled = false;
            player.GetComponentInChildren<Player_Raw_System>().enabled = false;
            Destroy(barrier1);
            Destroy(barrier2);
            friendlyKilled_Text.enabled = true;
            enemyKilled_Text.enabled = true;
            StartNextLevel();
        }
    }
    public void ExitGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
         
        }
      
    }

    public void StartNextLevel()
    {
        StartCoroutine(DelayLoadLevel(SceneManager.GetActiveScene().buildIndex - 1));

    }


    IEnumerator DelayLoadLevel(int levelIndex)
    {
      
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(levelIndex);
    }
    void CountKills ()
    {
        enemyKilled_Text.text = enemyKilled + enemyKilledCount;
        friendlyKilled_Text.text = friendlyKilled + friendlyKilledCount;
    }


    void SupportInTheWay_Potion()
    {
     if(Input.GetKeyDown(KeyCode.H) && canCallForSupport&&potionImage.fillAmount==1)
        {
            source.PlayOneShot(hButtonSound, 3);
            Debug.Log(" SUPPORT IS WORKING ");
            chooseRandomSupport = Random.Range(0, support.Length);
            supportPosRandomizer = Random.Range(0, respawnPointsForSupport.Length);
            supportRateTime = Random.Range(minSupportTime, maxSupportTime);
            SupportInTheWay_Text.text = SupportInTheWay;
            canCallForSupport = false;
            potionImage.fillAmount = 0;
            StartCoroutine("SupportHandler");
            StartCoroutine("ReactiveCallForSupport");
        }
        
     
     if(potionImage.fillAmount<1)
        {
            potionImage.fillAmount += supportCoolDown*Time.deltaTime;
        }
        
    }
    void SupportInTheWay_x2attack()
    {
       
        if (Input.GetKeyDown(KeyCode.J) && canCallForSupport && x2Attack.fillAmount == 1)
        {
            
            source.PlayOneShot(hButtonSound, 3);
            SupportInTheWay_Text.text = " Friendly attack x 2 ";
            canCallForSupport = false;
            x2Attack.fillAmount = 0;
            x2attackPower = true;
          
           
            StartCoroutine("SupportHandlerx2");
        
        }


        if (x2Attack.fillAmount < 1)
        {
            x2Attack.fillAmount += supportCoolDownx2* Time.deltaTime;
        }

    }

    IEnumerator SupportHandler()
    {
        yield return new WaitForSeconds(supportRateTime);
        SupportInTheWay_Text.text = "";
        Instantiate(support[chooseRandomSupport], respawnPointsForSupport[supportPosRandomizer].transform.position,Quaternion.identity);
    }
    IEnumerator ReactiveCallForSupport()
    {
        yield return new WaitForSeconds(2f);
        canCallForSupport = true;
        
    }
    IEnumerator SupportHandlerx2()
    {
        yield return new WaitForSeconds(10f);
        canCallForSupport = true;
        x2attackPower = false;



    }
    void PauseGame()
    {
        if(isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else if(isPaused==false)
        {
            Time.timeScale = 1;
            isPaused = false;
        }

       if(Input.GetKeyDown(KeyCode.Backspace))
        {
            isPaused =! isPaused;
        }
    }
   
}
