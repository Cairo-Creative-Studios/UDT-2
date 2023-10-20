using UDT.Scriptables.Utilities;
using NaughtyAttributes;
using UnityEngine;
using UDT.Audio;

namespace UDT.Scriptables.Actions
{
    [CreateNodeMenu("Audio/Actions/Play Sound Clip")]
    public class PlaySoundClip : ActionNode
    {
        [Dropdown("GetSoundClips")]
        public string clip = "";
        public string tag = "";

        public override void Process()
        {
            AudioManager.Play(clip, tag, "", AudioClipType.SFX);
            base.Process();
        }

        public DropdownList<string> GetSoundClips()
        {
            DropdownList<string> returnList = new();
            var soundClips = Resources.LoadAll<AudioClip>("Sounds");

            foreach(var sound in soundClips)
            {
                returnList.Add(sound.name, sound.name);
            }

            return returnList;
        }
    }
}