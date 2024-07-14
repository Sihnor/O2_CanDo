using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambience_sound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/music/music_game");
        FMODUnity.RuntimeManager.PlayOneShot("event:/gameplay/store_ambience");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
