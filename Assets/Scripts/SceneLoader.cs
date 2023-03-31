using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //niet erg netjes, maar laad de volgende scene
        SceneManager.LoadScene(2);
    }
    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ReloadGame()
    {
        Scene currentSceneIndex = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentSceneIndex.name);
    }
}
