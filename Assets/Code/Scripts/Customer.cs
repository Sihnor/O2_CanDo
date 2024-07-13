using System;
using UnityEngine;

namespace Code.Scripts
{
    public class Customer : MonoBehaviour
    {
        private Animator Animator;

        private void Start()
        {
            this.Animator = this.GetComponent<Animator>();
        }
        
        public void StartWalkingIn()
        {
            this.Animator.SetTrigger("StartWalkIn");
        }
        
        public void StartWalkingOut()
        {
            this.Animator.SetTrigger("StartWalkOut");
        }

        public void FinishedWalkingIn()
        {
            Debug.Log("Customer has finished walking in");
        }
        
        public void FinishedWalkingOut()
        {
            Debug.Log("Customer has finished walking out");
        }

        private void OnMouseDown()
        {
            StartWalkingOut();
        }
    }
}