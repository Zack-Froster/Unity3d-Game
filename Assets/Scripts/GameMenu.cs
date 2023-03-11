using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameMenu : MonoBehaviour
{

    public GameObject blackScene;
	public void OnStartGame()
    {
        blackScene.SetActive(true);
        Invoke("StartGame", 1f);
    }
    public void OnExitGame()
    {
        

/*#if UNITY_EDITOR
        Invoke("Exit", 0.8f);
#else
#endif*/
        Invoke("ExitGame", 0.8f);
    }
/*    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }*/

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
