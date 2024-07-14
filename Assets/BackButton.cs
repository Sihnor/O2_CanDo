using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using Code.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Button BackButtonB;
    
    // Start is called before the first frame update
    void Start()
    {
        this.BackButtonB = GetComponent<Button>();
        this.BackButtonB.onClick.AddListener(Back);
    }

    private void Back()
    {
        SceneLoader.Instance.LoadScene(EScenes.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
