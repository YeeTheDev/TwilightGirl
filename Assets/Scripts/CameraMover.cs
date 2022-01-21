using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TG.Abilities;

public class CameraMover : MonoBehaviour
{
    [SerializeField] float moveTime = 2f;
    [SerializeField] string playerTag = "Player";
    [SerializeField] LayerMask layerToHide = 0;
    [SerializeField] Transform realCamera = null;
    [SerializeField] Transform dummyCam2D = null;
    [SerializeField] GameObject objects3D = null;

    Vector3 position3D;
    Quaternion rotation3D;
    Swapper swapper;

    private void Awake()
    {
        position3D = realCamera.position;
        rotation3D = realCamera.rotation;

        swapper = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Swapper>();
        swapper.onSwap += MoveCamera;
    }

    private void MoveCamera()
    {
        bool using3DView = !objects3D.activeInHierarchy;

        objects3D.SetActive(using3DView);
        Camera.main.cullingMask = using3DView ? ~0 : ~layerToHide;
        StartCoroutine(LerpMove(using3DView));
    }

    private IEnumerator LerpMove(bool using3DView)
    {
        float timer = 0f;

        Vector3 targetPosition = using3DView ? position3D : dummyCam2D.position;
        Vector3 currentPosition = realCamera.position;

        Quaternion targetRotation = using3DView ? rotation3D : dummyCam2D.rotation;
        Quaternion currentRotation = realCamera.rotation;

        while (timer < moveTime)
        {
            timer += Time.deltaTime;
            realCamera.position = Vector3.Lerp(currentPosition, targetPosition, timer / moveTime);
            realCamera.rotation = Quaternion.Lerp(currentRotation, targetRotation, timer / moveTime);

            yield return new WaitForEndOfFrame();
        }

        realCamera.position = targetPosition;
        realCamera.rotation = targetRotation;
    }
}
