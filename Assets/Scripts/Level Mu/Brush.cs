using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    // The first bool is to determine whether the brush is being dragged;
    // The second bool is to determine whether the brush has enterded the collider of the shell
    [SerializeField] private bool isDragged, hasCollided;

    [SerializeField] private Shell shellScript;

    [SerializeField] private Vector3 brushStartPos;      // Brush position when first collided with the shell while brushing

    private Vector3 brushDefaultPos;    // (0, 0, 233)

    [SerializeField] private float distanceTravelled;

    private Vector3 lastFramePos;

    // Below are fields that are related to brush moving by itself as a hint
    [SerializeField] private Transform initPos, endPos;
    private Vector2 dir, negtiveDir;
    [SerializeField] private float speed, restTime;
    [SerializeField] private float timeRested;
    [SerializeField] private bool isgivingHint;
    [SerializeField] private bool isResting, isMoving, movingForward;
    
    // Start is called before the first frame update
    void Start()
    {
        // Brush moving & wiping related
        isDragged = false;
        hasCollided = false;

        brushDefaultPos = new Vector3(0, 0, 233);
        brushStartPos = brushDefaultPos;

        distanceTravelled = 0f;

        // Brush giving-hint related
        transform.position = initPos.position;
        dir = (endPos.position - initPos.position).normalized;
        negtiveDir = (initPos.position - endPos.position).normalized;
        timeRested = 0f;
        isgivingHint = true;
        isResting = true; isMoving = false; movingForward = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isgivingHint)
        {
            if (isResting)
            {
                timeRested += Time.deltaTime;

                if (timeRested >= restTime)
                {
                    isResting = false;
                    isMoving = true;
                    timeRested = 0f;
                }
            }
            else if (isMoving)
            {
                if (movingForward)
                {
                    transform.Translate(dir * speed * Time.deltaTime, Space.World);
                    
                    // Debug.Log("My y: " + transform.position.y + " terminal's y: " + endPos.position.y);
                    
                    if (transform.position.y < endPos.position.y)
                    {
                        movingForward = false;
                    }
                }
                else
                {
                    transform.Translate(negtiveDir * speed * Time.deltaTime, Space.World);

                    if (transform.position.y > initPos.position.y)
                    {
                        movingForward = true;
                        isResting = true;
                        isMoving = false;
                    }
                }
            }
        }
        
        if (isDragged)  // Sets brush position on screen
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Constraints
            if (mousePos.x < -5) { mousePos.x = -5; }
            if (mousePos.x > 5) { mousePos.x = 5; }
            if (mousePos.y < -3) { mousePos.y = -3; }
            if (mousePos.y > 3) { mousePos.y = 3; }

            // Update brush position
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);

            // Update distance travelled
            if (withinShell() && hasCollided)
            {
                distanceTravelled += Vector3.Distance(lastFramePos, transform.position);
                lastFramePos = transform.position;
            }
            else
            {
                distanceTravelled = 0;
                hasCollided = false;
            }
        }

        // Check for a successful "Wipe"
        if (distanceTravelled > 2 && Vector3.Distance(brushStartPos, transform.position) > 2)
        {
            Debug.Log("A successful wipe!");
            Brushing();
            distanceTravelled = 0f;
            brushStartPos = transform.position;
        }
    }

    public bool WhetherIsDragged()
    {
        return this.isDragged;
    } // Getter of the field isDragged

    public void SetHasCollided(bool _hascollided)
    {
        if (!this.hasCollided)  // First time entering the shell or being dragged while resting on the shell
        {
            brushStartPos = transform.position;

            lastFramePos = transform.position;
        }
        this.hasCollided = _hascollided;
    } // Setter of the field hasCollided

    private void OnMouseDown()
    {
        // Stop giving hint
        if (isgivingHint) { isgivingHint = false; }

        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        isDragged = true;
    }

    private void OnMouseUp()
    {
        isDragged = false;
        hasCollided = false;
        brushStartPos = brushDefaultPos;
        distanceTravelled = 0f;
    }

    private bool withinShell()  // Returns whether the brush is within the shell
    {
        return (transform.position.x > -1.25 && transform.position.x < 1.35 && transform.position.y > -2.15 && transform.position.y < 2.25);
    }

    private void Brushing()
    {
        shellScript.Brushed();
    }
}
