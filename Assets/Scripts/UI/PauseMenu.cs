using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void ExitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        EventBus.Publish(new LevelCompleteEvent(-2));
    }
    public void StartGame()
    {
        EventBus.Publish(new LevelCompleteEvent(-1));
    }
    public void ResumeGame()
    {
        GameObject.Find("Player").GetComponent<PauseMenuControl>().ToggleMenu();
    }
}
