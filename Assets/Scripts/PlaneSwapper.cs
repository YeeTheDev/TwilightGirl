using UnityEngine;

namespace TG.Abilities
{
    public class PlaneSwapper : MonoBehaviour
    {
        [SerializeField] Collider collider3D = null;
        [SerializeField] Collider colliderShadow = null;

        public bool InShadowRealm { get => colliderShadow.enabled; }

        public void SwapPlane()
        {
            collider3D.enabled = !collider3D.enabled;
            colliderShadow.enabled = !colliderShadow.enabled;
        }
    }
}