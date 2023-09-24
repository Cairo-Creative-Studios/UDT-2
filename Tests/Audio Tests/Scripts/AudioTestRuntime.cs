using Rich.System;
using UnityEngine;

namespace AudioTest
{
    public class AudioTestRuntime : Runtime<AudioTestRuntime, AudioTestData>
    {
        int hitCount = 37;
        float timer = 0f;
        int index = 0;

        void Start()
        {
            PlayAudio("TestSong_CynicMusic", "Music", Rich.Audio.AudioClipType.Music);
        }

        void Update()
        {
            timer += Time.deltaTime;
            if(timer > 1f)
            {
                index++;
                index = index % hitCount;
                timer = 0f;
                PlayAudio("hit"+index+".mp3", "SFX", Rich.Audio.AudioClipType.SFX);
            }
        }
    }
}