using System.Collections;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class FadeToBlack : MonoBehaviour {
    private Image _image;
    private float _imageAlpha;
    public float FadeSpeed = 1f;
    private GameControlScript _controlScript;
    
    private void Start() {
        _controlScript = GameObject.Find("EventSystem").GetComponent<GameControlScript>();
        _image = GetComponent<Image>();
        _imageAlpha = _image.color.a;
    }

    public IEnumerator StartFading() {
        while (_imageAlpha < 1) {
            _imageAlpha = Mathf.MoveTowards(_imageAlpha, 1, Time.deltaTime);
            _image.color = new Color(0,0,0, _imageAlpha * FadeSpeed);
            Debug.Log(_imageAlpha);
            yield return null;
        }
        Debug.Log("DS");
        _controlScript.DeathScreen();
    }
}
