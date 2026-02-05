using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    private Vector3 mousePos, offset;     // To store the offset between mouse position & object position
    [SerializeReference] private bool isPressed;
    [SerializeReference] private Vector3 defaultPos;
    public bool inCollision;      // To examine whether inside the dotted line

    // The following are used to examine whether the stick is placed at the correct position
    [SerializeField] private bool placedCorrectly;
    [SerializeField] private Transform targetPos;
    [SerializeField] private float radius = 1f;

    // The following are used for rotating the wood to its correct orientation
    [SerializeField] private float speed;
    [SerializeField] private float rotateAngle;

    void OnEnable()
    {
        placedCorrectly = false;
        rotateAngle = 90;

        transform.localPosition = defaultPos;
        transform.localRotation = Quaternion.Euler(0f, 0f, 90f);

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
            if (transform.localPosition.x < -4.8) { transform.localPosition = new Vector3(-4.8f, transform.localPosition.y, transform.localPosition.z); }
            if (transform.localPosition.x > 4.8) { transform.localPosition = new Vector3(4.8f, transform.localPosition.y, transform.localPosition.z); }
            if (transform.localPosition.y < -2) { transform.localPosition = new Vector3(transform.localPosition.x, -2, transform.localPosition.z); }
            if (transform.localPosition.y > 2) { transform.localPosition = new Vector3(transform.localPosition.x, 2, transform.localPosition.z); }
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
            transform.position = mousePos + offset;
        }
    }*/

    private void OnMouseUp()
    {
        // Vector3.Distance(targetPos.position, transform.position) < radius
        if (inCollision)
        {
            placedCorrectly = true;
            transform.position = targetPos.position;
            
            // For rotation
            transform.rotation = targetPos.rotation;
            //StartCoroutine(Rotating());
        }

        isPressed = false;
    }

    private IEnumerator Rotating()
    {
        while (rotateAngle >= 0)
        {
            rotateAngle -= speed * Time.deltaTime;
            transform.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
            yield return null;
        }

        transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
    }

    public bool WhetherPlacedCorrectly()
    {
        return this.placedCorrectly;
    }
}
