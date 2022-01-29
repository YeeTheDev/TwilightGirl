using UnityEngine;

namespace TG.Core
{
    public class Respawner : MonoBehaviour
    {
        Vector3 respawnPosition;

        private void Awake() { respawnPosition = transform.position; }

        public void Respawn() { transform.position = respawnPosition; }
    }
}