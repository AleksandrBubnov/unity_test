using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject _gameOverPanel;

    void Update()
    {
        if (Player.Instance._isDead)
        {
            _gameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Player.Instance.SetScore(0);
        Player.Instance.SetHealth(100);
        Player.Instance._isDead = false;
    }
}