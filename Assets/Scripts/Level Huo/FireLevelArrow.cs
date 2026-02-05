using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLevelArrow : MonoBehaviour
{
    [SerializeField] private Transform begin, end;
    [SerializeField] private float speed = 3f;
    [SerializeField] private bool moveForward;
    
    // Start is called before the first frame update
    void Start()
    {
        moveForward = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveForward)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        if (transform.position.x > end.position.x)
        {
            moveForward = false;
        }
        else if (transform.position.x < begin.position.x)
        {
            moveForward = true;
        }
    }

    private void OnEnable()
    {
        Debug.Log("Fire level arrow being enabled!");
        
        moveForward = false;
        transform.localPosition = new Vector3(begin.localPosition.x, -1.7f, 0f);
    }
}
