using Code.Scripts;
using Code.Scripts.Interfaces;
using UnityEngine;

public class ElementClass : MonoBehaviour, IDraggable
{
    public bool bIsDragging { get; protected set; }

    [SerializeField] private GameObject ElementItem;
    private GameObject ElementItemInstance;
    [SerializeField] private EStateOfMatter FutureState;

    public void Start()
    {
        this.bIsDragging = false;

        this.ElementItemInstance = null;
    }

    public void OnMouseDown()
    {
        this.bIsDragging = true;

        this.ElementItemInstance = Instantiate(this.ElementItem, this.transform.position, Quaternion.identity);
        Vector3 temp = new Vector3(this.ElementItemInstance.transform.position.x, this.ElementItemInstance.transform.position.y, 10);
        this.ElementItemInstance.transform.position = temp;

        switch (this.FutureState)
        {
            case EStateOfMatter.Solid:
                FMODUnity.RuntimeManager.PlayOneShot("event:/gameplay/material_hard");
                break;
            case EStateOfMatter.Liquid:
                FMODUnity.RuntimeManager.PlayOneShot("event:/gameplay/material_liquid");
                break;
            case EStateOfMatter.Gas:
                FMODUnity.RuntimeManager.PlayOneShot("event:/gameplay/material_gas");
                break;
        }
        
    }

    public void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 5;
        this.ElementItemInstance.transform.position = mousePosition;
    }

    public void OnMouseUp()
    {
        this.bIsDragging = false;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/gameplay/material_drop");
            ITool tool = hit.collider.gameObject.GetComponent<ITool>();

            tool?.SetElement(this.ElementItemInstance.GetComponent<ElementItem>());
        }
        
        Destroy(this.ElementItemInstance);
    }

}