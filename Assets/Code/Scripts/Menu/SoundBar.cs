using System;
using Code.Scripts.Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Scripts.Menu
{
    public class SoundBar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject StartSoundBarObject;
        [SerializeField] private Image FullSoundBar;

        float MinSoundVolume;
        float MaxSoundVolume;
        
        private Vector3 MinSoundBarPosition;
        private Vector3 MaxSoundBarPosition;
        
        private bool bIsDragging = false;

        private void Awake()
        {
            this.MinSoundVolume = this.StartSoundBarObject.transform.position.x;
            this.MaxSoundVolume = transform.position.x;
            
        }

        private void Start()
        {
            this.MinSoundBarPosition = this.StartSoundBarObject.transform.position;
            this.MaxSoundBarPosition = transform.position;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            this.bIsDragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            this.bIsDragging = false;
        }

        private void Update()
        {
            if (!this.bIsDragging) return;
            
            Vector3 pos = transform.position;
            
            //pos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            pos.x = Input.mousePosition.x;
            
            Vector3 projectedPoint = ProjectPointOnLine(this.MinSoundBarPosition, this.MaxSoundBarPosition, pos);
            
            transform.position = projectedPoint;
            // Berechne den aktuellen Fortschritt auf der Linie
            float distanceFromStart = Vector3.Distance(transform.position, this.MinSoundBarPosition);
            float fillAmount = distanceFromStart / (this.MaxSoundBarPosition - this.MinSoundBarPosition).magnitude;

            // Setze den fillAmount des UI-Images
            this.FullSoundBar.fillAmount = fillAmount; 
        }
        
        Vector3 ProjectPointOnLine(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
        {
            Vector3 lineDirection = lineEnd - lineStart;
            float lineLength = lineDirection.magnitude;
            lineDirection.Normalize();

            float projectLength = Mathf.Clamp(Vector3.Dot(point - lineStart, lineDirection), 0.0f, lineLength);
            return lineStart + lineDirection * projectLength;
        }
    }
}