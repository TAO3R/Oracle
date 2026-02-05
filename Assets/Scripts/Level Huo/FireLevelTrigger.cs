using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLevelTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] levelGameObjects;

    private void Awake()
    {
        foreach (GameObject i in levelGameObjects)
        {
            i.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on fire level trigger!");
        ActivateLevel();
    }

    private void ActivateLevel()
    {
        Debug.Log("Fire level activated!");

        foreach (GameObject i in levelGameObjects)
        {
            i.SetActive(true);
        }

        // gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
