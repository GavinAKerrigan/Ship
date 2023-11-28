using UnityEngine;
using TMPro;
using System;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerDisplay;
    void Update() { timerDisplay.text = "Time: " + GetElapsedTime(); }
    public string GetElapsedTime() { return TimeSpan.FromSeconds(Time.time).ToString("mm':'ss'.'ff"); }
}