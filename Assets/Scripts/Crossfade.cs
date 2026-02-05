using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crossfade : MonoBehaviour
{
    private Animator crossfadeAnim;

    private void Start()
    {
        crossfadeAnim = GetComponent<Animator>();
    }

    public void fadeOut()
    {
        crossfadeAnim.SetTrigger("Start");
    }

    public void NextScene()
    {
        Debug.Log("Closing level!");
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
