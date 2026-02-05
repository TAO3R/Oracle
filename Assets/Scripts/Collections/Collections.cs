using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collections : MonoBehaviour
{
    [SerializeField]
    [Tooltip("An array that stores detailed introduction of collections")]
    private Sprite[] Details;

    [SerializeField] private Sprite[] characterIntro;

    [SerializeField] private LevelManager levelManagerScript;

    /*A dictionary that stores the label of each collectable objects
     *and the index of their corresponding introductory images in the "Details" array
     */
    private Dictionary<string, int> nameIndexDic;

    // Two children that displays the detailed intro of collections
    [SerializeField]
    private Image detailZero, detailOne;

    private void Awake()
    {
        // Dictionary initialization
        nameIndexDic = new Dictionary<string, int>();
        
        // Adding key-value pairs
        nameIndexDic.Add("Yupei", 0);
        nameIndexDic.Add("Wenlei", 1);
        nameIndexDic.Add("Tongding", 2);
        nameIndexDic.Add("Taopen", 3);
        nameIndexDic.Add("Taoge", 4);
    }
    void Start()
    {
        //  Grab a refernece to gameobjects for displaying details and disactivate them
        GrabChildRefernece();
        CloseDetail();
    }

    void Update()
    {
        
    }

    /*public int GetIntroIndex(string _name)
    {
        return nameIndexDic[_name];
    }*/

    public void SetDetailSprite(string _name)
    {
        int index = nameIndexDic[_name];

        detailZero.sprite = Details[index];
        detailOne.sprite = detailZero.sprite;
    }

    public void SetIntroSprite(int _index)
    {
        detailZero.sprite = characterIntro[_index];
        detailOne.sprite = characterIntro[_index];
    }

    public void OpenDetail()
    {
        gameObject.SetActive(true);

        // Initialize the image to null
        detailZero.sprite = null;
        detailOne.sprite = null;
    }

    public void CloseDetail()   // Used by the cross button
    {
        if (detailZero.sprite == characterIntro[0])
        {   // Mu
            levelManagerScript.coroutineUnfocus();
            levelManagerScript.GetMu();
        }
        else if (detailZero.sprite == characterIntro[1])
        {   // Wang
            levelManagerScript.GetWang();
        }
        else if (detailZero.sprite == characterIntro[2])
        {   // Huo
            levelManagerScript.GetHuo();
        }

        gameObject.SetActive(false);
    }

    private void GrabChildRefernece()
    {
        detailZero = transform.GetChild(0).GetComponent<Image>();
        detailOne = transform.GetChild(1).GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (detailZero == detailOne)    // Both are null
        {
            GrabChildRefernece();
        }
    }

    private void OnDisable()
    {
        detailZero.sprite = null;
        detailOne.sprite = null;
    }
}
