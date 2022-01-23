using UnityEngine;

namespace TG.Core
{
    public class ShadowTerrainCreator : MonoBehaviour
    {
        [SerializeField] GameObject terrain = null;
        [SerializeField] float depth = 7;

        public float GetDepth { get => depth; }

        private void Awake() { CreateShadowTerrain(); }

        private void CreateShadowTerrain()
        {
            GameObject shadowColliders = Instantiate(terrain, transform);
            shadowColliders.name = "Shadow Colliders";

            foreach (Transform child in shadowColliders.transform)
            {
                SetChildPosition(child);
                SetChildScale(child);
                DisableChildRenderer(child);
            }
        }

        private void SetChildPosition(Transform child)
        {
            Vector3 newPosition = child.position;
            newPosition.z = depth;
            child.position = newPosition;
        }

        private static void DisableChildRenderer(Transform child)
        {
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            if (meshRenderer != null) { meshRenderer.enabled = false; }
        }

        private void SetChildScale(Transform child)
        {
            Vector3 normalizedScale = child.localScale;
            normalizedScale.z = 1;
            child.localScale = normalizedScale;
        }
    }
}