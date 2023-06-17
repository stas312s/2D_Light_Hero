using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

   [SerializeField] private GameObject winPanel;
   [SerializeField] private GameObject pausePanel;
   [SerializeField] private GameObject losePanel;
   private static UIManager instance;
   public static UIManager Instance
    {
        get { return instance; }
    }
   private void Awake()
   {
    if (instance == null)
        {
            instance = this;
        }
   }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) Pause();
    }
    
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void Pause()
    {
        if(Time.timeScale == 1)
        {
             pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else Resume();

       
    }
    public void Win()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }
    public void Lose()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }
}
