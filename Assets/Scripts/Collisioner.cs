using UnityEngine;
using UnityEngine.SceneManagement;
using TG.Abilities;
using TG.ShadowControl;

public class Collisioner : MonoBehaviour
{
    [SerializeField] float checkerRadius = 0;
    [SerializeField] Transform ground3DChecker = null;
    [SerializeField] Transform shadowGroundChecker = null;
    [SerializeField] LayerMask groundLayer = 0;

    [Header("Tags")]
    [SerializeField] string goalTag = "Goal";
    [SerializeField] string damagerTag = "Damager";
    [SerializeField] string spiderTag = "Spider";
    [SerializeField] string tutorialTag = "Tutorial";

    Transform groundChecker;
    Vector3 startingPosition;
    ParticlePlayer particlePlayer;
    PlaneSwapper swapper;
    ShadowScaler shadowScaler;

    private void Awake()
    {
        groundChecker = ground3DChecker;
        startingPosition = transform.position;

        swapper = GetComponent<PlaneSwapper>();
        particlePlayer = GetComponent<ParticlePlayer>();
        shadowScaler = GetComponent<ShadowScaler>();

        swapper.onSwapPlane += SetGroundChecker;
    }

    public bool IsGrounded() { return Physics.CheckSphere(groundChecker.position, CheckerRadius(), groundLayer); }

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

        if (other.CompareTag(spiderTag))
        {
            if (swapper.InShadowRealm)
            {
                transform.position = startingPosition;
            }
            else
            {
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("Tutorial"))
        {
            other.GetComponentInChildren<Canvas>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tutorial"))
        {
            other.GetComponentInChildren<Canvas>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        particlePlayer.PlayDustParticles(IsGrounded());
    }

    public float CheckerRadius()
    {
        if (swapper.InShadowRealm) { return checkerRadius * shadowScaler.ShadowXScale; }
        else { return checkerRadius; }
    }

    private void SetGroundChecker() { groundChecker = swapper.InShadowRealm ? shadowGroundChecker : ground3DChecker; }

    private void OnDrawGizmos()
    {
        if (ground3DChecker == null || shadowGroundChecker == null) { return; }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(ground3DChecker.position, checkerRadius);
        Gizmos.DrawWireSphere(shadowGroundChecker.position, checkerRadius);
    }
}

