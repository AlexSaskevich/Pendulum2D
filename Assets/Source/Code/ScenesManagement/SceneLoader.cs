using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Code.ScenesManagement
{
    public class SceneLoader : ISceneLoader
    {
        public async Task<AsyncOperation> LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode,
            bool allowSceneActivation)
        {
            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode) ??
                                                throw new ArgumentException();

            loadSceneOperation.allowSceneActivation = allowSceneActivation;

            float targetProgress = allowSceneActivation ? 1f : 0.9f;

            while (Mathf.Approximately(loadSceneOperation.progress, targetProgress) == false)
            {
                await Task.Yield();
            }

            Debug.Log("Scene loaded!");

            return loadSceneOperation;
        }
    }
}