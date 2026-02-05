using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outro : MonoBehaviour
{
    private float timeElapsed;
    private RectTransform outroRectTrans;

    [SerializeField] private float speed = 10f;

    [SerializeField] private Animator crossfadeAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0f;
        outroRectTrans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= 3)
        {
            outroRectTrans.anchoredPosition = new Vector2(0f, outroRectTrans.anchoredPosition.y + speed * Time.deltaTime);

            if (outroRectTrans.anchoredPosition.y >= 700)
            {
                crossfadeAnim.SetTrigger("Start");
            }
        }
    }
}
