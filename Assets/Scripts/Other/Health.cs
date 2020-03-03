using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Health : MonoBehaviour {
    public int HealthPoints;
    public GameObject Explosion;
    
    public void Damage(int damage) {
        HealthPoints -= damage;
        if (HealthPoints <= 0) {
            Explode();
        }
        else {
            StartCoroutine(Flash());
        }
    }

    public void Explode() {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    //Een flash voor feedback dat er iets is geraakt
    private Light2D _light2D;
    private float _intensity;
    private float _originalIntensity;
    private bool _flashCompleted;
    private bool _coroutineComplete;
    
    private void Start() {
        _light2D = GetComponent<Light2D>();
        _originalIntensity = _light2D.intensity;
    }
    private IEnumerator Flash() {
        _intensity = _originalIntensity;
        while (!_coroutineComplete) {
            if (!_flashCompleted) {
                Debug.Log(_flashCompleted);
                _intensity = Mathf.Lerp(_intensity, 3f, 10f * Time.deltaTime);
                _light2D.intensity = _intensity;
                if (_intensity >= 2.5f) {
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
}
