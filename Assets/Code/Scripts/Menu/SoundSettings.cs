using System;
using Code.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Menu
{
    public class SoundSettings : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(UnLoadSettingsMenu);
        }

        private void UnLoadSettingsMenu()
        {
            SceneLoader.Instance.UnloadScene(EScenes.Settings);
        }

    }
}