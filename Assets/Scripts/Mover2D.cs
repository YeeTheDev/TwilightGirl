using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover2D : Mover
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override bool IsRigidbodyEnabled() { return !rb.isKinematic; }

    public override void Move(float xAxis, float zAxis)
    {
        Debug.Log("Moving 2D thingy");
        Vector2 moveDirection = Vector2.zero;
        moveDirection.x = xAxis * Speed;
        moveDirection.y = rb.velocity.y;

        rb.velocity = moveDirection;
    }

    public override void SetBehaviorOnMovement(bool use3DMovement)
    {
        rb.bodyType = use3DMovement ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
        rb.velocity = Vector3.zero;
    }
}
