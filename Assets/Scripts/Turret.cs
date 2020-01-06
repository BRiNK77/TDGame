using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    public Transform partToRot;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float range = 15f;
    public float Tspeed = 10f;// speed turret rotates
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    public string enemyTag = "Enemy";


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDis = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach( GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDis)
            {
                shortestDis = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null && shortestDis <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
      if(target == null)
        {
            return;
        }

        // target location system

        Vector3 dir = target.position - transform.position; // determines distance from target to turret
        Quaternion lookRot = Quaternion.LookRotation(dir); // determines the rotation based off of dir

        Vector3 rota = Quaternion.Lerp(partToRot.rotation, lookRot, Time.deltaTime * Tspeed).eulerAngles;
        // gets the buffered turn speed from the Lerp function for the rotation and sets to eulerAngels

        partToRot.rotation = Quaternion.Euler(0f, rota.y, 0f);
        // rotates the partToRot to the determined angle with buffered speed

        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;

    }

    void Shoot()
    {
        //Debug.Log("Shoot!");
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if( bullet != null)
        {
            bullet.Seek(target);
        }
    }
    // function to draw the range of the turret in the scene 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
