namespace Code.Scripts.Interfaces
{
    public interface IDraggable
    {
        bool bIsDragging { get;  }
        
        void OnMouseDown();
        void OnMouseDrag();
        void OnMouseUp();
    }
}