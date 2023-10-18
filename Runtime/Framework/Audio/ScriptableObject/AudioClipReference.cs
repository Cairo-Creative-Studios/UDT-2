using NaughtyAttributes;
using UnityEngine;

namespace UDT.Audio
{
    [CreateAssetMenu(fileName = "AudioClipReference", menuName = "Rich/Audio/AudioClipReference")]
    public class AudioClipReference : ScriptableObject
    {
        public string Name;
        public AudioClip clip;
        public AudioClipType type;
        [MinMaxSlider(0, 1)]
        public float volume = 1;

        [HideInInspector]
        public AudioSource instantiatedAudioSource;
    }
}
