using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightFade : MonoBehaviour {
    public float FadingSpeed = 1.5f;
    public float SizeMultiplier = 1.5f;
    private Light2D _light2D;
    private float _intensity;
    private float _size;
    private float _newSize;
    private float _time;

    private void Start() {
        _intensity = transform.GetComponent<Light2D>().intensity;
        _size = transform.localScale.x;
        _time = GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).length;
    }

    private void Update() {
        _intensity = Mathf.Lerp(_intensity, 0f,  _time * Time.deltaTime * FadingSpeed);
        _newSize = Mathf.Lerp(_newSize, _size * SizeMultiplier,  _time * Time.deltaTime * FadingSpeed);
        Debug.Log(_newSize);
        transform.GetComponent<Light2D>().intensity = _intensity;
        transform.localScale = new Vector3(_newSize, _newSize, transform.localScale.z);
    }
}
