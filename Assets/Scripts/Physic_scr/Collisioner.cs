using UnityEngine;
using TG.Abilities;
using TG.VFX;
using TG.Core;

namespace TG.Physic
{
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
        [SerializeField] string loaderTag = "Loader";

        Transform groundChecker;
        ParticlePlayer particlePlayer;
        PlaneSwapper swapper;
        ShadowScaler shadowScaler;
        Respawner respawner;
        SceneLoader sceneLoader;

        private void Awake()
        {
            groundChecker = ground3DChecker;

            swapper = GetComponent<PlaneSwapper>();
            particlePlayer = GetComponent<ParticlePlayer>();
            shadowScaler = GetComponent<ShadowScaler>();
            respawner = GetComponent<Respawner>();
            sceneLoader = GameObject.FindGameObjectWithTag(loaderTag).GetComponent<SceneLoader>();

            swapper.onSwapPlane += SetGroundChecker;
        }

        public bool IsGrounded() { return Physics.CheckSphere(groundChecker.position, CheckerRadius(), groundLayer); }

        public float CheckerRadius()
        {
            if (swapper.InShadowRealm) { return checkerRadius * shadowScaler.ShadowXScale; }
            else { return checkerRadius; }
        }

        private void OnTriggerEnter(Collider other) { ActionOnCollisionType(other.transform, true); }
        private void OnTriggerExit(Collider other) { ActionOnCollisionType(other.transform, false); }

        private void ActionOnCollisionType(Transform other, bool onEnter)
        {
            if (other.CompareTag(damagerTag)) { respawner.Respawn(); }
            else if (other.CompareTag(spiderTag)) { Destroy(other.gameObject); }
            else if (other.CompareTag(goalTag)) { sceneLoader.StartLoading(0, -1); }
            else if (other.CompareTag(tutorialTag)) { other.GetComponent<Animator>().SetBool("Reading", onEnter); }
        }

        private void OnCollisionEnter(Collision collision) { particlePlayer.PlayDustParticles(IsGrounded()); }

        private void SetGroundChecker() { groundChecker = swapper.InShadowRealm ? shadowGroundChecker : ground3DChecker; }

        private void OnDrawGizmos()
        {
            if (ground3DChecker == null || shadowGroundChecker == null) { return; }

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(ground3DChecker.position, checkerRadius);
            Gizmos.DrawWireSphere(shadowGroundChecker.position, checkerRadius);
        }
    }

}