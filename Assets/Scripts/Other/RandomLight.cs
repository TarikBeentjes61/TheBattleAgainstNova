using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

[RequireComponent(typeof(Light2D))]
public class RandomLight : MonoBehaviour {
    public Gradient Gradient;
    public float Duration;
    private bool _switch;
    private Light2D _light2D;
    private float _timeTo;

    void Start() {
        _light2D = GetComponent<Light2D>();
    }
    private float _value;

    void Update() {
        if (!_switch) {
            _value = Mathf.Lerp(0f, 1f, _timeTo);
            if (_value >= 1) {
                _switch = true;
                _timeTo = 0;
            }
        }
        else {
            Debug.Log(_switch);
            _value = Mathf.Lerp(1f, 0f, _timeTo);
            if (_value <= 0) {
                _switch = false;
                _timeTo = 0;
            }
        }
        _timeTo += Time.deltaTime / Duration;
        Color color = Gradient.Evaluate(_value);
        _light2D.color = color;
        
    }
}
