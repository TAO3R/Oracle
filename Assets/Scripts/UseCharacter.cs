using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UseCharacter : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private GetCharacter getCharaScript;

    private Image image;
    private GameObject dup;

    [SerializeField] private RectTransform dupRectTrans;
    [SerializeField] private Canvas dupCanvas;
    [SerializeField] private RectTransform dupCanvasRectTrans;

    private void OnEnable()
    {
        getCharaScript = GetComponent<GetCharacter>();
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag being called by " + name);
        
        if (!getCharaScript.earned) { return; }

        dup = new GameObject("Draging");
        dup.transform.SetParent(eventData.pointerDrag.transform.parent.parent);

        dup.transform.localPosition = Vector3.zero;
        dup.transform.localScale = Vector3.one;

        Image dupImage = dup.AddComponent<Image>();
        dupImage.sprite = image.sprite;
        dupImage.SetNativeSize();
        dupImage.raycastTarget = false;

        dupRectTrans = dup.GetComponent<RectTransform>();
        dupRectTrans.anchoredPosition = Vector2.zero;

        Debug.Log("Dup anchored at: " + dupRectTrans.anchoredPosition);

        // dupRectTrans.localScale = Vector3.one;
        dupRectTrans.localScale = eventData.pointerDrag.transform.GetComponent<RectTransform>().localScale;
        // throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("OnDrag being called by " + name);

        if (!getCharaScript.earned || dup == null) { return; }

        dupCanvas = dup.GetComponentInParent<RectTransform>().gameObject.GetComponentInParent<RectTransform>().gameObject.GetComponentInParent<Canvas>();
        dupCanvasRectTrans = dupCanvas.gameObject.GetComponent<RectTransform>();

        Vector2 screenSize = dupCanvas.GetComponent<Canvas>().pixelRect.size;
        // Debug.Log("screen size: " + screenSize);
        Vector2 canvasSize = dupCanvasRectTrans.GetComponent<RectTransform>().sizeDelta;
        // Debug.Log("canvas size: " + canvasSize);

        Vector3 mousePos = Input.mousePosition;
        // Debug.Log("mouse position in pixels: " + mousePos);

        dupRectTrans.anchoredPosition = new Vector2((mousePos.x - screenSize.x / 2f) / screenSize.x * canvasSize.x, (mousePos.y - screenSize.y / 2f) / screenSize.y * canvasSize.y);
        // Debug.Log("dup anchored at " + dupRectTrans.anchoredPosition);

        // dup.transform.position = Input.mousePosition;

        // throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag being called by " + name);

        if (!getCharaScript.earned) { return; }
       
        Destroy(dup);
        dup = null;

        // throw new System.NotImplementedException();
    }
}
