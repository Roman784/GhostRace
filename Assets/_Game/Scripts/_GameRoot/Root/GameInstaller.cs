using UI;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private UIRoot _uiRootPrefab;

        public override void InstallBindings()
        {
            BindProviders();
            BindUI();
        }

        private void BindProviders()
        {
            Container.Bind<SceneProvider>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<UIRoot>().FromComponentInNewPrefab(_uiRootPrefab).AsSingle().NonLazy();
        }
    }
}