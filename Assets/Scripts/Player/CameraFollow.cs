using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform Target;
    public float SmoothSpeed = 0.125f;
    public Vector3 OffSet;

    private SpriteRenderer _background;
    [SerializeField]
    private float _leftLimit;
    [SerializeField]
    private float _rightLimit;
    [SerializeField]
    private float _topLimit;
    [SerializeField]
    private float _bottomLimit;

    //Zoek alle hoeken van de map zodat de camera er niet uit kan
    private void Start() {
        float vertExtent = gameObject.GetComponent<Camera>().orthographicSize;  
        float horzExtent = vertExtent * Screen.width / Screen.height;
        
        _background = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        _leftLimit = -_background.sprite.bounds.size.x / 2.1f;
        _rightLimit = _background.sprite.bounds.size.x / 2.1f;
        _bottomLimit = -_background.sprite.bounds.size.y / 2.1f;
        _topLimit = _background.sprite.bounds.size.y  / 2.1f;    
    }

    //Volgt de speler tot er een limiet is bereikt.
    private void FixedUpdate() {
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(Target.position.x, _leftLimit, _rightLimit),
            Mathf.Clamp(Target.position.y, _bottomLimit, _topLimit),
            -1);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
        transform.position = smoothedPosition;
    }
}
