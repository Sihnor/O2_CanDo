using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_music : MonoBehaviour
{

    private FMOD.Studio.EventInstance instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/music/music_menu");
        instance.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StopMusic()
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
