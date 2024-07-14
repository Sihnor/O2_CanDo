using System.Collections.Generic;

namespace Code.Scripts.Interfaces
{
    public interface ITool
    {
        ETools Tool { get; }
        
        ElementItem ContainerElement { get; }
        
        void SetElement(ElementItem element);

    }
}