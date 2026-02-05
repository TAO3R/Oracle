using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private GameObject[] levelGameObjects;
    [SerializeField] private Highlight highlightScript;

    private void Awake()
    {
        foreach(GameObject i in levelGameObjects)
        {
            i.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        ActivateLevel();
    }

    private void ActivateLevel()
    {
        foreach (GameObject i in levelGameObjects)
        {
            i.SetActive(true);
        }

        // gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
