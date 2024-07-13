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

        public bool SetElement(ElementItem element)
        {
            if (element.StateOfMatter is EStateOfMatter.Solid) return false;
            
            GameObject CopyElement = new GameObject();
            CopyElement.AddComponent<ElementItem>();
            
            CopyElement.GetComponent<ElementItem>().SetElement(element.Element);
            CopyElement.GetComponent<ElementItem>().SetStateOfMatter(element.StateOfMatter);

            #region MEINE_CODE

            

            #endregion
            
            this.ContainerElement = CopyElement.GetComponent<ElementItem>();
            
            this.ElementItems.Add(this.ContainerElement);
            this.ElementItem = this.ContainerElement;            
            this.ContainerElement = element;
            return true;
        }

        public override void OnMouseUp()
        {
            this.bIsDragging = false;
        }
    }
}