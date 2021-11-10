using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    [SerializeField] private int _scoreTotal;
    [SerializeField] private int _scoreForOne;

    private GameManager _gameManager;
    private void Start()
    {
        _gameManager = GameManager.GetInstance();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.name == "Obstacle")
        if (collision.collider.tag == "Obstacle")
        {
            _gameManager.LevelRestartIn();
            SetScore(0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            AddScore(1);
            Debug.Log($"score: {GetScore()}");
            Destroy(other.gameObject);
        }
    }
    public void AddScore(int value = 0)
    {
        this._scoreTotal += value;
    }
    public void AddScore()
    {
        this._scoreTotal += this._scoreForOne;
    }
    public void SetScore(int score = 0)
    {
        this._scoreTotal = score;
    }
    public int GetScore()
    {
        return this._scoreTotal;
    }
    public void ScoreToLog()
    {
        Debug.Log($"_scoreTotal: {this._scoreTotal}");
    }

}
