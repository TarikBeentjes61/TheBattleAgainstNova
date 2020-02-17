using UnityEngine;

public class GameControlScript : MonoBehaviour
{
    public GameObject life1, life2, life3, gameOver;
    public int health = 3;

    void Start()
    {
        gameOver.gameObject.SetActive(false);
    }
    
    //Veranderd naar methods omdat update 60 keer per seconde wordt uitgevoerd wat niet erg handig is.
    //Als er levens worden verandert worden de methods pas uitgevoerd.
    public void ChangeLife(int addedNumber) {
        health += addedNumber;
        if (health > 3) {
            health = 3;
        }
        Debug.Log(health);
        CheckLife();
    }

    public void CheckLife() {
        switch (health)
        {
            case 3:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(true);
                life3.gameObject.SetActive(true);
                gameOver.gameObject.SetActive(false);
                break;
            case 2:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(true);
                life3.gameObject.SetActive(false);
                gameOver.gameObject.SetActive(false);
                break;

            case 1:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(false);
                life3.gameObject.SetActive(false);
                gameOver.gameObject.SetActive(false);
                break;

            case 0:
                life1.gameObject.SetActive(false);
                life2.gameObject.SetActive(false);
                life3.gameObject.SetActive(false);
                gameOver.gameObject.SetActive(true);
                Time.timeScale = 0;
                break;
        }
    }
}
