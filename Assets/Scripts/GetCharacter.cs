using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCharacter : MonoBehaviour
{
    [SerializeField] private float duration = 1f;

    private RectTransform rectTransform;

    [SerializeField] private GameObject anchors;
    [SerializeField] private GameObject target;

    public bool earned;

    [SerializeField] LevelManager levelManagerScript;

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        // Debug.Log("Current Position: " + rectTransform.anchoredPosition);
        earned = false;

        StartCoroutine(FlyToSkillBar(duration));
    }

    private IEnumerator FlyToSkillBar(float _duration)
    {
        Vector2 startPos = rectTransform.anchoredPosition;
        float timeElapsed = 0f;
        
        /*Transform target = null;

        for (int i = 1; i <= 3; i++)
        {
            if (anchors.transform.GetChild(i).childCount == 0)
            {
                target = anchors.transform.GetChild(i);
            }

            break;
        }*/

        Vector2 endPos = target.GetComponent<RectTransform>().anchoredPosition;

        while (timeElapsed <= _duration)
        {
            float currentX = Mathf.Lerp(startPos.x, endPos.x, timeElapsed / _duration);
            float currentY = Mathf.Lerp(startPos.y, endPos.y, timeElapsed / _duration);

            float currentScale = Mathf.Lerp(1f, 0.25f, timeElapsed / _duration);

            rectTransform.anchoredPosition = new Vector2(currentX, currentY);
            rectTransform.localScale = new Vector3(currentScale, currentScale, 1);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        rectTransform.anchoredPosition = endPos;
        rectTransform.localScale = new Vector3(0.25f, 0.25f, 1f);
        earned = true;

        if (name == "Wang")
        {
            StartCoroutine(levelManagerScript.EnableWangGridExitArrow());
        }

        if (name == "Mu")
        {
            StartCoroutine(levelManagerScript.EnableSpawnExitArrow());
        }
    }
} // End of class