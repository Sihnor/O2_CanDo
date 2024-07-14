using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts
{
    public class CocktailMixer : ToolClass, ITool
    {
        public ETools Tool { get; private set; }

        [SerializeField] private GameObject CopyElement;
        
        private Vector3 OriginalPosition;
        private Quaternion OriginalRotation;
        private Coroutine shakeCoroutine;
        
        public ElementItem ContainerElement { get; private set; }
        
        private Collider2D Collider;

        private void Awake()
        {
            this.Collider = GetComponent<Collider2D>();
        }

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
            this.OriginalPosition = transform.position;
            this.OriginalRotation = transform.rotation;
            
            StartShaking(3, .1f, .1f);
            
            yield return new WaitForSeconds(3);
            
            StopShaking();
            
            this.CopyElement.GetComponent<ElementItem>().SetStateOfMatter(EStateOfMatter.Pulver);
            Debug.Log(this.CopyElement.GetComponent<ElementItem>().StateOfMatter);
        }
        
        public void OnMouseDown()
        {
            base.OnMouseDown();
            
            this.Collider.enabled = false;
        }

        public override void OnMouseUp()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/gameplay/place_device");
            this.bIsDragging = false;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            this.Collider.enabled = true;
            
            if (hit.collider == null || hit.collider.gameObject.GetComponent<ITool>() == this.gameObject.GetComponent<ITool>()) return;
            
            ITool tool = hit.collider.gameObject.GetComponent<ITool>();

            tool?.SetElement(this.ContainerElement);
            
            DeleteElement();
        }

        private void StartShaking(float duration, float positionMagnitude, float rotationMagnitude)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/gameplay/mixer");
            if (this.shakeCoroutine != null)
            {
                StopCoroutine(this.shakeCoroutine);
            }
            this.shakeCoroutine = StartCoroutine(Shake(duration, positionMagnitude, rotationMagnitude));
        }
        
        private void StopShaking()
        {
            if (this.shakeCoroutine == null) return;
            
            StopCoroutine(this.shakeCoroutine);
            this.CopyElement.transform.position = this.OriginalPosition;
            this.CopyElement.transform.rotation = this.OriginalRotation;
        }

        private IEnumerator Shake(float duration, float positionMagnitude, float rotationMagnitude)
        {
            float elapsed = 0.0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * positionMagnitude;
                float y = Random.Range(-1f, 1f) * positionMagnitude;
                float z = Random.Range(-1f, 1f) * positionMagnitude;

                float rotX = Random.Range(-1f, 1f) * rotationMagnitude;
                float rotY = Random.Range(-1f, 1f) * rotationMagnitude;
                float rotZ = Random.Range(-1f, 1f) * rotationMagnitude;

                transform.localPosition = new Vector3(this.OriginalPosition.x + x, this.OriginalPosition.y + y, this.OriginalPosition.z + z);
                transform.localRotation = new Quaternion(this.OriginalRotation.x + rotX, this.OriginalRotation.y + rotY, this.OriginalRotation.z + rotZ, this.OriginalRotation.w);

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = this.OriginalPosition;
            transform.localRotation = this.OriginalRotation; 
        }
    }
}