namespace Utilities.Audio
{
    public interface IAudioManager
    {
        bool IsReadyToPlayAudio { get; }
        void Play(string soundName);

        void PlayBGM(string bgmName);
        void SetBGMVolume(float newVolume);

        void Stop(string soundName);

        void StopAllSounds();
    }
}
