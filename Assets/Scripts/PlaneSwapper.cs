using UnityEngine;
using System;

namespace TG.Abilities
{
    public class PlaneSwapper : MonoBehaviour
    {
        public event Action onSwapPlane;

        [SerializeField] LayerMask shadowMask = 0;
        [SerializeField] Collider collider3D = null;
        [SerializeField] Collider colliderShadow = null;

        Rigidbody rb;

        private void Awake() { rb = GetComponent<Rigidbody>(); }

        public bool InShadowRealm { get => colliderShadow.enabled; }

        public void SwapPlane()
        {
            if (CheckIfShadowsTouching()) { return; }

            collider3D.enabled = !collider3D.enabled;
            colliderShadow.enabled = !colliderShadow.enabled;

            rb.constraints = InShadowRealm ?
                RigidbodyConstraints.FreezePositionZ ^ RigidbodyConstraints.FreezeRotation :
                RigidbodyConstraints.FreezeRotation;

            if (onSwapPlane != null) { onSwapPlane(); }
        }

        public bool CheckIfShadowsTouching()
        {
            Transform shadowTrans = colliderShadow.transform;
            CapsuleCollider capsule = colliderShadow as CapsuleCollider;

            if (shadowTrans != null && capsule != null && !colliderShadow.enabled)
            {
                Vector3 capsuleStart = shadowTrans.position + Vector3.right * capsule.center.x * 2 * shadowTrans.localScale.x;
                Vector3 capsuleEnd = capsuleStart + Vector3.up * capsule.height * 2 * shadowTrans.localScale.x;
                if (Physics.CheckCapsule(capsuleStart, capsuleEnd, capsule.radius, shadowMask)) { return true; }
            }

            return false;
        }
    }
}