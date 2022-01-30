using UnityEngine;
using UnityEngine.SceneManagement;
using TG.Core;

namespace TG.SceneControl
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] float extraTimeToLoad = 0.1f;

        int sceneToLoad;
        SceneEnder sceneEnder;

        private void Awake()
        {
            sceneEnder = GetComponent<SceneEnder>();

            sceneEnder.onSceneEnd += LoadNextSceneWithDelay;
        }

        public void LoadNextSceneWithDelay()
        {
            sceneToLoad = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
            Invoke("LoadScene", 2);
        }

        private void LoadScene() { SceneManager.LoadScene(sceneToLoad); }
    }
}