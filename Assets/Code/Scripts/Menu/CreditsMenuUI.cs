using Code.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Menu
{
    public class CreditsMenuUI : MonoBehaviour
    {
        [SerializeField] private Button BackButton;
        
        private void Start()
        {
            this.BackButton.onClick.AddListener(this.OnBackButton);
        }
        
        private void OnBackButton()
        {
            SceneLoader.Instance.LoadScene(EScenes.MainGame);
        }
    }
}