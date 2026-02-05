using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WangObject : MonoBehaviour
{
    [SerializeField] private LevelManager levelManagerScript;
    
    private void OnMouseDown()
    {
        levelManagerScript.IntroduceWang();
    }
}
