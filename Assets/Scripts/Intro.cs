using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Intro : MonoBehaviour
{
    public TextMeshProUGUI[] textComponent;
    public float delayBetweenLines = 1f;
    public float fadeDuration = 1f;

    private void Start()
    {
        foreach(TextMeshProUGUI line in textComponent)
        { line.alpha = 0; }

        StartCoroutine(FadeInText());
    }

    private IEnumerator FadeInText()
    {
        foreach (TextMeshProUGUI line in textComponent)
        {
            // Lerp the alpha of the line from 0 to 1
            float counter = 0;
            while (counter < fadeDuration)
            {
                counter += Time.deltaTime;
                float alpha = Mathf.Lerp(0, 1, counter / fadeDuration);

                // Change the alpha of the text
                //textComponent.alpha = alpha;
                line.alpha = alpha;

                yield return null;
            }

            yield return new WaitForSeconds(delayBetweenLines);
        }

        // Switch to the next scene here.
        Debug.Log("Loading next scene");
    }
}
