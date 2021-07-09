using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControls : MonoBehaviour
{
    [SerializeField] private Scene[] _scenes = new Scene[2];    
    public void OpenGame()
    {
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
        //SceneManager.UnloadSceneAsync("MainMenu");
        //SceneManager.SetActiveScene(_scenes[0]);
    }

    public void OpenAchievements()
    {
        SceneManager.LoadSceneAsync("Achievements"/*, LoadSceneMode.Additive*/);
        //SceneManager.UnloadSceneAsync("MainMenu");
    }

    public void OpenShop()
    {
        //here people will pay money for nothing
    }
    public void Exit()
    {
        Application.Quit();
    }
}
