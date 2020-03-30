using UnityEngine;
using UnityEngine.UI;


public class ScoreScript : MonoBehaviour
{
    private Text _score;
    public float CurrentScore;
    public int Kills;

    void Start() {
        _score = GetComponent<Text>();
        _score.text = "Score 0";
    }

    public void UpdateScore(float scorevalue) {
        CurrentScore += scorevalue;
        _score.text = "Score " + CurrentScore;
    }
}
