using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
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
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            _gameManager.AddScore(1);
            Debug.Log($"score: {_gameManager.GetScore()}");
            Destroy(other.gameObject);
        }
    }
}
