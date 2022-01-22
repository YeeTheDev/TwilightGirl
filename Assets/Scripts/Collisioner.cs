using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisioner : MonoBehaviour
{
    [SerializeField] string goalTag = "Goal";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(goalTag))
        {
            int sceneToLoad = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
