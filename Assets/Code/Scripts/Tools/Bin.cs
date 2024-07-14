using System;
using System.Collections.Generic;
using Code.Scripts.Interfaces;
using UnityEngine;

namespace Code.Scripts
{
    public class Bin : MonoBehaviour, ITransfer, IAnimation
    {
        [SerializeField] private List<GameObject> Trash = new List<GameObject>();

        public void TransferElements(List<ElementItem> elements)
        {
        }

        public void OnMouseEnter()
        {
            this.Trash[0].SetActive(false);
            this.Trash[1].SetActive(true);
        }

        public void OnMouseExit()
        {
            this.Trash[0].SetActive(true);
            this.Trash[1].SetActive(false);
        }
    }
}