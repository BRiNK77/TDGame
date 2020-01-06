using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab1;
    public Transform enemyPrefab2;
    public Transform enemyPrefab3;
    public Transform enemyPrefab4;

    public Transform spawnPoint;

    public static float timeToSpawn = 10f; // time before next wave spawns
    private float countDown; // time to spawn first wave, will set to 20 when game is complete
    private int waveNum = 0; // initial wave count

   // public Text waveCountText;
    public Text waveLeftText;
    public Text RoundNum;  // for display of round number
    public Text waveNumber; // for display of wave number

    public int[] totalEnemies; // array of total number of enemies to spawn each round
    public static int roundNum;
    public static int enemiesLeft;

    public int[] wave1Types;
    public int[] wave2Types;
    public int[] wave3Types;
    public bool wave1;
    public bool wave2;
    public bool wave3;
    public int[] nextWave = { 5, 10, 5, 10, 10, 10, 15, 15, 10, 20, 15, 15, 20, 20, 20, 25, 20, 25, 25, 25, 30, 20, 35, 35, 1 };
    void Start()
    {
        roundNum = 1;
        RoundNum.text = "1";
        waveNumber.text = "1";
        enemiesLeft = totalEnemies[roundNum-1];
        countDown = 10.1f;
        
      
    }
    void Update()
    {
        

        // if their are no more enemies, update enemies left, round num, and countdown to next wave start
        if(enemiesLeft <= 0)
        {
            // show message for round complete, start timer for next rounds waves to start
            roundNum++;
            RoundNum.text = roundNum.ToString();
            enemiesLeft = totalEnemies[roundNum - 1];
            Enemy.scaled = false; // sets scaled to false to begin checking if the next round should scale enemies
            countDown = 30f; // sets countdown to 30 to give player time to spend points before next wave

        }

        // if the countdown has been reached, start spawning enemies
        if (countDown <= 0f)
        {
            
            StartCoroutine(SpawnWaves()); // starts to spawn a wave
            countDown = 30; // sets time before each wave starts spawning
        }

        countDown -= Time.deltaTime; // decrements the count to next wave

        //waveCountText.text = Mathf.Round(countDown).ToString();
        waveLeftText.text = enemiesLeft.ToString();
    }

    // needs enumerator to allow ability to wait seperately from this scripts timing, as a co-routine
    IEnumerator SpawnWaves()
    {
        // increases wave number, spawning first 1 enemy, then 2, then 3, and so on
        //waveNum++;
        SetWaveTypes();

        for (int i = 0; i < nextWave[waveNum]; i++)
        {
            foreach(int number in wave1Types)
            {
                waveNumber.text = "1";
                SpawnEnemy(number); // spawns an enemy
                yield return new WaitForSeconds(0.5f); // interval of time between enemies in a wave
            }
            yield return new WaitForSeconds(10f);


            foreach (int number in wave2Types)
            {
                waveNumber.text = "2";
                SpawnEnemy(number); // spawns an enemy
                yield return new WaitForSeconds(0.5f); // interval of time between enemies in a wave
            }
            yield return new WaitForSeconds(10f);

            foreach (int number in wave3Types)
            {
                waveNumber.text = "3";
                SpawnEnemy(number); // spawns an enemy
                yield return new WaitForSeconds(0.5f); // interval of time between enemies in a wave
            }
            yield return new WaitForSeconds(20f);

           // waveNumber.text = "1";
            // Notify player of next round/level
        }
        //Debug.Log("Wave Incoming");
        waveNum++;
        //countDown = 60;

    }

    void SpawnEnemy(int num)
    {
        
        // spawns number of enemies given by nextWave
        
           if(num == 1)
            {
                Instantiate(enemyPrefab1, spawnPoint.position, spawnPoint.rotation);
            }
            else if(num == 2)
            {
                Instantiate(enemyPrefab2, spawnPoint.position, spawnPoint.rotation);
            } else if(num == 3)
            {
            Instantiate(enemyPrefab3, spawnPoint.position, spawnPoint.rotation);
            } else if(num == 4)
            {
            Instantiate(enemyPrefab4, spawnPoint.position, spawnPoint.rotation);
            }
           
            
    }

    // defines the type of enemy to use depending on the round
    void SetWaveTypes()
    {
       
        if (roundNum == 1)
        {
            
            wave1Types = new int[] { 1, 1, 1, 1, 1 };//  5
            wave2Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };//  10
            wave3Types = new int[] { 1, 1, 1, 1, 2 };// 5
            return;                                        // total 20
        }

        if (roundNum == 2)
        {
            wave1Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };// 15
            wave2Types = new int[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };// 10
            wave3Types = new int[] { 2, 2, 2, 2, 2 };// 5
            return;                                       // total 30
        }

        if (roundNum == 3)
        {
            wave1Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };// 15
            wave2Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };//  15
            wave3Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 3 };// 10
            return;                                                      // total 40
        }

        if (roundNum == 4)
        {
            wave1Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};// 20
            wave2Types = new int[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };// 15
            wave3Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 };// 15
            return;                                                                  // total 50
        }

        if (roundNum == 5)
        {
            wave1Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }; // 20
            wave2Types = new int[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }; // 20
            wave3Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }; // 20
            return;                                                                                 // total 60
        }

        if (roundNum == 6)
        {
            wave1Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };// 25
            wave2Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };// 20
            wave3Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 ,3, 3, 3, 3 };// 25
            return;                                                                                     // total 70
        }

        if (roundNum == 7)
        {
            wave1Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };// 25
            wave2Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 ,2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };// 25
            wave3Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };// 30
            return;                                                                                             // total 80
        }

        if (roundNum == 8)
        {
            wave1Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };// 20
            wave2Types = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 };// 25
            wave3Types = new int[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };// 20
            return;                                                                             // total 65
        }

        if (roundNum == 9)
        {
            wave1Types = new int[] { 4 }; // 1
            return;
            //wave2Types = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            //wave3Types = new int[] { 1, 1, 1, 1, 2 };
        }
        
    }
}
