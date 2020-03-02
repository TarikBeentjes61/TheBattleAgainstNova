using UnityEngine;

public class addlife : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Zoek naar het object Eventsystem en pak het script die de levens besturen, geef dan een plus getal mee.
        GameObject.Find("EventSystem").GetComponent<GameControlScript>().ChangeLife(1);
        Destroy(gameObject);
    }
}
