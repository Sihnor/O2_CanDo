using Code.Scripts;
using Code.Scripts.Managers;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> scriptsToDisable;

    [SerializeField] private TextMeshProUGUI total;
    [SerializeField] private TextMeshProUGUI right;
    [SerializeField] private TextMeshProUGUI wrong;

    [SerializeField] private int timeInSec = 60;
    private float curTime;

    private FMOD.Studio.EventInstance GameMusic;
    private FMOD.Studio.EventInstance AmbientMusic;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        this.GameMusic = FMODUnity.RuntimeManager.CreateInstance("event:/music/music_game");
        this.AmbientMusic = FMODUnity.RuntimeManager.CreateInstance("event:/gameplay/store_ambience");

        curTime = 0;
    }

    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= timeInSec)
        {
            ShowScore();
        }
    }

    private void ShowScore()
    {
        total.text = GameManager.Instance.CustomerServed.ToString();
        right.text = GameManager.Instance.CustomerServedRight.ToString();
        wrong.text = GameManager.Instance.CustomerServedWrong.ToString();

        foreach (MonoBehaviour script in scriptsToDisable)
        {
            if (script != null)
            {
                script.enabled = false;
            }
        }

        anim.SetTrigger("Show");
    }

    public void BackButton()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/ui/button_click");

        this.GameMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        this.AmbientMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        GameManager.Instance.ResetScores();
        SceneLoader.Instance.LoadScene(EScenes.MainMenu);
    }
}
