using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public bool activePauseMenu = true;

    // Start is called before the first frame update
    void Start()
    {
        DisplayPauseMenu();
    }

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DisplayPauseMenu();
        }
    }

    public void DisplayPauseMenu()
    {
        if(activePauseMenu == true)
        {
            pauseMenu.SetActive(false);
            activePauseMenu = false;
            Time.timeScale = 1;
        }
        else if(activePauseMenu == false)
        {
            pauseMenu.SetActive(true);
            activePauseMenu = true;
            Time.timeScale = 0;
        }
    }
}
