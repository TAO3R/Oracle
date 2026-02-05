using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    [TextArea(1, 3)]
    private string[] sentences;

    [SerializeField]
    private int[] order;

    [SerializeField]
    private int index = 0;

    // Dialog Windows of two characters
    [SerializeField]
    private GameObject charaZero, charaOne;

    [SerializeField] LevelManager levelManagerScript;
    
    private void OnEnable()
    {
        // Grab references to two dialog windows
        charaZero = transform.GetChild(0).gameObject;
        charaOne = transform.GetChild(1).gameObject;

        // Hide both dialog windows
        charaZero.SetActive(false);
        charaOne.SetActive(false);

        // Show the dialog window that contains the first sentence
        NextSentence(index++);
    }

    private void NextSentence(int _index)
    {
        if (_index == (sentences.Length - 1))
        {
            if (name == "2nd Act")
            {
                StartCoroutine(levelManagerScript.EnableFieldExitArrow());
            }
            else if (name == "3rd Act")
            {
                levelManagerScript.CloseLevel();
            }
        }
        
        if (_index == sentences.Length && name == "1st Act")
        {
            charaOne.SetActive(false);
            charaZero.transform.GetChild(0).GetComponent<TextMeshPro>().text = "我先找找网在哪里";
        }
        
        if (_index >= sentences.Length) { return; }
        
        if (_index % order.Length == 0)
        {
            // Change the text
            SetText(_index);

            // Disable the dialog box that is not responsible for the first sentence
            if (order[_index % order.Length] == 0)
            {   // Disable charaOne
                charaOne.SetActive(false);
            }
            else
            {
                charaZero.SetActive(false);
            }
        }
        else
        { SetText(_index); }
        
    }

    private void SetText(int _index)
    {
        if (order[_index % order.Length] == 0)
        {
            if (!charaZero.activeSelf) { charaZero.SetActive(true); }
            charaZero.transform.GetChild(0).GetComponent<TextMeshPro>().text = sentences[_index % order.Length];
        }
        else
        {
            if (!charaOne.activeSelf) { charaOne.SetActive(true); }
            charaOne.transform.GetChild(0).GetComponent<TextMeshPro>().text = sentences[_index % order.Length];
        }
    }

    private void OnMouseDown()
    {
        NextSentence(index++);
    }
}
