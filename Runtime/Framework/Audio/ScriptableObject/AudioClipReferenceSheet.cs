using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UDT.Audio
{
    public enum AudioClipType
    {
        Music,
        SFX
    }

    internal class AudioClipReferenceSheet : ScriptableObject
    {
        /// <summary>
        /// Access the AudioClipReferences in the Sheet by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AudioClipReference this[string name]
        {
            get
            {
                return audioClips.FirstOrDefault(x => x.name == name);
            }
            set
            {
                var index = audioClips.IndexOf(audioClips.FirstOrDefault(x => x.name == name));
                audioClips[index] = value;
            }
        }

        public List<AudioClipReference> audioClips = new();

        /// <summary>
        /// Returns True if the AudioClipReferenceSheet contains the AudioClip.
        /// </summary>
        /// <param name="clip"></param>
        /// <returns></returns>
        public bool Contains(AudioClip clip)
        {
            return audioClips.FirstOrDefault(x => x.clip == clip) != null;
        }
    }
}
