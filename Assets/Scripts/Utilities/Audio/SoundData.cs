using UnityEngine;
using UnityEngine.Audio;

namespace Utilities.Audio
{
    [System.Serializable]
    public struct SoundData
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 1f)] public float volume;
        [Range(.1f, 3f)] public float pitch;
        public bool loop;

        public AudioMixerGroup mixerGroup;

        [HideInInspector] public AudioSource source;
    }
}