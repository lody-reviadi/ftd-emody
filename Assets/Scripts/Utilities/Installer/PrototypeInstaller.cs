using Utilities.Audio;
using Zenject;

namespace Utilities.Installer
{
    public class PrototypeInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAudioManager>()
                .To<PrototypeAudioManager>()
                .FromResource("Managers/PrototypeAudioManager")
                .AsSingle();
        }
    }
}
