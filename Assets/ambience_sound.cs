using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using Code.Scripts.Managers;
using UnityEngine;

public class ambience_sound : MonoBehaviour
{
    private FMOD.Studio.EventInstance GameMusic;
    private FMOD.Studio.EventInstance AmbientMusic;
    
    // Start is called before the first frame update
    void Start()
    {
        this.GameMusic = FMODUnity.RuntimeManager.CreateInstance("event:/music/music_game");
        this.AmbientMusic = FMODUnity.RuntimeManager.CreateInstance("event:/gameplay/store_ambience");
        
        this.GameMusic.start();
        this.AmbientMusic.start();
    }

    // Update is called once per frame
    void Update()
    {
        // If esc is pressed, go back to the main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/ui/button_click");
            
            this.GameMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            this.AmbientMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            GameManager.Instance.ResetScores();
            SceneLoader.Instance.LoadScene(EScenes.MainMenu);
        }
    }
}
