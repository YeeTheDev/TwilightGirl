using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover3D : MonoBehaviour
{
    [SerializeField] float speed = 3f;

    bool holdHBtn;
    bool holdVBtn;
    bool lastPressedHBtn;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        holdHBtn = Input.GetButton("Horizontal");
        holdVBtn = Input.GetButton("Vertical");

        if (holdHBtn && !holdVBtn || holdVBtn && Input.GetButtonDown("Horizontal")) { lastPressedHBtn = true; }
        if (!holdHBtn && holdVBtn || holdHBtn && Input.GetButtonDown("Vertical")) { lastPressedHBtn = false; }

        Vector3 moveDirection = Vector3.one * rb.velocity.y;
        moveDirection.x = lastPressedHBtn ? Input.GetAxisRaw("Horizontal") * speed : 0;
        moveDirection.z = !lastPressedHBtn ? Input.GetAxisRaw("Vertical") * speed : 0;

        rb.velocity = moveDirection;
    }
}
