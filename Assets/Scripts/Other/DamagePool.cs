using System.Collections;
using UnityEngine;

public class DamagePool : MonoBehaviour {
    public float DamageIntervals = 1f;
    public float ScaleMultiplier = 2f;
    public float ScaleUpSpeed = 1f;
    public float ScaleDownSpeed = 2f;
    public float ActiveTime = 5f;
    private float _activeTimer;
    private bool _scalingDown;

    //Na een bepaalde tijd wordt de methode scaledown geroepen.
    private void Update() {
        _activeTimer += Time.deltaTime;
        if (!_scalingDown) {
            if (_activeTimer >= ActiveTime) {
                _scalingDown = true;
                StopCoroutine(_scaleUp);
                StartCoroutine(ScaleDown());
            }
        }
    }

    private Coroutine _scaleUp;
    private void Start() {
        _startingScale = transform.localScale.x;
        _endScale = _startingScale * ScaleMultiplier;
         _scaleUp = StartCoroutine(ScaleUp());
    }

    private float _startingScale;
    private float _endScale;
    //Methode maakt het object steeds groter tot een limiet.
    private IEnumerator ScaleUp() {
        float currentScale = transform.localScale.x;

        while (currentScale <= _endScale) {
            currentScale = transform.localScale.x;
            Vector2 vec = new Vector2(Mathf.Lerp(currentScale, _endScale, ScaleUpSpeed * Time.deltaTime)
                ,Mathf.Lerp(currentScale, _endScale, ScaleUpSpeed * Time.deltaTime));
            transform.localScale = vec;
            yield return null;
        }
    }
    //Methode maakt het object steeds kleiner tot 0.1 dan wordt het object verwijderd
    private IEnumerator ScaleDown() {
        float currentScale = transform.localScale.x;

        while (currentScale >= 0.1f) {
            currentScale = transform.localScale.x;
            Vector2 vec = new Vector2(Mathf.Lerp(currentScale, 0, ScaleDownSpeed * Time.deltaTime)
                ,Mathf.Lerp(currentScale, 0, ScaleDownSpeed * Time.deltaTime));
            transform.localScale = vec;
            yield return null;
        }
        Destroy(gameObject);
    }

    private float _timer;
    private bool _counting;

    //De timer houd bij hoelang het nodig heeft om damage op de speler te doen.
    private IEnumerator Timer() {
        _counting = true;
        while (_timer < DamageIntervals) {
            _timer += Time.deltaTime;
            yield return null;
        }

        _counting = false;
        _timer = 0;
        _readyToDamage = true;
    }

    private bool _readyToDamage;

    //Als er iets het gebied binnen loopt neemt het damage op.
    private void OnTriggerEnter2D(Collider2D other) {
        if (_scalingDown) {return;}
        if (other.gameObject.CompareTag("Player")) {
            GameObject.Find("EventSystem").GetComponent<GameControlScript>().ChangeLife(-1);
        }

        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<Health>().Damage(1);
        }
    }

    //Als er iets lang genoeg in het gebied is neemt het damage op.
    private void OnTriggerStay2D(Collider2D other) {
        if (_scalingDown) {return;}
        if (!_readyToDamage && !_counting) {
            StartCoroutine(Timer());
        }

        if (_readyToDamage) {
            if (other.gameObject.CompareTag("Player")) {
                GameObject.Find("EventSystem").GetComponent<GameControlScript>().ChangeLife(-1);
            }

            if (other.gameObject.CompareTag("Enemy")) {
                other.gameObject.GetComponent<Health>().Damage(1);
            }

            _readyToDamage = false;
        }
    }

    //Als er iets het gebied verlaat wordt de timer gereset.
    private void OnTriggerExit2D(Collider2D other) {
        if (_scalingDown) {return;}
        StopCoroutine(Timer());
        _readyToDamage = false;
        _counting = false;
        _timer = 0;
    }
}
