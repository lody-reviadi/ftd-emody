using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    private AudioSource _audioSource;
    
    void Awake()
    {
        this._audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
    }
    
    public void PlayBGM()
    {
        _audioSource.Play();
    }

    public void StopBGM()
    {
        _audioSource.Stop();
    }

    public void ChangeBGM(AudioClip clip)
    {
        _audioSource.clip = clip;
        PlayBGM();
    }
}
