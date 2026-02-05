using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropCharacter : MonoBehaviour, IDropHandler
{
    [SerializeField] private LevelManager levelManagerScript;

    private void OnEnable()
    {
        
    }

    public void OnDrop(PointerEventData _eventData)
    {
        Debug.Log(_eventData.pointerDrag.name + "OnDrop to" + name);
        StartLevel(_eventData);
        
        // throw new System.NotImplementedException();
    }

    private void StartLevel(PointerEventData _eventData)
    {
        if (name == "Wang Trigger" && _eventData.pointerDrag.name == "Wang")
        {
            Debug.Log("Start level Wang!");

            levelManagerScript.StartLevelWang();
            transform.GetChild(0).GetComponent<Image>().enabled = false;
        }

        else if (name == "Barricade" && _eventData.pointerDrag.name == "Huo")
        {
            Debug.Log("Burning the barricade!");

            StartCoroutine(levelManagerScript.BurnBarricade());
            transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
    }
}
