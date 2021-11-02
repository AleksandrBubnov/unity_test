using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _score;
    private static GameManager instance;
    public static GameManager GetInstance()
    {
        if (instance == null) instance = new GameManager();
        return instance;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        this._score = 0;
    }
    public void AddScore(int value = 0)
    {
        this._score += value;
    }
    public void SetScore(int score = 0)
    {
        this._score = score;
    }
    public int GetScore()
    {
        return this._score;
    }
    public void LevelRestartIn(float time = 0)
    {
        Invoke("LevelRestart", time);
    }
    public void LevelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
