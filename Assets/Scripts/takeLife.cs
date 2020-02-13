using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeLife : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameControlScript.health -= 1;
    }
}
