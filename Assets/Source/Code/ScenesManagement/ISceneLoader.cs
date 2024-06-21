using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Code.ScenesManagement
{
    public interface ISceneLoader
    {
        public Task<AsyncOperation> LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode,
            bool allowSceneActivation);
    }
}