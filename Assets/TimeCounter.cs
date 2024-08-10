using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public Text CounterText;

    private void Update()
    {
        // Get the time in seconds since the start of the game
        float time = Time.realtimeSinceStartup;

        // Calculate minutes, seconds and milliseconds
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        int milliseconds = Mathf.FloorToInt((time * 1000F) % 1000F);

        // Update the text to display time in minute:second:millisecond format
        CounterText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}