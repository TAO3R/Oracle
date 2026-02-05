using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    private Vector3 mousePos, offset;     // To store the offset between mouse position & object position
    [SerializeReference] private bool isPressed;
    [SerializeReference] private Vector3 defaultPos;

    public bool inCollision;      // To examine whether inside the dotted line

    // The following are used to examine whether the stick is placed at the correct position
    [SerializeField] private bool placedCorrectly;
    [SerializeField] private Transform targetPos;
    [SerializeField] private float radius = 0.25f;

    void OnEnable()
    {
        placedCorrectly = false;
        transform.localPosition = defaultPos;

        inCollision = false;
    }

    private void OnDisable()
    {
        placedCorrectly = false;
    }
    void Update()
    {
        if (isPressed && !placedCorrectly)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;
            transform.position = mousePos + offset;

            // Constraints set by the background
            if (transform.localPosition.x < -5) { transform.localPosition = new Vector3(-5, transform.localPosition.y, transform.localPosition.z); }
            if (transform.localPosition.x > 5) { transform.localPosition = new Vector3(5, transform.localPosition.y, transform.localPosition.z); }
            if (transform.localPosition.y < -2.2) { transform.localPosition = new Vector3(transform.localPosition.x, -2.2f, transform.localPosition.z); }
            if (transform.localPosition.y > 2.3) { transform.localPosition = new Vector3(transform.localPosition.x, 2.3f, transform.localPosition.z); }
        }
    }

    private void OnMouseDown()
    {
        if (!placedCorrectly)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;
            offset = transform.position - mousePos;
        }

        isPressed = true;
    }

    /*private void OnMouseDrag()
    {
        if (!placedCorrectly)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;

            Debug.Log("mouse position: " + mousePos + ", offset:" + offset);
            transform.position = mousePos + offset;
            Debug.Log("tranform.position: " + transform.position);
        }
    }*/

    private void OnMouseUp()
    {
        // Vector3.Distance(targetPos.position, transform.position) < radius
        if (inCollision)
        {
            placedCorrectly = true;
            transform.position = targetPos.position;
        }

        isPressed = false;
    }

    public bool WhetherPlacedCorrectly()
    {
        return this.placedCorrectly;
    }
}
