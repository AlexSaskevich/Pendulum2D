using System.Threading.Tasks;
using Source.Code.ScenesManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Source.Code.Infrastructure
{
    public abstract class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Button _switchSceneButton;
        [SerializeField] private LoadingScreen _loadingScreen;

        private ISceneLoader _sceneLoader;

        public abstract string TargetScene { get; }

        private void Start()
        {
            Bootstrap();
            OnBootstrap();
        }

        private void OnDestroy()
        {
            _switchSceneButton.onClick.RemoveListener(OnButtonClicked);
            OnDestroyed();
        }

        private async void OnButtonClicked()
        {
            await LoadScene();
        }

        protected async Task LoadScene(string sceneName = null)
        {
            _loadingScreen.Show();
            OnSwitchSceneButtonClicked();

            string targetScene = sceneName ?? TargetScene;

            Task<AsyncOperation> loadSceneOperation =
                _sceneLoader.LoadSceneAsync(targetScene, LoadSceneMode.Single, false);
            Task progressBarTask = _loadingScreen.ProgressBar.SetValue(1);

            await Task.WhenAll(progressBarTask, loadSceneOperation);

            if (loadSceneOperation.IsCompletedSuccessfully)
            {
                loadSceneOperation.Result.allowSceneActivation = true;
            }
        }

        private void Bootstrap()
        {
            _sceneLoader = new SceneLoader();
            _switchSceneButton.onClick.AddListener(OnButtonClicked);
        }

        protected abstract void OnBootstrap();
        protected abstract void OnDestroyed();
        protected abstract void OnSwitchSceneButtonClicked();
    }
}