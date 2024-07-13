using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Code.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropDownUI : MonoBehaviour, IDraggable 
{
    [SerializeField] private GameObject ListObject;

    [SerializeField] private float startPos;
    [SerializeField] private float endPos;

    Vector3 ListOffset;
    Vector3 MouseOffset;

    public bool bIsDragging { get; }

    public void OnMouseDown()
    {
        this.ListOffset = this.ListObject.transform.position;
        this.MouseOffset = Input.mousePosition;
    }

    public void OnMouseDrag()
    {
        Vector3 pos = this.ListObject.transform.position;
            
        pos.y = Input.mousePosition.y - this.MouseOffset.y + this.ListOffset.y;
        
        this.ListObject.transform.position = pos;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.x = transform.position.x;
        mousePosition.z = transform.position.z;
        transform.position = mousePosition;
    }

    public void OnMouseUp()
    {
    }
}
