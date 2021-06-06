using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] private TMP_Text timer;
    public float totalTime { get; private set; }
    public float elapsedTime { get; private set; }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);

        string time = timeSpan.ToString("mm\\:ss\\:ff");
        timer.text = time;
    }

    public void startTimer()
    {
        gameObject.SetActive(true);
        elapsedTime = 0;
    }

    public float stopTimer()
    {
        gameObject.SetActive(false);
        return elapsedTime;
    }

    public void addTotalTime()
    {
        totalTime += elapsedTime;
    }

    public void restartTimer()
    {
        this.totalTime = 0;
    }

}
