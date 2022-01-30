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
            if (sceneToLoad == -1)
            { sceneToLoad = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings; }
            Invoke("LoadScene", timeBeforeLoad + extraTimeToLoad);
        }

        private void LoadScene() { SceneManager.LoadScene(sceneToLoad); }
    }
}