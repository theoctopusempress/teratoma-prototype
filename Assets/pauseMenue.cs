using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pauseMenue : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool GameIsTabPaused = false;
    public GameObject pauseMenuUI;
    public GameObject tabMenuUI;
    public GameObject masterController;
    void Start()
    {
       // gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
            tabMenuUI.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameIsTabPaused)
            {
                tabMenuUI.SetActive(false);
                Time.timeScale = 1f;
                GameIsTabPaused = false;
            } else
            {
                tabMenuUI.SetActive(true);
                Time.timeScale = 0f;
                GameIsTabPaused = true;
            }
            pauseMenuUI.SetActive(false);
        }
    }
    public void Resume() 
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadLastSave()
    {
        var saver = masterController.GetComponent<GameManager>();
        saver.LoadState();
    }
    public void quickSave()
    {
        var saving = masterController.GetComponent<GameManager>();
        saving.SaveState();
    }
        

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
