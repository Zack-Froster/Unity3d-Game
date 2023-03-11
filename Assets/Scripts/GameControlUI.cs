using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameControlUI : MonoBehaviour
{


    public GameObject PauseButton;
    public GameObject StartButton;
    public GameObject ReplayButton;
    public GameObject BackButton;
    public GameObject Shade;


    private Button pause;
    private Button start;
    private Button replay;
    private Button back;



    public AudioSource BGMSE;
    public AudioSource ReplaySE;
    public AudioSource BackHomeSE;


    private void Awake()
    {
        pause = PauseButton.GetComponent<Button>();
        start = StartButton.GetComponent<Button>();
        replay = ReplayButton.GetComponent<Button>();
        back = BackButton.GetComponent<Button>();

    }




    public void OnPauseUIButton()
    {
        if (BGMSE.isPlaying == true)
        {
            BGMSE.Pause();
        }
        Time.timeScale = 0;
        StartButton.SetActive(true);
        PauseButton.SetActive(false);
        Shade.SetActive(true);

        
    }
    public void OnStartUIButton()
    {
        if(BGMSE.isPlaying == false)
        {
            BGMSE.UnPause();
        }
        Time.timeScale = 1;
        PauseButton.SetActive(true);
        StartButton.SetActive(false);

    }

    public void OnReplayUIButton()
    {
        ReplaySE.Play();
        Time.timeScale = 1;
        Invoke("Replay", 0.8f);
    }
    private void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void OnBackHomeButton()
    {
        BackHomeSE.Play();
        Time.timeScale = 1;
        Invoke("Back", 0.5f);
    }
    private void Back()
    {
        SceneManager.LoadScene(0);
    }

}
