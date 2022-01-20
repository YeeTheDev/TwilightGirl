using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Swapper))]
public class Control : MonoBehaviour
{
    float xAxis;
    float zAxis;
    bool holdHBtn;
    bool holdVBtn;
    bool lastPressedHBtn;

    Mover mover;
    Swapper swapper;

    private void Awake() { swapper = GetComponent<Swapper>(); }
    private void Start() { mover = swapper.GetActiveMover(); }

    void Update()
    {
        holdHBtn = Input.GetButton("Horizontal");
        holdVBtn = Input.GetButton("Vertical");

        if (holdHBtn && !holdVBtn || holdVBtn && Input.GetButtonDown("Horizontal")) { lastPressedHBtn = true; }
        if (!holdHBtn && holdVBtn || holdHBtn && Input.GetButtonDown("Vertical")) { lastPressedHBtn = false; }

        xAxis = lastPressedHBtn ? Input.GetAxisRaw("Horizontal") : 0;
        zAxis = !lastPressedHBtn ? Input.GetAxisRaw("Vertical") : 0;

        if (Input.GetKeyDown(KeyCode.K)) { mover = swapper.Swap(); }
    }

    private void FixedUpdate()
    {
        Debug.Log(mover);
        mover.Move(xAxis, zAxis);
    }
}
