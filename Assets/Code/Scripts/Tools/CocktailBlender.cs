using Code.Scripts.Interfaces;
using UnityEngine;

namespace Code.Scripts
{
    public class CocktailBlender : ToolClass, ITool
    {
        public ETools Tool { get; private set; }

        public ElementItem ContainerElement { get; private set; }

        public void SetElement(ElementItem element)
        {
            this.ContainerElement = element;
        }
    }
}