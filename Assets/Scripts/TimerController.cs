using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Text stopwatchText;
    private float elapsedTime;
    private bool isRunning;

    void Start()
    {
        // Initialize
        elapsedTime = 0f;
        isRunning = true;
    }

    void Update()
    {
        // If the stopwatch is running, update the elapsed time
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateStopwatchText(elapsedTime);
        }
    }

    // Method to start the stopwatch
    public void StartStopwatch()
    {
        isRunning = true;
    }

    // Method to stop the stopwatch
    public void StopStopwatch()
    {
        isRunning = false;
    }

    // Method to reset the stopwatch


    // Update the UI Text component with the formatted time
    void UpdateStopwatchText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        int milliseconds = Mathf.FloorToInt((time * 1000F) % 1000F);
        stopwatchText.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }
}
