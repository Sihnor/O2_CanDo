using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DropDownUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject ListObject;

    [SerializeField] private float startPos;
    [SerializeField] private float endPos;
    private RectTransform rectTransform;

    bool drag;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        drag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        drag = false;
    }
    private void Update()
    {
        if(drag)
        {
            rectTransform.position = new Vector3(rectTransform.position.x, Mathf.Clamp(Input.mousePosition.y, endPos, startPos));
            Debug.Log(Input.mousePosition.y);
        }
    }
}
