using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] RectTransform highlightTrans;

    public float focusDuration, fadeDuration;
    [SerializeField] private float focusScale = 0.1f;

    public Transform target;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private bool isFocusing, changingFocus;

    // [SerializeField] private float progress;
    
    // Start is called before the first frame update
    void Start()
    {
        isFocusing = false;
        changingFocus = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Trigger for focusing
        if (target != null && !changingFocus)
        {
            if (!isFocusing)    // Focus on the target position 
            {
                // target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Focus(focusDuration);
                isFocusing = true;
            }
            else                // Lose focus
            {
                StartCoroutine(Unfocus(fadeDuration, focusDuration));
                isFocusing = false;
            }
        }
    }

    public void Focus(float _duration)
    {
        changingFocus = true;

        // float timeElapsed = 0f;
        // progress = timeElapsed;

        Vector2 screenSize = highlightTrans.GetComponentInParent<Canvas>().pixelRect.size;

        Vector2 canvasSize = new Vector2 (highlightTrans.GetComponentInParent<RectTransform>().sizeDelta.x, highlightTrans.GetComponentInParent<RectTransform>().sizeDelta.y);
        // Debug.Log("Canvas size: " + canvasSize);

        targetPos = target.position;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(targetPos);
        // Debug.Log("Screen point: " + screenPoint);
        // Debug.Log("World position: " + cam.ScreenToWorldPoint(Input.mousePosition));

        Vector2 newPos = new Vector2((screenPoint.x - screenSize.x / 2f) / screenSize.x * canvasSize.x, (screenPoint.y - screenSize.y / 2f) / screenSize.x * canvasSize.x);
        // Debug.Log("New position: " + newPos);

        Vector2 oldPos = highlightTrans.anchoredPosition;
        // Debug.Log("Old position: " + oldPos);
        
        // Cancelled on 6/28

        /*while (timeElapsed <= _duration)
        {
            float currentX = Mathf.Lerp(oldPos.x, newPos.x, timeElapsed / _duration);
            float currentY = Mathf.Lerp(oldPos.y, newPos.y, timeElapsed / _duration);

            float currentScale = Mathf.Lerp(2f, 0.1f, timeElapsed / _duration);

            highlightTrans.anchoredPosition = new Vector2(currentX, currentY);
            highlightTrans.localScale = new Vector3(currentScale, currentScale, 1);

            timeElapsed += Time.deltaTime;
            yield return null;
        }*/

        highlightTrans.anchoredPosition = new Vector2(newPos.x, newPos.y);
        highlightTrans.localScale = new Vector3(0.1f, 0.1f, 1);

        changingFocus = false;
        target = null;
        targetPos = cam.transform.position;
        // Debug.Log("New target position: " + target);
    }

    public IEnumerator Unfocus(float _fadeDuration, float _duration)
    {
        Debug.Log("Unfocusing!");

        changingFocus = true;
        float timeElapsed = 0f;
        // progress = timeElapsed;

        CutoutMaskUI shadowScript = transform.GetChild(0).GetComponent<CutoutMaskUI>();

        // Cancelled on 6/28

        /*float oldOpacity = shadowScript.color.a;

        while (timeElapsed <= _fadeDuration)
        {
            float currentAlpha = Mathf.Lerp(oldOpacity, 0f, timeElapsed / _fadeDuration);
            shadowScript.color = new Color(shadowScript.color.r, shadowScript.color.g, shadowScript.color.b, currentAlpha);

            timeElapsed += Time.deltaTime;
            // progress = timeElapsed;
            // Debug.Log(progress + "has spent fading, continue fading: " + (timeElapsed <= _fadeDuration));
            yield return null;
        }

        shadowScript.color = new Color(shadowScript.color.r, shadowScript.color.g, shadowScript.color.b, 0f);
        timeElapsed = 0;*/

        Vector2 canvasSize = highlightTrans.GetComponentInParent<Canvas>().pixelRect.size;
        // Debug.Log("Canvas size: " + canvasSize);

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(targetPos);
        // Debug.Log("Screen point: " + screenPoint);

        Vector2 newPos = new Vector2(screenPoint.x - canvasSize.x / 2f, screenPoint.y - canvasSize.y / 2f);
        // Debug.Log("New position: " + newPos);

        Vector2 oldPos = highlightTrans.anchoredPosition;
        // Debug.Log("Old position: " + oldPos);

        while (timeElapsed <= _duration)
        {
            float currentX = Mathf.Lerp(oldPos.x, newPos.x, timeElapsed / _duration);
            float currentY = Mathf.Lerp(oldPos.y, newPos.y, timeElapsed / _duration);

            float currentScale = Mathf.Lerp(0.1f, 2f, timeElapsed / _duration);

            highlightTrans.anchoredPosition = new Vector2(currentX, currentY);
            highlightTrans.localScale = new Vector3(currentScale, currentScale, 1);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        changingFocus = false;
        target = null;

        Debug.Log("Unfoucsing completed!");
    }
}
