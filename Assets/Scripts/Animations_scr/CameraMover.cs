using System.Collections;
using UnityEngine;
using TG.Abilities;

namespace TG.Animations
{
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
            float timer = 0;
            while (timer < timeToMove)
            {
                timer += Time.deltaTime;

                transform.position = Vector3.Lerp(GetStartPosition(), GetTargetPosition(), timer / timeToMove);
                transform.rotation = Quaternion.Lerp(GetStartRotation(), GetTargetRotation(), timer / timeToMove);

                yield return new WaitForEndOfFrame();
            }

            transform.position = GetTargetPosition();
            transform.rotation = GetTargetRotation();
        }

        private Vector3 GetStartPosition() { return inShadowRealm ? position3D : dummyCam.position; }
        private Vector3 GetTargetPosition() { return inShadowRealm ? dummyCam.position : position3D; }

        private Quaternion GetStartRotation() { return inShadowRealm ? rotation3D : dummyCam.rotation; }
        private Quaternion GetTargetRotation() { return inShadowRealm ? dummyCam.rotation : rotation3D; }
    }
}