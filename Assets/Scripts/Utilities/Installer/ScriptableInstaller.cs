using UnityEngine;
using Utilities.Audio;
using Zenject;

namespace Utilities.Installer
{
    [CreateAssetMenu(fileName = "ScriptableInstaller", menuName = "Installers/ScriptableInstaller")]
    public class ScriptableInstaller : ScriptableObjectInstaller<ScriptableInstaller>
    {
        [SerializeField] private PrototypeAudioManager protoAudioManagerPrefab;
        public override void InstallBindings()
        {
            Container.Bind<IAudioManager>()
                .To<PrototypeAudioManager>()
                .FromComponentInNewPrefab(protoAudioManagerPrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}