using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TG.Abilities;

public class CameraMover : MonoBehaviour
{
    [SerializeField] float timeToMove = 0.25f;
    [SerializeField] LayerMask shadowRealmMask = 0;
    [SerializeField] Transform dummyCam = null;
    [SerializeField] string playerTag = "Player";

    bool inShadowRealm;
    Vector3 position3D;
    Quaternion rotation3D;
    PlaneSwapper planeSwapper;

    private void Awake()
    {
        planeSwapper = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlaneSwapper>();

        position3D = transform.position;
        rotation3D = transform.rotation;

        planeSwapper.onSwapPlane += StartMovingCamera;
    }

    private void StartMovingCamera()
    {
        inShadowRealm = !inShadowRealm;

        Camera.main.cullingMask = inShadowRealm ? (int)shadowRealmMask : ~0;

        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        Vector3 startPosition = inShadowRealm ? position3D : dummyCam.position;
        Quaternion startRotation = inShadowRealm ? rotation3D : dummyCam.rotation;

        Vector3 targetPosition = inShadowRealm ? dummyCam.position : position3D;
        Quaternion targetRotation = inShadowRealm ? dummyCam.rotation : rotation3D;

        float timer = 0;
        while (timer < timeToMove)
        {
            timer += Time.deltaTime;

            transform.position = Vector3.Lerp(startPosition, targetPosition, timer / timeToMove);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, timer / timeToMove);

            yield return new WaitForEndOfFrame();
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}
