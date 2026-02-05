using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    [SerializeField] private Sprite[] flameLevel;
    public int flameLevelIndex;

    private SpriteRenderer flameRd;

    [SerializeField] private FireLevelManager levelManagerScirpt;
    
    // Start is called before the first frame update
    void Start()
    {
        flameRd = GetComponent<SpriteRenderer>();

        flameLevelIndex = -1;       // No flame at the start
        flameRd.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        flameRd = GetComponent<SpriteRenderer>();
        flameRd.sprite = null;
        flameLevelIndex = -1;
    }

    public void BurnHarder(int _index)
    {
        if (_index < flameLevel.Length)
        {
            flameRd.sprite = flameLevel[_index];
        }

        if (_index == 2)
        {
            StartCoroutine(levelComplete());
        }
    }

    private IEnumerator levelComplete()
    {
        yield return new WaitForSeconds(1f);
        levelManagerScirpt.CompleteLevel();
    }
}
