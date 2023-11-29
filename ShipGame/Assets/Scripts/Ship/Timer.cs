using UnityEngine;
using TMPro;
using System;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerDisplay;
    private float elapsedTime;
    void Start() { elapsedTime = 0f; }
    void Update()
    {
        timerDisplay.text = "Time: " + GetElapsedTime();
        elapsedTime += Time.deltaTime;
    }
    public string GetElapsedTime() { return TimeSpan.FromSeconds(elapsedTime).ToString("mm':'ss'.'ff"); }
}