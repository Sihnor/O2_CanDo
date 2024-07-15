using Code.Scripts.Managers;
using System;
using UnityEngine;

namespace Code.Scripts
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private ECocktails FavoriteCocktail;
        [SerializeField] private GameObject TextBubble;
        
        private Animator Animator;
        
        private bool bIsWalking = false;
        
        
        public event Action OnFinishedWalkingOut;

        private FMOD.Studio.EventInstance instance;

        public void GetCocktail(ECocktails cocktail)
        {
            if (this.FavoriteCocktail == cocktail)
            {
                GameManager.Instance.CustomerServed++;
                GameManager.Instance.CustomerServedRight++;
                FMODUnity.RuntimeManager.PlayOneShot("event:/stinger/victory");
            }
            else
            {
                GameManager.Instance.CustomerServed++;
                GameManager.Instance.CustomerServedWrong++;
                FMODUnity.RuntimeManager.PlayOneShot("event:/stinger/defeat");
            }
            
            StartWalkingOut();
        }

        private void Awake()
        {
            this.Animator = this.GetComponent<Animator>();
        }

        private void Start()
        {
            return;
            
            StartWalkingIn();
        }

        private void OnEnable()
        {
            this.Animator = this.GetComponent<Animator>();
            StartWalkingIn();
        }

        public void StartWalkingIn()
        {
            GameObject temp = new GameObject();
            Debug.Log("Customer is walking in");
            this.Animator.SetTrigger("StartWalkIn");
            this.bIsWalking = true;
            instance = FMODUnity.RuntimeManager.CreateInstance("event:/gameplay/footsteps_sand");
            instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(temp));
            
            instance.start();
            Destroy(temp);
        }
        
        public void StartWalkingOut()
        {
            this.Animator.SetTrigger("StartWalkOut");
            this.bIsWalking = true;
            DisableTextBlase();
        }
        
        public void ResetTriggers()
        {
            this.Animator.ResetTrigger("StartWalkIn");
            this.Animator.ResetTrigger("StartWalkOut");
        }

        public void FinishedWalkingIn()
        {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.release();
            this.bIsWalking = false;
            
            this.TextBubble.SetActive(true);
            
            Invoke(nameof(DisableTextBlase), 5);
        }
        
        public void FinishedWalkingOut()
        {
            Debug.Log("Customer has left");
            ResetTriggers();
            this.bIsWalking = false;
            OnFinishedWalkingOut?.Invoke();
        }

        private void OnMouseUpAsButton()
        {
            if (this.bIsWalking) return;
            StartWalkingOut();
        }

        private void DisableTextBlase()
        {
            this.TextBubble.SetActive(false);
        }
    }
}