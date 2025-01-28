using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    
    public bool startreset = true;
    void Start()
    {
       startreset = true;
    }
    [SerializeField] public GameObject PauseMenuPanel;
    [SerializeField] public GameObject stopButton;
    [SerializeField] public GameObject BuyMoneyMenuPanel;
    [SerializeField] public GameObject GameOverPanel;
    


    public void Pause()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PauseForMoney()
    {
        BuyMoneyMenuPanel.SetActive(true);
        stopButton.SetActive(false);
    }
    
    public void CloseMoneyMenu()
    {
        BuyMoneyMenuPanel.SetActive(false);
        stopButton.SetActive(true);
    }
    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void AfterAd()
    {
        GameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}