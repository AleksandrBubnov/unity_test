using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager GetInstance()
    {
        if (instance == null) instance = new GameManager();
        return instance;
    }
    private void Awake()
    {
        Time.timeScale = 1;
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this);

    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("Score")) Player.Instance.SetScore(PlayerPrefs.GetInt("Score"));
        if (PlayerPrefs.HasKey("Health")) Player.Instance.SetHealth(PlayerPrefs.GetInt("Health"));
    }
    public void LevelRestartIn(float time = 0)
    {
        Invoke("LevelRestart", time);
    }
    public void LevelRestart()
    {
        SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void NextLevel()
    {
        SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        //SceneManager.LoadScene(0);
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        SaveData();
        Application.Quit();
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt("Score", Player.Instance.ScoreTotal);
        PlayerPrefs.SetInt("Health", Player.Instance.Health);
    }
    public void DeleteData()
    {
        Player.Instance.SetScore(0);
        Player.Instance.SetHealth(Player.Instance.HealthMax);
        PlayerPrefs.SetInt("Score", Player.Instance.ScoreTotal);
        PlayerPrefs.SetInt("Health", Player.Instance.Health);
        //PlayerPrefs.DeleteAll();
    }
}