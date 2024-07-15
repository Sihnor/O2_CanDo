using Code.Scripts;
using Code.Scripts.Interfaces;
using UnityEngine;

public class ElementClass : MonoBehaviour, IDraggable
{
    public bool bIsDragging { get; protected set; }

    [SerializeField] private GameObject ElementItem;
    private GameObject ElementItemInstance;
    [SerializeField] private EStateOfMatter FutureState;
    bool isEnabled;
    private void OnEnable()
    {
        isEnabled = true;
    }

    private void OnDisable()
    {
        isEnabled = false;
    }
    public void Start()
    {
        this.bIsDragging = false;

        this.ElementItemInstance = null;
    }

    public void OnMouseDown()
    {
        if (!isEnabled) return;
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
        if (!isEnabled) return;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 5;
        this.ElementItemInstance.transform.position = mousePosition;
    }

    public void OnMouseUp()
    {
        if (!isEnabled) return;
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