using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public int dustRemaining;

    [SerializeField]    // Time for the dust to turn transparent
    private float fadeDuration = 0.5f;

    [SerializeField]
    private bool canBeBrushed;

    [SerializeField] private LevelManager levelManagerScript;

    void Start()
    {
        dustRemaining = 3;
        canBeBrushed = true;   
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Brush")
        {
            Brush brushScript = collision.GetComponent<Brush>();
            if (brushScript.WhetherIsDragged()) // Brush is being dragged
            {
                brushScript.SetHasCollided(true);
            }
        }
    }

    public void Brushed()
    {
        if (dustRemaining < 0 || !canBeBrushed) { return; }

        canBeBrushed = false;
        StartCoroutine(Wiped(fadeDuration, --dustRemaining));
    }

    private IEnumerator Wiped(float _duration, int _index)
    {
        float timeElapsed = 0f;

        SpriteRenderer dustRd = transform.GetChild(_index).gameObject.GetComponent<SpriteRenderer>();

        while (timeElapsed < _duration)
        {
            float currentOpacity = Mathf.Lerp(1f, 0f, timeElapsed / _duration);
            dustRd.color = new Color(dustRd.color.r, dustRd.color.g, dustRd.color.b, currentOpacity);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        dustRd.color = new Color(dustRd.color.r, dustRd.color.g, dustRd.color.b, 0f);
        canBeBrushed = true;

        if (dustRemaining == 0)
        {
            levelManagerScript.CloseMuLevel();
        }
    }

} // End of class
