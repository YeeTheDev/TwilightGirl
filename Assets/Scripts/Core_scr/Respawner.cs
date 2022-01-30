using UnityEngine;

namespace TG.Core
{
    public class Respawner : MonoBehaviour
    {
        [SerializeField] float minHeightToRespawn = -4;

        Vector3 respawnPosition;

        private void Awake() { respawnPosition = transform.position; }

        public void CheckFall() { if (transform.position.y < minHeightToRespawn) { Respawn(); } }

        public void Respawn() { transform.position = respawnPosition; }
    }
}