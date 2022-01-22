using UnityEngine;

namespace TG.ShadowControl
{
    public class ShadowScaler : MonoBehaviour
    {
        [SerializeField] float shadowMaxSize = 2f;
        [SerializeField] float maxDistance = 9f;
        [SerializeField] float wallDistance = 5f;
        [SerializeField] Transform shadow = null;
        [SerializeField] Transform shadowCollider = null;

        float shadowZPosition;

        public float ShadowXScale { get => shadowCollider.localScale.x; }

        private void Awake() { shadowZPosition = shadowCollider.position.z; }

        private void Update()
        {
            ClampColliderZPosition();
            ScaleShadowByDistance();
        }

        private void ClampColliderZPosition()
        {
            Vector3 zClampedPosition = shadowCollider.position;
            zClampedPosition.z = shadowZPosition;
            shadowCollider.position = zClampedPosition;
        }

        private void ScaleShadowByDistance()
        {
            float lerpT = (wallDistance - transform.position.z - transform.localScale.z / 2) / maxDistance;
            float scaleByDistance = Mathf.Lerp(1, shadowMaxSize, lerpT);

            Vector3 shadowScale = Vector3.one * scaleByDistance;
            shadowScale.z = shadow.localScale.z;
            shadow.localScale = shadowScale;

            shadowScale.z = shadowCollider.localScale.z;
            shadowCollider.localScale = shadowScale;
        }
    }
}