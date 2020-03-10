using UnityEngine;
using UnityEngine.UI;


public class ScoreScript : MonoBehaviour
{
    private Text _score;
    private float _currentScore;
    void Start()
    {
        _score = GetComponent<Text>();
        _score.text = "Score 0";
    }

    public void UpdateScore(float scorevalue) {
        _currentScore += scorevalue;
        _score.text = "Score " + _currentScore;
    }
}
