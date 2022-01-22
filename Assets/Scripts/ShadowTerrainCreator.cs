using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTerrainCreator : MonoBehaviour
{
    [SerializeField] GameObject terrain = null;
    [SerializeField] float depth = 7;

    private void Awake() { CreateShadowTerrain(); }

    private void CreateShadowTerrain()
    {
        GameObject shadowColliders = Instantiate(terrain, transform);
        shadowColliders.name = "Shadow Colliders";

        foreach (Transform child in shadowColliders.transform)
        {
            SetChildScale(child);
            SetChildPosition(child);
            DisableChildRenderer(child);
        }
    }

    private static void DisableChildRenderer(Transform child)
    {
        MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    private void SetChildPosition(Transform child)
    {
        Vector3 newPosition = child.position;
        newPosition.z = depth;
        child.position = newPosition;
    }

    private void SetChildScale(Transform child)
    {
        Vector3 normalizedScale = child.localScale;
        normalizedScale.z = 1;
        child.localScale = normalizedScale;
    }
}
