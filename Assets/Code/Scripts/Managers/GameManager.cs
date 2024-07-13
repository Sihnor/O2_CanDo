using UnityEngine;

namespace Code.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance { get; private set; }

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

        public float SoundVolume { get;  set; } = 1f;
        
    }
}
        