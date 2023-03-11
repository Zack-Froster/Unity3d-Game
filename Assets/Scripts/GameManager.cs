using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject VictotyUI;
    public GameObject GameOverUI;

    public static GameManager Instance;
    private EnemySpawner enemySpawner;

    public AudioSource ReplaySE;
    public AudioSource BackHomeSE;

    public AudioSource BGMSE;
    public AudioSource GameOverSE;
    public AudioSource VictorySE;


    public void Awake()
    {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }
    public void Victory()
    {
        VictotyUI.SetActive(true);
        BGMSE.Stop();
        VictorySE.Play();

    }
    public void GameOver()
    {
        if (GameOverSE.isPlaying == false)
        {
            GameOverSE.Play();
        }
        enemySpawner.Stop();
        GameOverUI.SetActive(true);
        if(BGMSE.isPlaying == true)
        {
            BGMSE.Stop();
        }

    }

    public void OnReplayButton()
    {
        ReplaySE.Play();
        Invoke("Replay", 0.8f);
    }

    private void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnBackButton()
    {
        BackHomeSE.Play();
        Invoke("Back", 0.5f);
    }
    private void Back()
    {
        SceneManager.LoadScene(0);
    }
}
