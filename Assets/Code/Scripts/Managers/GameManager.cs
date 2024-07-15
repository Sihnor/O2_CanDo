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

        public int CustomerServed = 0;
        public int CustomerServedRight = 0;
        public int CustomerServedWrong = 0;


        public void ResetScores()
        {
            CustomerServed = 0;
            CustomerServedRight = 0;
            CustomerServedWrong = 0;
        }
    }
}
        