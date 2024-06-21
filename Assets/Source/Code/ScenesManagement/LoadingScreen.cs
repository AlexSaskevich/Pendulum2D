using UnityEngine;

namespace Source.Code.ScenesManagement
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [field: SerializeField] public ProgressBar ProgressBar { get; private set; }

        public void Show()
        {
            gameObject.SetActive(true);
            ProgressBar.Init();
            Debug.Log("Loading screen showed!");
        }

        public void Hide()
        {
            ProgressBar.Dispose();
            gameObject.SetActive(false);
            Debug.Log("Loading screen hided!");
        }

        private void OnDestroy()
        {
            ProgressBar.Dispose();
        }
    }
}