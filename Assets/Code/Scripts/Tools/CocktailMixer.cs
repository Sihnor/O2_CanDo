using System.Collections;
using Code.Scripts.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Scripts
{
    public class CocktailMixer : ToolClass, ITool
    {
        public ETools Tool { get; private set; }

        private GameObject CopyElement;

        public ElementItem ContainerElement { get; private set; }

        [SerializeField] private ElementItem DebugItem;

        public bool SetElement(ElementItem element)
        {
            if (this.CopyElement) return false;
            if (element.StateOfMatter is not EStateOfMatter.Solid) return false;

            this.CopyElement = new GameObject();
            this.CopyElement.AddComponent<ElementItem>();

            this.ContainerElement = this.CopyElement.GetComponent<ElementItem>();
            
            this.ContainerElement.SetElement(element.Element);
            this.ContainerElement.SetStateOfMatter(element.StateOfMatter);

            this.DebugItem = this.ContainerElement;

            Debug.Log(this.DebugItem.StateOfMatter);
            
            StartCoroutine(nameof(MixElements));
            return true;
        }

        private void DeleteElement()
        {
            Destroy(this.CopyElement);
        }

        private IEnumerator MixElements()
        {
            Debug.Log("Mixing Elements");
            yield return new WaitForSeconds(3);
            Debug.Log("Elements Mixed");
            Debug.Log(this.DebugItem.StateOfMatter);
            this.DebugItem.SetStateOfMatter(EStateOfMatter.Pulver);
            Debug.Log(this.DebugItem.StateOfMatter);
            Debug.Log(this.CopyElement.GetComponent<ElementItem>().StateOfMatter);
        }

        public override void OnMouseUp()
        {
            this.bIsDragging = false;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider == null || hit.collider.gameObject.GetComponent<ITool>() == this.gameObject.GetComponent<ITool>()) return;
            
            Debug.Log(hit.collider.gameObject.name);
            ITool tool = hit.collider.gameObject.GetComponent<ITool>();

            if (this.DebugItem) tool?.SetElement(this.DebugItem);
            
            DeleteElement();
        }
    }
}