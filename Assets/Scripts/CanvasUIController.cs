using UnityEngine;

public class CanvasUIController : MonoBehaviour
{
    [SerializeField] GameObject _mainScreen;
    [SerializeField] GameObject _pauseMenu;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ToggleScreen();
    }
    public void ToggleScreen()
    {
        if (isPaused)
        {
            isPaused = false;
            _pauseMenu.SetActive(false);
            _mainScreen.SetActive(true);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            _mainScreen.SetActive(false);
            _pauseMenu.SetActive(true);
        }
    }
}