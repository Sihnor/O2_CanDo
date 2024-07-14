using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts
{
    [System.Serializable] public struct SRecipe
    {
        public List<SElement> Elements;
        public GameObject ResultObject;
        
        public SRecipe(List<SElement> elements, GameObject resultObject)
        {
            this.Elements = elements;
            this.ResultObject = resultObject;
        }
    }
    
    [System.Serializable] public struct SElement
    {
        public EElement Element;
        public EStateOfMatter StateOfMatter;
        
        public SElement(EElement element, EStateOfMatter stateOfMatter)
        {
            this.Element = element;
            this.StateOfMatter = stateOfMatter;
        }
    }
}