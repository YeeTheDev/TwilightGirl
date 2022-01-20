using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : MonoBehaviour
{
    [SerializeField] Mover3D mover3D = null;
    [SerializeField] Mover2D mover2D = null;
    [SerializeField] Transform playerParent = null;

    bool use3DMovement = true;

    public Mover GetActiveMover()
    {
        if (mover3D.IsRigidbodyEnabled()) { return mover3D; }
        return mover2D;
    }

    public Mover Swap()
    {
        use3DMovement = !use3DMovement;

        mover3D.SetBehaviorOnMovement(use3DMovement);
        mover2D.SetBehaviorOnMovement(use3DMovement);

        mover3D.transform.parent = null;
        mover2D.transform.parent = null;
        mover3D.transform.parent = use3DMovement ? playerParent : mover2D.transform;
        mover2D.transform.parent = use3DMovement ? mover3D.transform : playerParent;

        return GetActiveMover();
    }
}
