using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Intro2 : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float delayBetweenLines = 3f;
    public float fadeDuration = 1f;

    [SerializeField] private static float[] waitTime = { 3f, 3f, 5f};     // Used to customize the dealy time between each two different lines

    private void Start()
    {
        textComponent.alpha = 0;
        StartCoroutine(FadeInText());
    }

    private IEnumerator FadeInText()
    {
        string[] lines = textComponent.text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            // Reset the text to only this line.
            textComponent.text = line;

            // Lerp the alpha of the line from 0 to 1
            float counter = 0;
            while (counter < fadeDuration)
            {
                counter += Time.deltaTime;
                float alpha = Mathf.Lerp(0, 1, counter / fadeDuration);

                // Change the alpha of the text
                textComponent.alpha = alpha;

                yield return null;
            }

            yield return new WaitForSeconds(waitTime[i]);

            counter = 0;
            while (counter < fadeDuration)
            {
                counter += Time.deltaTime;
                float alpha = Mathf.Lerp(1, 0, counter / fadeDuration);

                textComponent.alpha = alpha;

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }

        // Switch to the next scene here.
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        Debug.Log("Loading next scene...");
        SceneManager.LoadScene("Level");
    }
}
