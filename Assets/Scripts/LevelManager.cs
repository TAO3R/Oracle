using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Highlight highlightScript;
    [SerializeField] private Transform brush;
    [SerializeField] private GameObject[] characters;

    [SerializeField] private GameObject[] levelMuGOs, levelHuoGOs;
    [SerializeField] private GameObject levelWang, levelWangTrigger, thirdActDialog;

    [SerializeField] private GameObject barricade, forestExitArrow;

    [SerializeField] private Collections collectionsScript;

    [SerializeField] private AudioSource mouseClick;

    [SerializeField] private GameObject wangGridExitArrow, fieldExitArrow, spawnExitArrow;

    [SerializeField] private Crossfade crossfadeScript;

    // Start is called before the first frame update
    void Awake()
    {
        highlightScript.target = brush;

        foreach (GameObject i in characters)
        {
            i.SetActive(false);
        }

        levelWang.SetActive(false);
        forestExitArrow.SetActive(false);

        // For wang grid exit arrow
        SpriteRenderer wangGridExitArrowRd = wangGridExitArrow.GetComponent<SpriteRenderer>();
        // Set the arrow to transparent and disable its collider
        wangGridExitArrowRd.color = new Color(wangGridExitArrowRd.color.r, wangGridExitArrowRd.color.g, wangGridExitArrowRd.color.b, 0);
        wangGridExitArrow.GetComponent<BoxCollider2D>().enabled = false;

        // For field grid exit arrow
        SpriteRenderer fieldExitArrowRd = fieldExitArrow.GetComponent<SpriteRenderer>();
        fieldExitArrowRd.color = new Color(fieldExitArrowRd.color.r, fieldExitArrowRd.color.g, fieldExitArrowRd.color.b, 0);    // Transparent
        fieldExitArrow.GetComponent<BoxCollider2D>().enabled = false;

        // For spawn grid exit arrow
        SpriteRenderer spawnExitArrowRd = spawnExitArrow.GetComponent<SpriteRenderer>();
        spawnExitArrowRd.color = new Color(spawnExitArrowRd.color.r, spawnExitArrowRd.color.g, spawnExitArrowRd.color.b, 0);    // Transparent
        spawnExitArrow.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Start()
    {
        thirdActDialog.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseClick.Play();
        }
    }

    #region Mu

    public void GetMu()
    {
        characters[0].SetActive(true);
    }

    private void IntroduceMu()
    {
        collectionsScript.OpenDetail();
        collectionsScript.SetIntroSprite(0);
    }

    public void CloseMuLevel()
    {
        foreach (GameObject i in levelMuGOs)
        {
            i.SetActive(false);
        }

        IntroduceMu();
    }

    public void CloseMuIntro()
    {
        collectionsScript.CloseDetail();

        GetMu();
    }

    #endregion

    #region Wang

    public void GetWang()
    {
        characters[1].SetActive(true);
    }

    public void IntroduceWang()
    {
        collectionsScript.OpenDetail();
        collectionsScript.SetIntroSprite(1);
    }

    public void CloseWangLevel()
    {
        levelWang.SetActive(false);
        levelWangTrigger.SetActive(false);
        // IntroduceWang();
    }

    public void CloseWangIntro()
    {
        collectionsScript.CloseDetail();

        GetWang();
    }

    public void StartLevelWang()
    {
        levelWang.SetActive(true);
    }

    public IEnumerator EnableWangGridExitArrow()
    {
        float timeElapsed = 0f;
        SpriteRenderer wangGridExitArrowRd = wangGridExitArrow.GetComponent<SpriteRenderer>();

        while (timeElapsed <= 1f)
        {
            float currentAlpha = Mathf.Lerp(0, 1, timeElapsed / 1f);
            wangGridExitArrowRd.color = new Color(wangGridExitArrowRd.color.r, wangGridExitArrowRd.color.g, wangGridExitArrowRd.color.b, currentAlpha);
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        wangGridExitArrowRd.color = new Color(wangGridExitArrowRd.color.r, wangGridExitArrowRd.color.g, wangGridExitArrowRd.color.b, 1);
        wangGridExitArrow.GetComponent<BoxCollider2D>().enabled = true;
    }

    #endregion

    #region Huo

    public void GetHuo()
    {
        characters[2].SetActive(true);
    }

    private void IntroduceHuo()
    {
        collectionsScript.OpenDetail();
        collectionsScript.SetIntroSprite(2);
    }

    public void CloseHuoLevel()
    {
        foreach (GameObject i in levelHuoGOs)
        {
            i.SetActive(false);
        }

        IntroduceHuo();
    }

    public void CloseHuoIntro()
    {
        collectionsScript.CloseDetail();

        GetHuo();
    }

    public IEnumerator BurnBarricade()
    {
        Image barricadeImage = barricade.GetComponent<Image>();
        float timeElapsed = 0f, oldAlpha = barricadeImage.color.a;

        float currentAlpha = oldAlpha;

        while (timeElapsed <= 1f)   // Duration of this process is set to be 1s
        {
            currentAlpha = Mathf.Lerp(oldAlpha, 0, timeElapsed / 1f);
            barricadeImage.color = new Color(barricadeImage.color.r, barricadeImage.color.g, barricadeImage.color.b, currentAlpha);
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        barricadeImage.color = new Color(barricadeImage.color.r, barricadeImage.color.g, barricadeImage.color.b, 0);

        barricade.SetActive(false);

        StartCoroutine(ShowForestExit());
    }

    private IEnumerator ShowForestExit()
    {
        forestExitArrow.SetActive(true);
        forestExitArrow.GetComponent<BoxCollider2D>().enabled = false;

        SpriteRenderer arrowRd = forestExitArrow.GetComponent<SpriteRenderer>();

        float timeElapsed = 0f, targetAlpha = arrowRd.color.a;

        arrowRd.color = new Color(arrowRd.color.r, arrowRd.color.g, arrowRd.color.b, 0);

        while (timeElapsed <= 1f)    // The duration of this process is defaulted to finish within 1s
        {
            arrowRd.color = new Color(arrowRd.color.r, arrowRd.color.g, arrowRd.color.b, Mathf.Lerp(0, targetAlpha, timeElapsed / 1f));

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        forestExitArrow.GetComponent<BoxCollider2D>().enabled = true;
    }

    #endregion

    public IEnumerator EnableFieldExitArrow()
    {
        float timeElapsed = 0f;
        SpriteRenderer fieldExitArrowRd = fieldExitArrow.GetComponent<SpriteRenderer>();

        while (timeElapsed <= 1f)
        {
            float currentAlpha = Mathf.Lerp(0, 1, timeElapsed / 1f);
            fieldExitArrowRd.color = new Color(fieldExitArrowRd.color.r, fieldExitArrowRd.color.g, fieldExitArrowRd.color.b, currentAlpha);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fieldExitArrowRd.color = new Color(fieldExitArrowRd.color.r, fieldExitArrowRd.color.g, fieldExitArrowRd.color.b, 1);
        fieldExitArrow.GetComponent<BoxCollider2D>().enabled = true;
    }

    public IEnumerator EnableSpawnExitArrow()
    {
        float timeElapsed = 0f;
        SpriteRenderer spawnExitArrowRd = spawnExitArrow.GetComponent<SpriteRenderer>();

        while (timeElapsed <= 1f)
        {
            float currentAlpha = Mathf.Lerp(0, 1, timeElapsed / 1f);
            spawnExitArrowRd.color = new Color(spawnExitArrowRd.color.r, spawnExitArrowRd.color.g, spawnExitArrowRd.color.b, currentAlpha);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        spawnExitArrowRd.color = new Color(spawnExitArrowRd.color.r, spawnExitArrowRd.color.g, spawnExitArrowRd.color.b, 1);
        spawnExitArrow.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void CloseLevel()
    {
        crossfadeScript.fadeOut();
    }

    // Following are used by buttons

    public void coroutineUnfocus()
    {
        StartCoroutine(highlightScript.Unfocus(highlightScript.fadeDuration, highlightScript.focusDuration));
    }

} // End of class
