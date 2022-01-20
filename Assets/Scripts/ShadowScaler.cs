using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScaler : MonoBehaviour
{
    [SerializeField] float shadowMaxSize = 2f;
    [SerializeField] float maxDistance = 9f;
    [SerializeField] Transform shadow = null;
    [SerializeField] Transform model = null;

    float shadowZPosition;

    private void Awake() { shadowZPosition = shadow.position.z; }

    private void Update()
    {
        ClampShadowZPosition();
        ScaleShadowByDistance();
    }

    private void ClampShadowZPosition()
    {
        Vector3 zClampedPosition = shadow.position;
        zClampedPosition.z = shadowZPosition;
        shadow.position = zClampedPosition;
    }

    private void ScaleShadowByDistance()
    {
        float lerpT = (shadow.position.z - model.position.z - model.localScale.z / 2) / 9;
        float scaleByDistance = Mathf.Lerp(1, shadowMaxSize, lerpT);
        shadow.localScale = Vector3.one * scaleByDistance;
    }
}
