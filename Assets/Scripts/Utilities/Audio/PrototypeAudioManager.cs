using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Utilities.Audio
{
    public class PrototypeAudioManager : MonoBehaviour, IAudioManager
    {
        private static PrototypeAudioManager _instance;

        [SerializeField] private AudioMixerGroup mixerGroup;
        
        [SerializeField] private SoundData[] sounds;

        private AudioSource _bgmSource;

        public bool IsReadyToPlayAudio { get; private set; }
        
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }

            for (var i = 0; i < sounds.Length; i++)
            {
                var source = gameObject.AddComponent<AudioSource>();
                sounds[i].mixerGroup = mixerGroup;
                
                source.clip = sounds[i].clip;
                source.loop = sounds[i].loop;
                source.outputAudioMixerGroup = mixerGroup;
                source.playOnAwake = false;
                
                sounds[i].source = source;
            }

            _bgmSource = gameObject.AddComponent<AudioSource>();

            IsReadyToPlayAudio = true;
        }

        public void Play(string soundName)
        {
            if (!IsReadyToPlayAudio)
            {
                return;
            }
            
            if (!Array.Exists(sounds, item => item.name == soundName))
            {
                return;
            }
            
            var s = Array.Find(sounds, item => item.name == soundName);

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = mixerGroup;
            s.source.playOnAwake = false;

            s.source.Play();
        }

        public void PlayBGM(string bgmName)
        {
            if (!IsReadyToPlayAudio)
            {
                return;
            }
            
            if (!Array.Exists(sounds, item => item.name == bgmName))
            {
                return;
            }
            
            var s = Array.Find(sounds, item => item.name == bgmName);

            _bgmSource.volume = s.volume;
            _bgmSource.pitch = s.pitch;
            
            if ((_bgmSource.clip != null) && (_bgmSource.clip == s.clip))
            {
                return;
            }
            
            _bgmSource.clip = s.clip;
            
            
            _bgmSource.loop = s.loop;
            _bgmSource.outputAudioMixerGroup = mixerGroup;
            _bgmSource.playOnAwake = false;

            _bgmSource.Play();
        }

        public void SetBGMVolume(float newVolume)
        {
            _bgmSource.volume = newVolume;
        }

        public void Stop(string soundName)
        {
            if (!Array.Exists(sounds, item => item.name == soundName))
            {
                return;
            }

            var s = Array.Find(sounds, item => item.name == soundName);
            
            s.source.Stop();
        }

        public void StopAllSounds()
        {
            foreach (var sound in sounds)
            {
				sound.source.Stop();
			}
		}
    }
}