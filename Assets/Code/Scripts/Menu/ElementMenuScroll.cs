using UnityEngine;

namespace Code.Scripts.Menu
{
    public class ElementMenuScroll : MonoBehaviour
    {
        [SerializeField] private GameObject ListElements;

        [SerializeField] private GameObject MinPos;
        [SerializeField] private GameObject MaxPos;

        float MinScroll;
        float MaxScroll;

        Vector3 ListOffset;

        float GlobalScroll = 0;

        private bool bIsInside = false;

        public void SetOffset()
        {
            this.MinScroll = this.MinPos.transform.position.y;
            this.MaxScroll = this.MaxPos.transform.position.y;
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            bIsInside = false;
            foreach (var hit in hits)
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    bIsInside = true;
                }
            }
            Debug.Log(bIsInside);
            if (!this.bIsInside) return;

            float scroll = Input.mouseScrollDelta.y / 4;

            if (scroll == 0) return;

            Vector3 position = this.ListElements.transform.position;

            position.y -= scroll;

            position.y = Mathf.Clamp(position.y, this.MinScroll, this.MaxScroll);
            this.ListElements.transform.position = position;
        }
    }
}