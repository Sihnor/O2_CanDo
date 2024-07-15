using System;
using Code.Scripts.Interfaces;
using UnityEngine;

namespace Code.Scripts
{
    public abstract class ToolClass : MonoBehaviour, IDraggable
    {
        [SerializeField] private ETools StartTool;
        
        public bool bIsDragging { get; protected set; }

        [SerializeField] protected Vector3 StartPosition = Vector3.zero;
        [SerializeField] protected Vector3 EndPosition = Vector3.zero;

        private float fWidth = 0.0f;
        private float fHeight = 0.0f;
        bool isEnabled;
        private void OnEnable()
        {
            isEnabled = true;
        }

        private void OnDisable()
        {
            isEnabled = false;
        }
        public void OnMouseDown()
        {
            if (!isEnabled) return;
            FMODUnity.RuntimeManager.PlayOneShot("event:/gameplay/grab_device");
            this.bIsDragging = true;
            this.fHeight = this.EndPosition.y / 2;
            this.fWidth = this.EndPosition.x / 2;
            
            //Vector3 currentPosition = this.transform.position;
            //currentPosition.z = -0.5f;
            //this.transform.position = currentPosition;
        }

        public void OnMouseDrag()
        {
            if (!isEnabled) return;
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

        public abstract void OnMouseUp();

        private void OnDrawGizmos()
        {
            if (!isEnabled) return;
            FMODUnity.RuntimeManager.PlayOneShot("event:/gameplay/grab_device");
            // Draw red rectangle  without infill only border on the bottom of the screen
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(this.StartPosition, this.EndPosition);
            
        }

    }
}