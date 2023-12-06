using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    [System.Obsolete]
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().Reset();
    }
    public void LoadGameOver() 
    {
        //SceneManager.LoadScene("Game Over");
        //SceneManager.LoadScene("Game Over", LoadSceneMode.Additive);
        StartCoroutine(DelayGameLoadScreen());
    }
    IEnumerator DelayGameLoadScreen()
    {
        //SceneManager.LoadScene("Game Over", LoadSceneMode.Additive);
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }
    public void LoadGameScene()
    {
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Click");
    }
}
