using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] Text _scoreTxtUI;
    [SerializeField] Slider _sliderHealthUI;
    private Text _healthTxtUI;
    void Start()
    {
        _scoreTxtUI.text = $"Score:  {Player.Instance.ScoreTotal}";
        _healthTxtUI = _sliderHealthUI.GetComponentInChildren<Text>();
        _healthTxtUI.text = $"Health: {Player.Instance.Health}";
        _sliderHealthUI.maxValue = Player.Instance.HealthMax;
        _sliderHealthUI.value = Player.Instance.Health;
    }

    void Update()
    {
        _scoreTxtUI.text = $"Score:  {Player.Instance.ScoreTotal}";
        _healthTxtUI.text = $"Health: {Player.Instance.Health}";
        _sliderHealthUI.value = Player.Instance.Health;
    }
}