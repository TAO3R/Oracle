using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log(gameObject.name + "is avtivated");
        Debug.Log("GameObject enabled by: " + new System.Diagnostics.StackTrace());
    }
}
