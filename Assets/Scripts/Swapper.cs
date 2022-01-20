using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : MonoBehaviour
{
    [SerializeField] Mover3D mover3D = null;
    [SerializeField] Mover2D mover2D = null;
    [SerializeField] Transform playerParent = null;

    bool in3D;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Swap();
        }
    }

    private void Swap()
    {
        in3D = !in3D;

        mover3D.DisableMovement(in3D);
        mover2D.DisableMovement(in3D);

        mover3D.transform.parent = null;
        mover2D.transform.parent = null;
        mover3D.transform.parent = in3D ? playerParent : mover2D.transform;
        mover2D.transform.parent = in3D ? mover3D.transform : playerParent;
    }
}
