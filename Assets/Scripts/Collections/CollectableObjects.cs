using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObjects : MonoBehaviour
{
    public Collections collectionScript;

    private void OnMouseDown()
    {
        collectionScript.OpenDetail();
        collectionScript.SetDetailSprite(gameObject.tag);
    }
}
