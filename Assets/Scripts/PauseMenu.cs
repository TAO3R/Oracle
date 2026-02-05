using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject restartButton, quitButton;

    private void Awake()
    {
        restartButton.SetActive(false);
        quitButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if (restartButton.activeSelf)
        {   // unpause
            restartButton.SetActive(false);
            quitButton.SetActive(false);

            Time.timeScale = 1;
        }
        else
        {   // pause
            restartButton.SetActive(true);
            quitButton.SetActive(true);

            Time.timeScale = 0;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
