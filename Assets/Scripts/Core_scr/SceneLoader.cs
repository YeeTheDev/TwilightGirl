using UnityEngine;
using UnityEngine.SceneManagement;

namespace TG.Core
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] float extraTimeToLoad = 0.1f;

        int sceneToLoad;

        public void StartLoading(float timeBeforeLoad, int sceneIndex)
        {
            sceneToLoad = sceneIndex;
            Invoke("LoadScene", timeBeforeLoad + extraTimeToLoad);
        }

        public void LoadScene() { SceneManager.LoadScene(sceneToLoad); }
    }
}