using System;
using UnityEngine;

namespace Code.Scripts.Menu
{
    public class ElementMenuScroll : MonoBehaviour
    {
        [SerializeField] private GameObject ListElements;
        float MinScroll;
        float MaxScroll = 5.46f;
        
        private bool bIsInside = false;

        private void Start()
        {
            this.MinScroll = this.ListElements.transform.position.y;
            Debug.Log(this.MinScroll);
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