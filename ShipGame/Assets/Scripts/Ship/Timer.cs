using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public static Timer timer;

    public TextMeshProUGUI timerDisplay;

    private TimeSpan timePlaying;
    private bool timerStart;

    private float elapsedTime;

    // Start is called before the first frame update
    void Awake()
    {
        timer = this;
    }
    void Start()
    {
        timerDisplay.text = "Time: 00:00.00";
        timerStart = false;
    }
    // Update is called once per frame
    public void BeginTimer()
    {
        timerStart = true;
        elapsedTime = 0f;
    }

    public void EndTimer()
    {
        timerStart = false;
    }

    private IEnumerator UpdateTimer()
    {
        while(timerStart)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timerDisplay.text = timePlayingStr;

            yield return null;
        }
    }
}
