using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EscMenu : MonoBehaviour
{

    bool open = false;
    public Canvas menu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!open)
            {
                menu.enabled = true;
                open = true;
            }
            else
            {
                menu.enabled = false;
                open = false;
            }
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
