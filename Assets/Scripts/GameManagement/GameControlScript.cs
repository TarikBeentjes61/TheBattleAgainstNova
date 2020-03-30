using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;

public class GameControlScript : MonoBehaviour
{
    public GameObject life1, life2, life3;
    public int health = 3;
    private ScoreScript _scoreScript;
    private GameObject _blackFade;
    private GameObject _player;
    private void Start()
    {
        _player = GameObject.Find("Player");
        _blackFade = GameObject.Find("FadeToBlack");
        _light2D = GameObject.FindWithTag("Player").GetComponent<Light2D>();
        _originalIntensity = _light2D.intensity;        
        _scoreScript = GameObject.Find("ScoreText").GetComponent<ScoreScript>();
    }
    
    //Veranderd naar methods omdat update 60 keer per seconde wordt uitgevoerd wat niet erg handig is.
    //Als er levens worden verandert worden de methods pas uitgevoerd.
    public void ChangeLife(int addedNumber) {
        if (addedNumber < 0) {
            StartCoroutine(Flash());
        }
        health += addedNumber;
        if (health > 3) {
            health = 3;
        }
        CheckLife();
    }
    //Een flash voor feedback dat er iets is geraakt
    private Light2D _light2D;
    private float _intensity;
    private float _originalIntensity;
    private bool _flashCompleted;
    private bool _coroutineComplete;
    
    private IEnumerator Flash() {
        _intensity = _originalIntensity;
        while (!_coroutineComplete) {
            if (!_flashCompleted) {
                _intensity = Mathf.Lerp(_intensity, 4f, 10f * Time.deltaTime);
                _light2D.intensity = _intensity;
                if (_intensity >= 3.5f) {
                    _flashCompleted = true;
                }
            } else {
                _intensity = Mathf.Lerp(_intensity, _originalIntensity, 10f * Time.deltaTime);
                _light2D.intensity = _intensity;
                if (_intensity <= _originalIntensity) {
                    _coroutineComplete = true;
                }
            }

            yield return null;
        }
        _flashCompleted = false;
        _coroutineComplete = false;

    }

    public GameObject Explosion;
    public void CheckLife() {
        switch (health) {
            case 3:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(true);
                life3.gameObject.SetActive(true);
                break;
            case 2:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(true);
                life3.gameObject.SetActive(false);
                break;

            case 1:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(false);
                life3.gameObject.SetActive(false);
                break;

            case 0:
                life1.gameObject.SetActive(false);
                life2.gameObject.SetActive(false);
                life3.gameObject.SetActive(false);
                _player.SetActive(false);
                Instantiate(Explosion, _player.transform.position, Quaternion.identity);
                StartCoroutine(_blackFade.GetComponent<FadeToBlack>().StartFading());
                break;
        }
    }

    public void DeathScreen() {
        PlayerPrefs.SetInt("Score", Mathf.RoundToInt(_scoreScript.CurrentScore));
        PlayerPrefs.SetInt("Kills",_scoreScript.Kills);
        SceneManager.LoadScene("DeathScreen");
    }
}
