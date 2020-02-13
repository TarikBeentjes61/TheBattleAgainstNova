using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addlife : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameControlScript.health += 1;
    }
}
