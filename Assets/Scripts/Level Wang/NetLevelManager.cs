using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetLevelManager : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    [SerializeField] private GameObject lines, scoringArea, button, thirdActDialog;
    [SerializeField] private int score;

    [Header("Pop-up Intro")]
    [SerializeField] private GameObject popUpIntro;
    [SerializeField] private Sprite[] introSprites;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;

        scoringArea.transform.GetChild(0).gameObject.SetActive(true);

        for (int i = 1; i < scoringArea.transform.childCount; i++)
        {
            scoringArea.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < lines.transform.childCount; i++)
        {
            lines.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }

        popUpIntro.SetActive(false);
        button.SetActive(true);
    }

    private void OnEnable()
    {
        score = 0;

        scoringArea.transform.GetChild(0).gameObject.SetActive(true);

        for (int i = 1; i < scoringArea.transform.childCount; i++)
        {
            scoringArea.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < lines.transform.childCount; i++)
        {
            lines.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }

        popUpIntro.SetActive(false);
        button.SetActive(true);
    }

    private void OnMouseDown()
    {
        
        if (Hit())
        {
            Score();
            
            if (score == 6)
            {
                StartCoroutine(CompleteLevel());
            }
        }
    }

    private bool Hit()
    {
        float angle = arrow.eulerAngles.z;
        Debug.Log(angle);
        
        switch (this.score)
        {
            case 0:
                if ((26 > angle && angle >= 0) || (360 > angle && angle > 313)) { return true; }
                break;
            case 1:
                if (61 < angle && angle < 117) { return true; }
                break;
            case 2:
                if (241 < angle && angle < 268) { return true; }
                break;
            case 3:
                if (154.4 < angle && angle <204) { return true; }
                break;
            case 4:
                if (251 < angle && angle < 282) { return true; }
                break;
            case 5:
                if (146 < angle && angle < 188) { return true; }
                break;
            
            default:
                break;
        }
        
        return false;
    }

    private void Score()
    {
        if (score >= 6) { return; }

        // 取消当前判定区
        scoringArea.transform.GetChild(score).gameObject.SetActive(false);

        // 鱼和贝壳的介绍弹窗，暂停时间
        if (score == 0)
        {
            IntroPopUp(0);
        }
        else if (score == 2)
        {
            IntroPopUp(1);
        }

        // 增加收获物图示, 更新得分
        lines.transform.GetChild(score++).GetChild(0).gameObject.SetActive(true);

        if (score >= 6) { return; }

        // 更新判定区到下一级
        scoringArea.transform.GetChild(score).gameObject.SetActive(true);
        // 反转箭头旋转方向
        arrow.gameObject.GetComponent<NetLeevlArrow>().clockwise = !arrow.gameObject.GetComponent<NetLeevlArrow>().clockwise;
    }

    private void IntroPopUp(int _index)
    {
        popUpIntro.SetActive(true);
        for (int i = 0; i < popUpIntro.transform.childCount; i++)
        {
            popUpIntro.transform.GetChild(i).gameObject.SetActive(true);
        }

        popUpIntro.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = introSprites[_index];
        Time.timeScale = 0;

    }

    private IEnumerator CompleteLevel()
    {
        yield return new WaitForSeconds(0.5f);
        thirdActDialog.SetActive(true);
        gameObject.SetActive(false);
    }
}
