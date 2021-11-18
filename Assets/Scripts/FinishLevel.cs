using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] GameObject _playerPrefab;
    [SerializeField] Transform _startPoints;
    [SerializeField] Transform _finishPoints;

    [SerializeField] GameObject _mainScreen;
    [SerializeField] GameObject _nextLevelScreen;
    [SerializeField] GameObject _startScreen;
    [SerializeField] GameObject _endScreen;
    void Start()
    {

    }

    void Update()
    {
        if (Vector3.Distance(
            _playerPrefab.transform.position, 
            _finishPoints.position) < 5f)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            VisibleMainScreen(false);
            _nextLevelScreen.SetActive(true);
        }
    }

    private void VisibleMainScreen(bool value = true)
    {
        _mainScreen.SetActive(value);
    }
    private void StartGame()
    {
        _endScreen.SetActive(false);
        _startScreen.SetActive(true);
    }
    private void EndGame()
    {
        VisibleMainScreen(false);
        _endScreen.SetActive(true);
    }

    public void StartLevel()
    {
        EndGame();
        Invoke("StartGame", 60);
        Invoke("VisibleMainScreen", 120);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}