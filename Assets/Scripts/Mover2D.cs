using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover2D : MonoBehaviour
{
    [SerializeField] float speed = 3f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = Input.GetAxisRaw("Horizontal") * speed;
        moveDirection.y = rb.velocity.y;

        rb.velocity = moveDirection;
    }

    public void DisableMovement(bool in3D)
    {
        rb.isKinematic = in3D;
        enabled = !in3D;

        rb.velocity = Vector3.zero;
    }
}
