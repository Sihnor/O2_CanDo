using System;
using Code.Scripts.Interfaces;
using Code.Scripts.Menu;
using UnityEngine;

public class DropDownUI : MonoBehaviour, IDraggable 
{
    [SerializeField] private GameObject ListObject;
    [SerializeField] private GameObject ListElements;

    [SerializeField] private float startPos;
    [SerializeField] private float endPos;

    Vector3 ListOffset;
    Vector3 MouseOffset;

    public bool bIsDragging { get; }

    private void Start()
    {
        this.startPos = this.ListObject.transform.position.y;
    }

    public void OnMouseDown()
    {
        this.ListOffset = this.ListObject.transform.position;
        this.MouseOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnMouseDrag()
    {
        Vector3 pos = this.ListObject.transform.position;

        pos.y = (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - this.MouseOffset.y + this.ListOffset.y);
        
        if(pos.y > this.startPos)
            pos.y = this.startPos;
        else if(pos.y < this.endPos)
            pos.y = this.endPos;
        
        this.ListObject.transform.position = pos;
    }

    public void OnMouseUp()
    {
        this.ListElements.GetComponent<ElementMenuScroll>().SetOffset();
    }
}
