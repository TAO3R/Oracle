using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossButton : MonoBehaviour
{
    public GameObject[] levelGameObjects;

    public Color hoverColor, pressColor;

    private Color defaultColor;
    private SpriteRenderer crossRd;
    [SerializeField] private bool mouseOnButton, isPressed;

    [SerializeField] private FireLevelManager fireLevelScript;

    // Start is called before the first frame update
    void Start()
    {
        crossRd = GetComponent<SpriteRenderer>();
        defaultColor = crossRd.color;
        mouseOnButton = false;
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseLevel()
    {
        Debug.Log("Closing Level!");

        foreach(GameObject i in levelGameObjects)
        {
            Debug.Log("Disactivating " + i.name);
            i.SetActive(false);
        }
        
        if (transform.parent.name == "Level ¡°Mu¡±")
        {
            Debug.Log("Closing level Mu!");
            levelGameObjects[0].SetActive(true);
            levelGameObjects[0].GetComponent<SpriteRenderer>().enabled = true;
            levelGameObjects[0].GetComponent<CapsuleCollider2D>().enabled = true;
        }
        else if (transform.parent.name == "Level ¡°Huo¡±")
        {
            Debug.Log("Closing level Huo!");
            GetComponentInParent<PolygonCollider2D>().enabled = false;
            levelGameObjects[0].SetActive(true);
            levelGameObjects[0].GetComponent<SpriteRenderer>().enabled = true;
            levelGameObjects[0].GetComponent<BoxCollider2D>().enabled = true;
            fireLevelScript.phaseChanged = false;
        }

        gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        // Change to a lighter color
        crossRd.color = hoverColor;
        mouseOnButton = true;
    }

    private void OnMouseExit()
    {
        // Change to the default color
        if (!isPressed)
        { crossRd.color = defaultColor; }
        
        mouseOnButton = false;
    }

    private void OnMouseDown()
    {
        // Change to a darker color
        crossRd.color = pressColor;
        isPressed = true;
    }

    private void OnMouseUp()
    {
        if (mouseOnButton)
        { CloseLevel(); }
        else
        {
            mouseOnButton = false;
            isPressed = false;
            crossRd.color = defaultColor;
        }
    }
}
