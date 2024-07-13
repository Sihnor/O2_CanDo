using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.Managers
{
    public class SceneLoader : MonoBehaviour
    {
        #region Singleton
        public static SceneLoader Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        [SerializeField] private EScenes StartScene = EScenes.MainMenu;

        private void Start()
        {
            LoadScene(this.StartScene);
        }
        public void LoadScene(EScenes scene, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene((int)scene, loadSceneMode);
        }
        public void UnloadScene(EScenes scene)
        {
            if (SceneManager.GetSceneByName(scene.ToString()).isLoaded)
            {
                SceneManager.UnloadSceneAsync((int)scene);
            }
        }
    }
}