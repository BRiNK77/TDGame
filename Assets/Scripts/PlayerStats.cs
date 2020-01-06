using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int Energy;
    public Text energyOnHand;
    public int startEnergy = 100; // initial starting money value to be set on static Money every time the game loads

    public static int HP;
    public int startHP = 25;
    public Text currentHP;

    public static bool gameEnd;
    public GameObject gameOverUI;
    void Start()
    {
        Energy = startEnergy;
        HP = startHP;
        gameEnd = false;
    }

    void Update()
    {
        if (gameEnd)
        {
            return;
        }
        energyOnHand.text = Energy.ToString();
        currentHP.text = HP.ToString();

        if(HP <= 0)
        {
            // stop game and display Game Over
            EndGame();

        }
    }

    void EndGame()
    {
        gameEnd = true;

        gameOverUI.SetActive(true);
        Debug.Log("GAME OVER");
    }
}
