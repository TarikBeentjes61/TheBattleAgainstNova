using TMPro;
using UnityEngine;

public class EndScore : MonoBehaviour {
    public GameObject Score;
    public GameObject Kills;
    
    private float _finalScore;
    private int _finalKills;
    private TextMeshProUGUI _scoreTextComponent;
    private TextMeshProUGUI _killsTextComponent;
    private void Start() {
        _finalScore = PlayerPrefs.GetInt("Score");
        _finalKills = PlayerPrefs.GetInt("Kills");
        _scoreTextComponent = Score.GetComponent<TextMeshProUGUI>();
        _killsTextComponent = Kills.GetComponent<TextMeshProUGUI>();
    }

    private float _countingScore;
    private float _countingKills;
    private void Update() {
        _countingScore = Mathf.MoveTowards(_countingScore, _finalScore, Time.deltaTime * _finalScore);
        _countingKills = Mathf.MoveTowards(_countingKills, _finalKills, Time.deltaTime * _finalKills);
        _scoreTextComponent.text = "Score: " + Mathf.RoundToInt(_countingScore);
        _killsTextComponent.text = "Kills: " + Mathf.RoundToInt(_countingKills);
    }
}
