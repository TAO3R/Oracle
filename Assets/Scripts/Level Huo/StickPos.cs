using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickPos : MonoBehaviour
{
    [SerializeField] Stick stickScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stick")
        {
            stickScript.inCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stick")
        {
            stickScript.inCollision = false;
        }
    }
}
