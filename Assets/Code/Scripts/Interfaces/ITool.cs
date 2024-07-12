namespace Code.Scripts.Interfaces
{
    public interface ITool
    {
        ETools Tool { get; }
        
        void SetElement(ElementItem element);
    }
}