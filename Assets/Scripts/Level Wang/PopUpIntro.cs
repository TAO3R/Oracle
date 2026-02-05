using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpIntro : MonoBehaviour
{
    private void OnDisable()
    {
        // Reset sprite and timescale
        GetComponent<SpriteRenderer>().sprite = null;
        Time.timeScale = 1;
    }
}
