using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private Transform nextGrid;

    [SerializeField]
    private Camera gameCam;

    private bool isClicked;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        gameCam.GetComponent<CamMove>().SetTargetGrid(nextGrid);

        // Cancelled on 6/26

        /*if (gameObject.tag == "EnterForestArrow")
        {
            gameCam.transform.GetChild(0).GetComponent<Audio>().EnterForest();
        }
        else if (gameObject.tag == "LeaveForestArrow")
        {
            gameCam.transform.GetChild(0).GetComponent<Audio>().LeaveForest();
        }*/
    }
}
