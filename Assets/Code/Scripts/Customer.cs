using System;
using UnityEngine;

namespace Code.Scripts
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private ECocktails FavoriteCocktail;
        
        private Animator Animator;
        
        private bool bIsWalking = false;
        
        public event Action OnFinishedWalkingOut;

        private FMOD.Studio.EventInstance instance;

        public void GetCocktail(ECocktails cocktail)
        {
            if (this.FavoriteCocktail == cocktail)
            {
                Debug.Log("Customer is happy");
            }else{
                Debug.Log("Customer is not happy");
            }
            
            StartWalkingOut();
        }

        private void Start()
        {
            this.Animator = this.GetComponent<Animator>();
            StartWalkingIn();
        }
        
        public void StartWalkingIn()
        {
            instance = FMODUnity.RuntimeManager.CreateInstance("event:/LoopEvent");
            instance.start();
            this.Animator.SetTrigger("StartWalkIn");
            this.bIsWalking = true;
        }
        
        public void StartWalkingOut()
        {
            this.Animator.SetTrigger("StartWalkOut");
            this.bIsWalking = true;
        }
        
        public void ResetTriggers()
        {
            this.Animator.ResetTrigger("StartWalkIn");
            this.Animator.ResetTrigger("StartWalkOut");
        }

        public void FinishedWalkingIn()
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();
            this.bIsWalking = false;
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
    }
}