using System;
using UnityEngine;

namespace Code
{
    public class TEST : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Debug.Log("Mouse Down");
        }

        private void OnMouseEnter()
        {
            Debug.Log("Mouse Enter");
        }
    }
}