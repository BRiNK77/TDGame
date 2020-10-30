using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    private Transform target;
    public GameObject impactEffect;
    public GameObject impactEffect2;
    private int wavepointIndex = 0;
    public static bool scaled = true;
   
    [SerializeField]
    public float speed = 6f;
    public int health = 2;
    public int damage = 1; // amount of damage enemy will do to player HP
    public int cost;
    void Start()
    {
        target = Waypoints.points[0];
    }

    void Update()
    {
        
        // scales the enemies based off the round number
        /*if (WaveSpawner.roundNum / 2 == 0 && scaled == false)
        {
            WaveSpawner.timeToSpawn += 2f;
            health += 2;
            speed += 2;
            cost = cost * 2;
            scaled = true;
        }
        */

        Vector3 dir = target.position - transform.position;  // creates vector to desired position based off current position
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); // uses vector to move enemy to desired position at set speed
        // uses normalized to ensure correct direction with set float for speed, along with deltaTime to ensure
        // movement is not frame dependant

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    //Function increments the waypointIndex and checks if last point has been reached
    void GetNextWayPoint()
    {
        // if enemy reaches last waypoint, destroy it, will also call damage function here for player health
        if(wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndReached();
            return; // used to ensure object is destroyed before moving past last waypoint
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    // Function destroys enemy and reduces player HP if enemy reaches the last check point(ie home)
    void EndReached()
    {
        Destroy(gameObject);
        PlayerStats.HP = PlayerStats.HP - damage;
        
    }

    public void TakeDamage(int dama)
    {
        health -= dama;
        if(health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        WaveSpawner.enemiesDestroyed += 1;
        PlayerStats.Energy += cost;

        Destroy(gameObject);
    }
}
