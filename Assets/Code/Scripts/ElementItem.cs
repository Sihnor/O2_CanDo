using System;
using Code.Scripts.Interfaces;
using UnityEngine;

namespace Code.Scripts
{
    public class ElementItem : MonoBehaviour, IElement
    {
        [SerializeField] private EElement StartElement;
        [SerializeField] private EStateOfMatter StartStateOfMatter;
        
        public EElement Element { get; private set; }

        public EStateOfMatter StateOfMatter { get; private set; }
        
        private void Start()
        {
            this.Element = this.StartElement;
            this.StateOfMatter = this.StartStateOfMatter;
        }
        
        public void SetElement(EElement element)
        {
            this.Element = element;
            this.StartElement = element;
        }
        
        public void SetStateOfMatter(EStateOfMatter stateOfMatter)
        {
            this.StateOfMatter = stateOfMatter;
            this.StartStateOfMatter = stateOfMatter;
        }
    }
}