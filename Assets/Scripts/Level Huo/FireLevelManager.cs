using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLevelManager : MonoBehaviour
{
    [Header("Level Management")]
    [SerializeField] private GameObject[] fireLevelGameObjects;
    [SerializeField] private LevelManager levelManagerScript;

    [Header("Level Transit")]
    public Wood woodScript;
    public Stick stickScript;
    
    public bool phaseChanged;
    [SerializeField] private GameObject phaseOne, phaseTwo;

    [Header("Phase two")]
    [SerializeField] private Transform arrow;
    [SerializeField] private ScoringBar scoringBarScript;
    [SerializeField] private Flame flameScript;

    // Start is called before the first frame update
    void Start()
    {
        // Deavtivate the level
        DeavtivateLevel();
        GetComponent<PolygonCollider2D>().enabled = false;

        // Testing phase two
        // phaseTwo.SetActive(false);

        phaseChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (phaseChanged) { return; }

        if (woodScript.WhetherPlacedCorrectly() && stickScript.WhetherPlacedCorrectly())
        {
            ChangingPhase();
        }
    }

    private void DeavtivateLevel()
    {
        foreach (GameObject i in fireLevelGameObjects)
        {
            i.SetActive(false);
        }
    }

    private void ChangingPhase()
    {
        phaseOne.SetActive(false);
        phaseTwo.SetActive(true);
        phaseChanged = true;
        GetComponent<PolygonCollider2D>().enabled = true;
    }

    private void OnMouseDown()
    {
        if (!phaseChanged) { return; }

        if (ArrowHit())
        {
            flameScript.BurnHarder(++flameScript.flameLevelIndex);
            scoringBarScript.ActivateScoringBar(++scoringBarScript.scoringBarIndex);
        }
        else
        {
            Debug.Log("Missed!");
        }
    }

    private bool ArrowHit()
    {
        switch (scoringBarScript.scoringBarIndex)
        {
            case 0:
                if (arrow.localPosition.x > 0.75 && arrow.localPosition.x < 2.75)
                { return true; }
                break;
            case 1:
                if (arrow.localPosition.x > -2.5 && arrow.localPosition.x < -1)
                { return true; }
                break;
            case 2:
                if (arrow.localPosition.x > -0.9 && arrow.localPosition.x < -0.1)
                { return true; }
                break;
            default:
                break;
        }
        
        return false;
    }

    public void CompleteLevel()
    {
        Debug.Log("Fire level complete");
        GetComponent<PolygonCollider2D>().enabled = false;
        levelManagerScript.CloseHuoLevel();
    }
}
