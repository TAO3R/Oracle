using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringBar : MonoBehaviour
{
    public int scoringBarIndex;

    // Start is called before the first frame update
    void Start()
    {
        scoringBarIndex = 0;
        ActivateScoringBar(scoringBarIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        scoringBarIndex = 0;
        ActivateScoringBar(scoringBarIndex);
    }

    public void ActivateScoringBar(int _index)
    {
        if (_index >= transform.childCount) { return; }
        
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == _index)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
