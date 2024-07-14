using System;
using Code.Scripts.Interfaces;
using UnityEngine;

namespace Code.Scripts
{
    public class Cocktail : MonoBehaviour, IDraggable
    {
        [SerializeField] private ECocktails ActualCocktail;
        BoxCollider2D BoxCollider2D;

        private void Start()
        {
            this.BoxCollider2D = this.GetComponent<BoxCollider2D>();
        }

        public bool bIsDragging { get; private set; }

        public void OnMouseDown()
        {
            this.bIsDragging = true;
            this.BoxCollider2D.enabled = false;
        }

        public void OnMouseDrag()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            this.transform.position = new Vector3(mousePosition.x, mousePosition.y, this.transform.position.z);
        }

        public void OnMouseUp()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                     
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                // Check if tag is hit
                if (!hit.collider.CompareTag("Customer")) goto END;
                
                Customer customer = hit.collider.gameObject.GetComponent<Customer>();
                customer.GetCocktail(this.ActualCocktail);
                
                Destroy(this.gameObject);
            }
            
            END:
            this.BoxCollider2D.enabled = true;
        }
    }
}