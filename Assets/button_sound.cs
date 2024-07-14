using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class button_sound : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }


       
        

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseOver()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/ui/button_hover");
    }

    private void OnMouseExit()
    {
        
    }
}
