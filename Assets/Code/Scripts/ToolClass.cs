using System;
using Code.Scripts.Interfaces;
using UnityEngine;

namespace Code.Scripts
{
    public class ToolClass : MonoBehaviour, ITool, IDraggable
    {
        [SerializeField] private ETools StartTool;
        
        private EElement CurrentElement;
        
        public ETools Tool { get; private set; }

        public void SetElement(ElementItem element)
        {
        }

        private void Start()
        {
            this.Tool = this.StartTool;
        }
        
        public void SetElement(EElement element)
        {
            this.CurrentElement = element;
        }

        public bool bIsDragging { get; private set; }

        public Vector3 StartPosition = Vector3.zero;
        public Vector3 EndPosition = Vector3.zero;

        private float fWidth = 0.0f;
        private float fHeight = 0.0f;
        
        public void OnMouseDown()
        {
            this.bIsDragging = true;
            
            this.fHeight = this.EndPosition.y / 2;
            this.fWidth = this.EndPosition.x / 2;
        }

        public void OnMouseDrag()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            float minX = this.StartPosition.x - this.fWidth;
            float maxX = this.StartPosition.x + this.fWidth;
            float minY = this.StartPosition.y - this.fHeight;
            float maxY = this.StartPosition.y + this.fHeight;
            
            if (mousePosition.x < minX)
            {
                mousePosition.x = minX;
            }
            else if (mousePosition.x > maxX)
            {
                mousePosition.x = maxX;
            }
            
            if (mousePosition.y < minY)
            {
                mousePosition.y = minY;
            }
            else if (mousePosition.y > maxY)
            {
                mousePosition.y = maxY;
            }
            
            this.transform.position = new Vector3(mousePosition.x, mousePosition.y, this.transform.position.z);
        }

        public void OnMouseUp()
        {
            this.bIsDragging = false;
        }

        private void OnDrawGizmos()
        {
            // Draw red rectangle  without infill only border on the bottom of the screen
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(this.StartPosition, this.EndPosition);
            
        }

    }
}