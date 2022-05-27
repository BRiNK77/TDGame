using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab1;      // enemy prefabs for spawning into game, along with their spawn point 
    public Transform enemyPrefab2;
    public Transform enemyPrefab3;
    public Transform enemyPrefab4;
    public Transform spawnPoint;

    public Enemy e1; // gets the enemy component to envoke scaling
    public Enemy e2;
    public Enemy e3;

    private float countDown;            // time to spawn first wave, will set to 20 when game is complete

    public Text destroyedText;          // for display of enemies destoryed, round number, and wave number
    public Text RoundNum;
    public Text waveNumber;

    public static int roundNum;         // integer number keeping track of the round number
    public static int enemiesDestroyed; //integer keeping track of enemies destroyed

    public int[] wave1Types;            // array to store enemies' types in each wave
    public int[] wave2Types;
    public int[] wave3Types;

    public bool start;                 // boolean flags for keeping track of the start and end of each round
    public bool clear;
    void Start()
    {
        roundNum = 1;
        RoundNum.text = "1";   // sets round counter
        waveNumber.text = "1"; // sets wave counter
        enemiesDestroyed = 0;  // sets enemies destroyed counter
        countDown = 5.1f;      // initial count down time
        clear = false;         // round starts, is not cleared
        start = true;          // start of round
        setStartStat();        // sets initial stats at game start
      
    }
    void Update()
    {
        // if last round is clear, update the round UI, scale enemies, set clear = false and start = true
        if (clear)
        {
            RoundNum.text = roundNum.ToString();
            //TODO: show message for round completion

            clear = false;
            start = true;
         
        }

        // if countDown is 0 and round has started, begin coroutine
        if (countDown <= 0f && start)
        {
            StartCoroutine(SpawnWaves());  // starts to spawn a round
            
        }

        countDown -= Time.deltaTime;                      // decrements the count till first wave spawns
        destroyedText.text = enemiesDestroyed.ToString(); // updates enemiesDestroyed

    }

    // needs enumerator to allow ability to wait seperately from this scripts timing, as a co-routine
    // rountine calls setWaveTypes before generating each enemy based on given type for each wave
    IEnumerator SpawnWaves()
    {
        start = false;
        SetWaveTypes();

        foreach (int number in wave1Types)          // for each enemy type number in the first wave, make the enemy and wait
        {
            waveNumber.text = "1";                 // updates waveNumber UI
            SpawnEnemy(number);                    // spawns an enemy for each number in wave1Types
            yield return new WaitForSeconds(0.5f); // interval of time between enemies in a wave
        }

        // Could switch to waiting for wave clear before next wave here
        //TODO: Notify of next wave

        yield return new WaitForSeconds(10f);      // wait before spawning next wave, rinse and repeat till round end

        foreach (int number in wave2Types)
        {
            waveNumber.text = "2";
            SpawnEnemy(number);
            yield return new WaitForSeconds(0.5f);
        }
        
        yield return new WaitForSeconds(10f);

        foreach (int number in wave3Types)
        {
            waveNumber.text = "3";
            SpawnEnemy(number);
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(10f);

        clear = true;        // set clear to true
        doScaling(roundNum); // scale enemies 
        roundNum++;          // increment round number
        // TODO: Notify player of next round end

    }

    void setStartStat()
    {
        e1 = enemyPrefab1.GetComponent<Enemy>();
        e2 = enemyPrefab2.GetComponent<Enemy>();
        e3 = enemyPrefab3.GetComponent<Enemy>();

        e1.setStats(2, 1, 8, 1);
        e2.setStats(4, 2, 7, 2);
        e3.setStats(6, 4, 6, 5);

    }

    // TODO: make gameWinning screen appear on boss kill

    void doScaling(int round)
    {
        e1 = enemyPrefab1.GetComponent<Enemy>();
        e2 = enemyPrefab2.GetComponent<Enemy>();
        e3 = enemyPrefab3.GetComponent<Enemy>();

        if (round == 1)
        {
            e1.scaleUp(0, 0, 1, 0);
            e2.scaleUp(1, 0, 0, 0);
        }
        if(round == 2)
        {
            e1.scaleUp(1, 1, 2, 1);
            e2.scaleUp(2, 1, 1, 1);
        }
        if (round == 3)
        {
            e1.scaleUp(2, 2, 3, 2);
            e2.scaleUp(4, 2, 2, 3);
        }
        if (round == 4)
        {
            e1.scaleUp(3, 3, 4, 2);
            e2.scaleUp(6, 3, 2, 4);
            e3.scaleUp(4, 2, 2, 2);
        }
    }

    // function that will spawn an enemy type based on the int given
    void SpawnEnemy(int num)
    {
        if (num == 1)
        {
            Instantiate(enemyPrefab1, spawnPoint.position, spawnPoint.rotation);
        } else if (num == 2)
        {
            Instantiate(enemyPrefab2, spawnPoint.position, spawnPoint.rotation);
        } else if (num == 3)
        {
            Instantiate(enemyPrefab3, spawnPoint.position, spawnPoint.rotation);
        } else if (num == 4)
        {
            Instantiate(enemyPrefab4, spawnPoint.position, spawnPoint.rotation);
        } else if (num == 0)
        {
            // set flag for end of game
        }
        return;
    }

    // function that defines the type of enemy to use depending on the round, also organizes waves for each round
    // by defining their composition in each waveType array
    void SetWaveTypes()
    {
        if (roundNum == 1)
        {

            wave1Types = new int[] { 1, 1, 1, 1, 1 };               // 5         - 5
            wave2Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };// 10        - 15
            wave3Types = new int[] { 1, 1, 1, 1, 2 };               // 5
            return;                                                 // total 20
        }
        if (roundNum == 2)
        {
            wave1Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };// 15        - 35
            wave2Types = new int[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };               // 10        - 45
            wave3Types = new int[] { 2, 2, 2, 2, 2 };                              // 5         - 50
            return;                                                                // total 30
        }
        if (roundNum == 3)
        {
            wave1Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };// 15       - 65
            wave2Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };// 15       - 80
            wave3Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 3 };               // 10       - 90
            return;                                                                // total 40
        }
        if (roundNum == 4)
        {
            wave1Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };// 20       - 110
            wave2Types = new int[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };               // 15       - 125
            wave3Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 };               // 15       - 140
            return;                                                                               // total 50
        }
        if (roundNum == 5)
        {
            wave1Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }; // 20       - 160
            wave2Types = new int[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }; // 20       - 180
            wave3Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }; // 20       - 200
            return;                                                                                // total 60
        }
        if (roundNum == 6)
        {
            wave1Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };// 25       - 225
            wave2Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };               // 20       - 245
            wave3Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };// 25       - 270
            return;                                                                                              // total 70
        }
        if (roundNum == 7)
        {
            wave1Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };               // 25       - 295
            wave2Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };               // 25       - 320
            wave3Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };// 30       - 350
            return;                                                                                                             // total 80
        }
        if (roundNum == 8)
        {
            wave1Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };               // 20       - 370
            wave2Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 };// 25       - 395
            wave3Types = new int[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };               // 20       - 415
            return;                                                                                              // total 65
        }
        if (roundNum == 9)
        {
            wave1Types = new int[] { 4 }; // 1    - 416
            wave2Types = new int[] { 0 };
            wave3Types = new int[] { 0 };
            return;

        }
    }

}  // end class
