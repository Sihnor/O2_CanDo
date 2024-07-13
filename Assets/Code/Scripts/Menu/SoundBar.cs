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
        
        private bool bIsDragging = false;

        private void Awake()
        {
            this.MinSoundVolume = this.StartSoundBarObject.transform.position.x;
            this.MaxSoundVolume = transform.position.x;
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
            
            pos.x = Mathf.Clamp(pos.x, this.MinSoundVolume, this.MaxSoundVolume);
            
            transform.position = pos;
            
             float diff = this.MaxSoundVolume - this.MinSoundVolume;

             float scaledToZero = pos.x - this.MinSoundVolume;
             
             float volume = scaledToZero/diff;
             
             this.FullSoundBar.fillAmount = volume;
             
             GameManager.Instance.SoundVolume = volume;
        }
    }
}