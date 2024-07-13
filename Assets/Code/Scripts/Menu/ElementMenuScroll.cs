using System;
using UnityEngine;

namespace Code.Scripts.Menu
{
    public class ElementMenuScroll : MonoBehaviour
    {
        [SerializeField] private GameObject ListElements;
        
        [SerializeField] private GameObject MinPos;
        float MinScroll;
        float MaxScroll = 13.54f;
        
        Vector3 ListOffset;
        
        float GlobalScroll = 0;
        
        private bool bIsInside = false;
        
        public void SetOffset()
        {
            this.MinScroll = this.MinPos.transform.position.y;
            this.MaxScroll = this.MinScroll + 13.54f;
        }

        private void OnMouseEnter()
        {
            this.bIsInside = true;
        }

        private void OnMouseExit()
        {
            this.bIsInside = false;
        }

        private void Update()
        {
            if (!this.bIsInside) return;
            
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll == 0) return;
            
            Vector3 position = this.ListElements.transform.position;
            
            position.y -= scroll;
            
            position.y = Mathf.Clamp(position.y, this.MinScroll, this.MaxScroll);
            this.ListElements.transform.position = position;
        }
    }
}