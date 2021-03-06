﻿using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Health : MonoBehaviour {
    public float ScoreValue = 10;
    public int HealthPoints;
    public GameObject Explosion;
    public GameObject SpawnPowerUp;
    public int DropChance = 10;
    private ScoreScript _scoreScript;
    
    //Deze method wordt geroepen als er damage wordt opgenomen. Er wordt dan een flash uitgevoerd voor feedback.
    //Als de enemy 0 of minder health over heeft explodeerd hij.
    public void Damage(int damage) {
        HealthPoints -= damage;
        if (HealthPoints <= 0) {
            Explode();
        }
        else {
            StartCoroutine(Flash());
        }
    }

    //Spawn powerup en update score
    public void Explode() {
        if (Random.Range(0, 100) < DropChance)
        {
            Instantiate(SpawnPowerUp, transform.position, Quaternion.identity);
        }
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDestroy() {
        if (_scoreScript) {
            _scoreScript.UpdateScore(ScoreValue);
            _scoreScript.Kills++;
        }
    }

    //Een flash voor feedback dat er iets is geraakt
    private Light2D _light2D;
    private float _intensity;
    private float _originalIntensity;
    private bool _flashCompleted;
    private bool _coroutineComplete;
    
    private void Start() {
        _scoreScript = GameObject.Find("ScoreText").GetComponent<ScoreScript>();
        _light2D = GetComponent<Light2D>();
        _originalIntensity = _light2D.intensity;
    }
    //Een snelle flash zodat de speler kan zien dat hij iets heeft geraakt.
    private IEnumerator Flash() {
        _intensity = _originalIntensity;
        while (!_coroutineComplete) {
            if (!_flashCompleted) {
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
