using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //Simpel script om een nieuwe Scene te laden in middel van een knop
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    //Sluit het spel af
    public void Quit() {
        Application.Quit();
    }
}
