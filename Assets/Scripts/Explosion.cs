using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Update()
    {
        Destroy(gameObject, 0.5f);        
    }
}
