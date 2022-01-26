using UnityEngine;
using TG.Core;

namespace TG.ShadowControl
{
    public class ShadowScaler : MonoBehaviour
    {
        [SerializeField] float shadowMaxSize = 2f;
        [SerializeField] float maxDistance = 9f;
        [SerializeField] float wallDistance = 5f;
        [SerializeField] Transform shadow = null;
        [SerializeField] Transform shadowCollider = null;
        [SerializeField] string terrainTag = "Terrain";

        float stuckZPosition;
        ShadowTerrainCreator terrainCreator;

        public float ShadowXScale { get => shadowCollider.localScale.x; }

        private void Awake()
        {
            terrainCreator = GameObject.FindGameObjectWithTag(terrainTag).GetComponent<ShadowTerrainCreator>();

            SetInitialZPosition();
        }

        private void SetInitialZPosition()
        {
            Vector3 shadowColliderStartPosition = transform.position;
            shadowColliderStartPosition.z = terrainCreator.GetDepth;
            shadowCollider.position = shadowColliderStartPosition;
            stuckZPosition = shadowCollider.position.z;
        }

        private void Update()
        {
            StuckColliderZPosition();
            ScaleShadowByDistance();
        }

        private void StuckColliderZPosition()
        {
            Vector3 zClampedPosition = shadowCollider.position;
            zClampedPosition.z = stuckZPosition;
            shadowCollider.position = zClampedPosition;
        }

        private void ScaleShadowByDistance()
        {
            float lerpT = (wallDistance - transform.position.z - transform.localScale.z / 2) / maxDistance;
            float scaleByDistance = Mathf.Lerp(1, shadowMaxSize, lerpT);

            Vector3 shadowScale = Vector3.one * scaleByDistance;
            shadowScale.x = shadow.localScale.x;
            shadow.localScale = shadowScale;

            shadowScale.x = shadowCollider.localScale.x;
            shadowCollider.localScale = shadowScale;
        }
    }
}