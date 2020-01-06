using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 50f;

    public GameObject bullet1Prefab;
    public GameObject bullet2Prefab;
    

    public float explosionRadius = 0f;
    public int damage = 1;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        //transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns = null;

        if (this.gameObject == bullet1Prefab)
        {
            effectIns = (GameObject)Instantiate(target.GetComponent<Enemy>().impactEffect, transform.position, transform.rotation);
        } else
        {
            effectIns = (GameObject)Instantiate(target.GetComponent<Enemy>().impactEffect2, transform.position, transform.rotation);
        }
        
        
        Destroy(effectIns, 2f);

        if(explosionRadius > 0f)
        {
            Explode();
        } else
        {
            Damage(target);
        }
        
        //Debug.Log("Hit Something!");
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] objHit = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in objHit)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        
        if(e != null)
        {
            e.TakeDamage(damage);
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);

    }
}
