using UnityEngine.InputSystem;
using UDT.Scriptables.Utilities;
using UDT.Controllables;
using NaughtyAttributes;
using UnityEngine;
using UDT.Audio;

namespace UDT.Scriptables.Actions
{
    public class PlayMusic : ActionNode
    {
        [Dropdown("GetMusicClips")]
        public string clip = "";
        public string tag = "Music";

        public override void Process()
        {
            AudioManager.StopWithTag(tag);
            AudioManager.Play(clip, tag, "", AudioClipType.Music);
            base.Process();
        }

        public DropdownList<string> GetSoundClips()
        {
            DropdownList<string> returnList = new();
            var soundClips = Resources.LoadAll<AudioClip>("Music");

            foreach(var sound in soundClips)
            {
                returnList.Add(sound.name, sound.name);
            }

            return returnList;
        }
    }
}