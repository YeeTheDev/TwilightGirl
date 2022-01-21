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
            shadow.localScale = Vector3.one * scaleByDistance;
            shadowCollider.localScale = shadow.localScale;
        }
    }
}