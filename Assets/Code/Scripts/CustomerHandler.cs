using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts
{
    public class CustomerHandler : MonoBehaviour
    {
        [SerializeField] List<GameObject> customers = new List<GameObject>();
        
        private GameObject CurrentCustomer;
        
        private void Start()
        {
            foreach (GameObject customer in this.customers)
            {
                Customer customerComponent = customer.GetComponent<Customer>();
                customerComponent.OnFinishedWalkingOut += ActivateCustomer;
                customer.SetActive(false);
            }
            
            ActivateCustomer();
        }
        
        private void ActivateCustomer()
        {
            if (this.CurrentCustomer)
            {
                this.CurrentCustomer.SetActive(false);
                this.CurrentCustomer.transform.position = Vector3.zero;
            }
            
            int randomIndex = Random.Range(0, this.customers.Count);
            this.CurrentCustomer = this.customers[randomIndex];
            this.customers[randomIndex].transform.position = new Vector3(38.5f, 7.5f, 0.8f);
            
            this.customers[randomIndex].SetActive(true);
        }
        
        
    }
}