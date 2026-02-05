using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTransit : MonoBehaviour
{
    public Wood woodScript;
    public Stick stickScript;

    [SerializeField] private bool phaseChanged;
    [SerializeField] private GameObject phaseOne, phaseTwo;

    // Start is called before the first frame update
    void OnEnable()
    {
        phaseChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (phaseChanged) { return; }
        
        if (woodScript.WhetherPlacedCorrectly() && stickScript.WhetherPlacedCorrectly())
        {
            ChanginePhase();
        }
    }

    private void ChanginePhase()
    {
        phaseOne.SetActive(false);
        phaseTwo.SetActive(true);
        phaseChanged = true;
    }
}
