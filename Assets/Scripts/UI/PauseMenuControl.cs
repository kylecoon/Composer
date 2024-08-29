using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuControl : MonoBehaviour
{
    private GameObject pauseMenu;
    private bool menuOpened = false;

    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        pauseMenu = canvas.transform.GetChild(canvas.transform.childCount - 1).gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }
    public void ToggleMenu()
    {
        menuOpened = !menuOpened;
        pauseMenu.transform.GetChild(0).gameObject.SetActive(menuOpened);
    }
}
