using System;
using UDT.Scriptables.Events;
using UnityEngine;

namespace UDT.Scriptables
{
    [Serializable]
    public class Timer
    {
        public string name;
        public float startTime;
        public float duration;
        public float currentTime;

        public Timer(string name, float duration)
        {
            this.name = name;
            this.duration = duration;
            startTime = Time.time;
        }

        public void Start()
        {
            OnTimerStarted.Invoke(name);
        }

        public void Advance()
        {
            currentTime = Time.time - startTime;
            if (Time.time - startTime > duration) OnTimerEnded.Invoke(name);
        }
    }
}