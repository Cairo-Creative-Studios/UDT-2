using UDT.DataContainers;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace UDT.Audio
{
    public class AudioManagerData : Data
    {
        public List<AudioMixer> mixers = new();
    }
}
