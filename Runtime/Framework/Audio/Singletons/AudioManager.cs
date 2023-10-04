using UDT.System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace UDT.Audio
{
    public class AudioManager : Singleton<AudioManager, AudioManagerData>
    {
        //Assets
        private AudioClip[] audioClipsInResources;
        private AudioClipReferenceSheet[] audioClipReferences;
        private AudioMixer[] audioMixers;

        public AudioMixer masterMixer;

        //Music
        private AudioSource defaultMusicSource;
        private AudioMixerGroup defaultMusicMixerGroup;

        private Dictionary<(string, AudioClipType, string), AudioSource> audioSources = new();

        //SFX
        private AudioSource defaultSFXSource;
        public AudioMixer defaultSFXMixer;
        private AudioMixerGroup defaultSFXMixerGroup;

        void Awake()
        {
            masterMixer = Resources.Load<AudioMixer>("MasterMixer");

            audioClipsInResources = Resources.LoadAll<AudioClip>("");
            audioClipReferences = Resources.LoadAll<AudioClipReferenceSheet>("");
            audioMixers = Resources.LoadAll<AudioMixer>("");

            defaultMusicSource = gameObject.AddComponent<AudioSource>();
            defaultSFXSource = gameObject.AddComponent<AudioSource>();
        }

        public static AudioMixer GetMixer(string name)
        {
            foreach(var mixer in singleton.audioMixers)
            {
                if(mixer.name == name)
                {
                    return mixer;
                }
            }
            return null;
        }

        /// <summary>
        /// Play an AudioClip by name. The Audio Manager searches the previously stored Reference Sheets, and the Resources folder for the AudioClip.
        /// If an AudioClipReference is used the AudioClipType is acquired from it. Otherwise, it must be specified.
        /// </summary>
        /// <param name="clipName"></param>
        /// <param name="reference"></param>
        /// <param name="searchResources"></param>
        /// <param name="clipType"></param>
        public static AudioClipReference Play(string clipName, string tag = "", string reference = "", AudioClipType clipType = AudioClipType.Music, bool searchResources = true)
        {
            if(reference == "")
            {
                var fromReferenceSheet = singleton.audioClipReferences.Where(x => x.audioClips.FirstOrDefault(x => x.name == clipName) != null).Select(x => x[clipName]).FirstOrDefault();
                var inResources = singleton.audioClipsInResources.FirstOrDefault(x => x.name == clipName);
                
                var clip = fromReferenceSheet != null ? fromReferenceSheet.clip : inResources;

                if (fromReferenceSheet != null)
                {
                    return Play(fromReferenceSheet, tag, fromReferenceSheet.type);
                }
                else
                {
                    var createdReference = ScriptableObject.CreateInstance<AudioClipReference>();
                    if(clip == null) return null;
                    
                    createdReference.clip = clip;
                    createdReference.name = clip.name;
                    return Play(createdReference, tag, clipType);
                }
            }
            return null;
        }

        /// <summary>
        /// Plays the AudioClip for the Reference given. The original reference is Cloned, and returned for further use.
        /// </summary>
        /// <param name="originalClipReference"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static AudioClipReference Play(AudioClipReference originalClipReference, string tag, AudioClipType type = AudioClipType.Music)
        {
            var instancedClipReference = Instantiate(originalClipReference);
            var createdAudioSource = new GameObject(type.ToString() + tag).AddComponent<AudioSource>();
            createdAudioSource.transform.parent = singleton.transform;

            var baseAudioSource = type == AudioClipType.Music ? singleton.defaultMusicSource : singleton.defaultSFXSource;

            createdAudioSource.volume = baseAudioSource.volume * instancedClipReference.volume;
            createdAudioSource.outputAudioMixerGroup = baseAudioSource.outputAudioMixerGroup;

            createdAudioSource.clip = instancedClipReference.clip;
            createdAudioSource.Play();

            singleton.audioSources.Add((originalClipReference.name, type, tag), createdAudioSource);

            instancedClipReference.instantiatedAudioSource = createdAudioSource;
            return instancedClipReference;
        }


        /// <summary>
        /// Stops all AudioSources playing AudioClips with the given name.
        /// </summary>
        /// <param name="name"></param>
        public static void StopWithName(string name)
        {
            foreach (var audioSourcesKey in singleton.audioSources.Keys)
            {
                if (audioSourcesKey.Item1 == name)
                {
                    singleton.audioSources[audioSourcesKey].Stop();
                }
            }
        }

        /// <summary>
        /// Stops all AudioSources of the given Type
        /// </summary>
        /// <param name="type"></param>
        public static void StopWithType(AudioClipType type)
        {
            foreach (var audioSourcesKey in singleton.audioSources.Keys)
            {
                if (audioSourcesKey.Item2 == type)
                {
                    singleton.audioSources[audioSourcesKey].Stop();
                }
            }
        }

        /// <summary>
        /// Stops all AudioSources with the given tag.
        /// </summary>
        /// <param name="tag"></param>
        public static void StopWithTag(string tag)
        {
            foreach (var audioSourcesKey in singleton.audioSources.Keys)
            {
                if(audioSourcesKey.Item3 == tag)
                {
                    singleton.audioSources[audioSourcesKey].Stop();
                }
            }
        }
    }
}
