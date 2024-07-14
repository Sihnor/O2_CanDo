using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Scripts
{
    public class CocktailMixer : ToolClass, ITool
    {
        public ETools Tool { get; private set; }

        [SerializeField] private GameObject CopyElement;

        public ElementItem ContainerElement { get; private set; }

        public void SetElement(ElementItem element)
        {
            if (this.CopyElement) return ;
            if (element.StateOfMatter is not EStateOfMatter.Solid) return ;

            this.CopyElement = new GameObject();
            this.CopyElement.AddComponent<ElementItem>();

            this.ContainerElement = this.CopyElement.GetComponent<ElementItem>();
            
            this.ContainerElement.SetElement(element.Element);
            this.ContainerElement.SetStateOfMatter(element.StateOfMatter);

            StartCoroutine(nameof(MixElements));
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
            this.CopyElement.GetComponent<ElementItem>().SetStateOfMatter(EStateOfMatter.Pulver);
            Debug.Log(this.CopyElement.GetComponent<ElementItem>().StateOfMatter);
        }

        public override void OnMouseUp()
        {
            this.bIsDragging = false;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            Vector3 currentPosition = this.transform.position;
            currentPosition.z = -1.0f;
            this.transform.position = currentPosition;
            if (hit.collider == null || hit.collider.gameObject.GetComponent<ITool>() == this.gameObject.GetComponent<ITool>()) return;
            
            Debug.Log(hit.collider.gameObject.name);
            ITool tool = hit.collider.gameObject.GetComponent<ITool>();

            tool?.SetElement(this.ContainerElement);
            
            DeleteElement();
        }
    }
}