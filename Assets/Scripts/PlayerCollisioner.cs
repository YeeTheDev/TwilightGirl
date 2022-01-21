using UnityEngine;
using UnityEngine.SceneManagement;

//TODO Massive Refactor
public class PlayerCollisioner : MonoBehaviour
{
    [SerializeField] Transform respawnPoint = null;
    [SerializeField] int levelToLoad = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Damager"))
        {
            transform.position = respawnPoint.position;
        }

        if (collision.transform.CompareTag("Goal"))
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
