using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuText;
    public GameObject instrucText;
    public void startGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void instrucPage()
    {
        menuText.SetActive(false);
        instrucText.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void backToTitle()
    {
        instrucText.SetActive(false);
        menuText.SetActive(true);

    }
}
