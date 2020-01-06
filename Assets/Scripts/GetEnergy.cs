using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnergy : MonoBehaviour
{
    public float resTime = 2f;
    public float time;
    public int ene = 1;
    // Start is called before the first frame update

    void Update()
    {
        
        if (resTime <= 0)
        {
            PlayerStats.Energy += ene;
            resTime = time;
        }

        resTime -= Time.deltaTime;
    }

    
}
