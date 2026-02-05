using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    private Vector3 currentGrid, targetGrid;

    [SerializeField]
    [Tooltip("Specifies the time spent for camera to move from one grid to another")]
    private float timeOfMove = 5f;

    [SerializeField]
    private float timeSpent = 0f;

    /*[SerializeField]
    private float speed = 0.5f;*/

    [SerializeField]
    private Transform spawnGrid;

    private void Awake()
    {
        transform.position = new Vector3(spawnGrid.position.x, spawnGrid.position.y, -10);
    }

    void Start()
    {
        currentGrid = gameObject.transform.position;
        targetGrid = currentGrid;
    }

    void Update()
    {
        if (currentGrid != targetGrid)
        {
            if (timeSpent < timeOfMove)
            {
                timeSpent += Time.deltaTime;
                Vector3 pos = new Vector3 (Vector3.Lerp(currentGrid, targetGrid, timeSpent / timeOfMove).x,
                                           Vector3.Lerp(currentGrid, targetGrid, timeSpent / timeOfMove).y,
                                           transform.position.z);

                transform.position = pos;
            }
            else
            {
                timeSpent = 0f;
                currentGrid = targetGrid;
            }
        }
    }

    public Vector3 GetTargetGrid()
    {
        return this.targetGrid;
    }   // Getter of targetGrid

    public void SetTargetGrid(Transform _targetGrid)
    {
        this.targetGrid = new Vector3(_targetGrid.position.x, _targetGrid.position.y, -10);
    }   // Setter of targetGrid
}
