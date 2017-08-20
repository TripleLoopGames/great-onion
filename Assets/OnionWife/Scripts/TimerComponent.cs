using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerComponent : MonoBehaviour
{
    public TimerComponent StartTimer(int time, Action<int> onSecond, Action onEnd)
    {
        StopCorutines();
        this.timeSpent = 0;
        this.onSecond = onSecond;
        this.onEnd = onEnd;
        this.coroutine = WaitAndExecuteEverySecond(time, onSecond, onEnd);
        StartCoroutine(this.coroutine);
        return this;
    }

    public TimerComponent StartTimer(int time, Action onEnd)
    {
        StartTimer(time, (value) => { }, onEnd);
        return this;
    }

    public TimerComponent StopTimer()
    {
        StopCorutines();
        return this;
    }

    public TimerComponent RestartTimer()
    {
        StopCorutines();
        this.coroutine = WaitAndExecuteEverySecond(this.timeLeft, this.onSecond, this.onEnd);
        StartCoroutine(this.coroutine);
        return this;
    }

    public TimerComponent SetTimeLeft(int time)
    {
        this.timeLeft = time;
        this.onSecond(this.timeLeft);
        return this;
    }

    public int GetTimeLeft()
    {
        return this.timeLeft;
    }

    public int GetTimeSpent()
    {
        return this.timeSpent;
    }

    // oups this is not a timer this is a countdown
    private IEnumerator WaitAndExecuteEverySecond(int time, Action<int> onSecond, Action onEnd)
    {
        this.timeLeft = time;
        while (this.timeLeft > 0)
        {
            onSecond(this.timeLeft);
            yield return new WaitForSeconds(1f);
            this.timeLeft--;
            this.timeSpent++;
        }
        onSecond(this.timeLeft);
        onEnd();
        this.coroutine = null;
    }

    private TimerComponent StopCorutines()
    {
        if (this.coroutine != null)
        {
            StopCoroutine(this.coroutine);
            this.coroutine = null;
        }
        return this;
    }

    private Action<int> onSecond;
    private Action onEnd;
    private int timeLeft;
    private int timeSpent = 0;
    private IEnumerator coroutine;
}