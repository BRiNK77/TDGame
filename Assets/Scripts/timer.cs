using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public float secondsT;
    public float minutesT;

    public Text timerTextM;    // for display of game time minutes
    public Text timerTextS;    // for display of game time seconds
    // Start is called before the first frame update
    void Start()
    {
        secondsT = 0;          // sets seconds for timer
        minutesT = 0;          // sets minutes for timer
    }

    // Update is called once per frame
    void Update()
    {
        secondsT += Time.deltaTime;
        timerTextS.text = string.Format("{0:00}", Mathf.Round(secondsT));
        // if seconds reaches 60, set it to zero, increment minutes by 1
        if (secondsT >= 59)
        {
            secondsT = 0;
            timerTextS.text = string.Format("{0:00}", secondsT);
            minutesT += 1;
            timerTextM.text = string.Format("{0:0}", Mathf.Round(minutesT));
        }
    }
}
