using UnityEngine;

namespace TG.Abilities
{
    public class PlaneSwapper : MonoBehaviour
    {
        [SerializeField] float shadowCheckerRadius = 0.45f;
        [SerializeField] Collider collider3D = null;
        [SerializeField] Collider colliderShadow = null;

        public bool InShadowRealm { get => colliderShadow.enabled; }

        public void SwapPlane()
        {
            if (CheckIfShadowsTouching()) { return; }

            collider3D.enabled = !collider3D.enabled;
            colliderShadow.enabled = !colliderShadow.enabled;
        }

        public bool CheckIfShadowsTouching()
        {
            BoxCollider box = colliderShadow as BoxCollider;
            if (box != null && !colliderShadow.enabled)
            {
                float radius = shadowCheckerRadius * colliderShadow.transform.localScale.x;
                Vector3 checkerCenter = colliderShadow.transform.position + box.center * colliderShadow.transform.localScale.x;
                if (Physics.CheckSphere(checkerCenter, radius)) { return true; }
            }

            return false;
        }
    }
}