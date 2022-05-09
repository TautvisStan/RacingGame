using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    private bool open = false;
    [SerializeField] private Canvas menu;

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