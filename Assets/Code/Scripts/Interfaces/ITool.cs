namespace Code.Scripts.Interfaces
{
    public interface ITool
    {
        ETools Tool { get; }
        
        ElementItem ContainerElement { get; }
        
        bool SetElement(ElementItem element);
    }
}