using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetLeevlArrow : MonoBehaviour
{
    public bool clockwise;
    [SerializeField] private float speed;

    [SerializeField] private float rotateAngle;

    // Start is called before the first frame update
    void Start()
    {
        clockwise = true;
        speed = 75f;
        rotateAngle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (clockwise)
        {
            rotateAngle -= speed * Time.deltaTime;
        }
        else
        {
            rotateAngle += speed * Time.deltaTime;
        }
        transform.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
    }
}
