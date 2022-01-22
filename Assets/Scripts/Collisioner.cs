using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisioner : MonoBehaviour
{
    [SerializeField] string goalTag = "Goal";
    [SerializeField] string damagerTag = "Damager";

    Vector3 startingPosition;

    private void Awake() { startingPosition = transform.position; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(goalTag))
        {
            int sceneToLoad = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(sceneToLoad);
        }

        if(other.CompareTag(damagerTag))
        {
            transform.position = startingPosition;
        }
    }
}
