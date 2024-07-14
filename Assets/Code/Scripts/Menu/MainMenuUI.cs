using System;
using Code.Scripts.Managers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Scripts.Menu
{
    public class MainMenuUI : MonoBehaviour
    {
        private Animator Animator;
        [SerializeField] private menu_music SaniSound;
        
        [SerializeField] private Button NewGameButton;
        [SerializeField] private Button LoadGameButton;
        [SerializeField] private Button SettingsButton;
        [SerializeField] private Button CreditsButton;
        [SerializeField] private Button ExitButton;
        
        [SerializeField] private Button BackButton;
        
        private void Awake()
        {
            this.Animator = GetComponent<Animator>();
            this.NewGameButton.onClick.AddListener(StartGame);
            this.SettingsButton.onClick.AddListener(Settings);
            this.CreditsButton.onClick.AddListener(Credits);
            this.ExitButton.onClick.AddListener(ExitGame);           
            this.BackButton.onClick.AddListener(Back);
        }

        private void Back()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/ui/button_click");

            this.Animator.SetBool("OpenSettings", false);
            this.BackButton.gameObject.transform.parent.gameObject.SetActive(false);
            SceneLoader.Instance.UnloadScene(EScenes.Settings);
        }
        private void Credits()
        {

            FMODUnity.RuntimeManager.PlayOneShot("event:/ui/button_click");
            SceneLoader.Instance.LoadScene(EScenes.Credits);
        }
        private void StartGame()
        {

            FMODUnity.RuntimeManager.PlayOneShot("event:/ui/button_click");
            this.SaniSound.StopMusic();
            SceneLoader.Instance.LoadScene(EScenes.MainGame);
        }
        
        private void ExitGame()
        {

            FMODUnity.RuntimeManager.PlayOneShot("event:/ui/button_click");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif  
        }
        
        private void Settings()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/ui/button_click");

            this.Animator.SetBool("OpenSettings", true);
        }
        
        public void OpenSettings()
        {
            this.BackButton.gameObject.transform.parent.gameObject.SetActive(true);
            SceneLoader.Instance.LoadScene(EScenes.Settings, LoadSceneMode.Additive);
        }
    }
}