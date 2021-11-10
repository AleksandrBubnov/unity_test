using UnityEngine;

public class Player
{
    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Player();
            }
            return _instance;
        }
    }
    public int ScoreTotal { get; private set; } = 0;
    public int ScoreForOne { get; private set; } = 0;
    public int HealthMax { get; private set; } = 100;
    public int Health { get; private set; } = 100;
    public bool _isDead = false;

    public void TakeDamage(int value)
    {
        if (_isDead) return;

        AddHealth(-value);
        if (Health <= 0 && !_isDead) _isDead = true;
    }

    public void SetHealth(int health = 0)
    {
        this.Health = health;
    }
    public void AddHealth(int health = 0)
    {
        this.Health += health;
    }

    public void AddScore(int value = 0)
    {
        this.ScoreTotal += value;
    }
    public void AddScore()
    {
        this.ScoreTotal += this.ScoreForOne;
    }
    public void SetScore(int score = 0)
    {
        this.ScoreTotal = score;
    }
    public void ScoreToLog()
    {
        Debug.Log($"_scoreTotal: {this.ScoreTotal}");
    }

    #region Singleton
    //public class GameManager : MonoBehaviour
    //{
    //    public static GameManager instance { get; private set; }
    //    private void Awake()
    //    {
    //        if (instance == null)
    //        {
    //            instance = this;
    //            DontDestroyOnLoad(this.gameObject);
    //            return;
    //        }
    //        Destroy(this.gameObject);
    //    }
    //}
    #endregion
}
