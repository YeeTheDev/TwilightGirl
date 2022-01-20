using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover3D : Mover
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override bool IsRigidbodyEnabled() { return !rb.isKinematic; }

    public override void Move(float xAxis, float zAxis)
    {
        Debug.Log("3D thingy moving");
        Vector3 moveDirection = Vector3.one * rb.velocity.y;
        moveDirection.x = xAxis * Speed;
        moveDirection.z = zAxis * Speed;

        rb.velocity = moveDirection;
    }

    public override void SetBehaviorOnMovement(bool use3DMovement)
    {
        rb.isKinematic = !use3DMovement;
        rb.velocity = Vector3.zero;
    }
}
