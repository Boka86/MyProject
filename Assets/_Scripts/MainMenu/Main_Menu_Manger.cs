using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Manger : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator fadeTran;
    [SerializeField] float waitTime;
    private void Start()
    {
        MouseCur();
    }
    void Update()
    {
        
    }


    public void StartNextLevel()
    {
        StartCoroutine(DelayLoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
    }



   public void ExitGame()
    {
        
        
            Application.Quit();
        
    }

    IEnumerator DelayLoadLevel(int levelIndex)
    {
        fadeTran.SetTrigger("Fade");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(levelIndex);
    }

    void MouseCur()
    {
        
        
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        
    }
}
