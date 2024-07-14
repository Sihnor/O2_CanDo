using System.Collections.Generic;
using Code.Scripts.Interfaces;
using UnityEngine;

namespace Code.Scripts
{
    public class CocktailShaker : ToolClass, ITool
    {
        public ETools Tool { get; private set; }
        
        public ElementItem ContainerElement { get; private set; }
        
        [SerializeField] private List<ElementItem> ElementItems = new List<ElementItem>();
        
        [SerializeField] ElementItem ElementItem;

        public void SetElement(ElementItem element)
        {
            if (!element) return;
            if (element.StateOfMatter is EStateOfMatter.Solid) return ;
            
            GameObject CopyElement = new GameObject();
            CopyElement.AddComponent<ElementItem>();
            
            CopyElement.GetComponent<ElementItem>().SetElement(element.Element);
            CopyElement.GetComponent<ElementItem>().SetStateOfMatter(element.StateOfMatter);
            
            this.ContainerElement = CopyElement.GetComponent<ElementItem>();
            
            this.ElementItems.Add(this.ContainerElement);
            this.ElementItem = this.ContainerElement;            
            this.ContainerElement = element;
        }

        public override void OnMouseUp()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/gameplay/place_device");
            this.bIsDragging = false;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                     
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            
            Vector3 currentPosition = this.transform.position;
            currentPosition.z = -1.0f;
            this.transform.position = currentPosition;
            
            if (this.ElementItems.Count == 0) return;
            
            if (hit.collider != null)
            {
                if (hit.collider == null ) return;
                
                ITransfer transfer = hit.collider.gameObject.GetComponent<ITransfer>();
                
                transfer?.TransferElements(this.ElementItems);
                
                // Destroy all gameobjects in the list
                foreach (ElementItem element in this.ElementItems)
                {
                    Destroy(element.gameObject);
                }
                this.ElementItems.Clear();
            }
        }
        
        public void Start()
        {
            this.bIsDragging = false;
        }
    }
}